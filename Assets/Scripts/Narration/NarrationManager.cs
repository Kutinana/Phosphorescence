using System;
using System.Collections;
using System.Collections.Generic;
using Kuchinashi;
using Kuchinashi.Utils.Progressable;
using Phosphorescence.DataSystem;
using Phosphorescence.Game;
using QFramework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Phosphorescence.Narration
{
    public enum NarrationType
    {
        None,
        LeftAvatarText,
        RightAvatarText,
        FullScreenText,
        LeftAvatarOptions,
        RightAvatarOptions,
        FullScreenOptions,
        SubtitleText,
        Special
    }

    public partial class NarrationManager : MonoSingleton<NarrationManager>
    {
        public FSM<NarrationType> StateMachine => Instance._stateMachine ??= new();
        private FSM<NarrationType> _stateMachine;

        private PlotData m_currentPlot;
        public bool IsNarrationActive => m_currentPlot != null;

        public Progressable BlackEdgeProgressable;
        private NarrationComponent m_CurrentComponent;
        private bool m_CanReceiveInput = true;

        public CanvasGroupAlphaProgressable CanvasGroup;

        public SerializableDictionary<NarrationType, NarrationComponent> Components = new();

        private void Awake()
        {
            CanvasGroup = GetComponent<CanvasGroupAlphaProgressable>();
            CanvasGroup.Progress = 0f;

            StateMachine.AddState(NarrationType.None, new NoneState(StateMachine, this));
            StateMachine.AddState(NarrationType.LeftAvatarText, new LeftAvatarTextState(StateMachine, this));
            StateMachine.AddState(NarrationType.RightAvatarText, new RightAvatarTextState(StateMachine, this));
            StateMachine.AddState(NarrationType.FullScreenText, new FullScreenTextState(StateMachine, this));
            StateMachine.AddState(NarrationType.LeftAvatarOptions, new LeftAvatarOptionsState(StateMachine, this));
            StateMachine.AddState(NarrationType.FullScreenOptions, new FullScreenOptionsState(StateMachine, this));
            StateMachine.AddState(NarrationType.SubtitleText, new SubtitleTextState(StateMachine, this));
            StateMachine.AddState(NarrationType.Special, new SpecialState(StateMachine, this));

            StateMachine.StartState(NarrationType.None);

            TypeEventSystem.Global.Register<OnLineReadEvent>(e => {
                OnLineReceived(e);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            TypeEventSystem.Global.Register<OnLinesReadEvent>(e => {
                OnLinesReceived(e);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            TypeEventSystem.Global.Register<OnStoryEndEvent>(e => {
                StateMachine.ChangeState(NarrationType.None);

                if (!m_currentPlot.Temporary) GameProgressData.Instance.FinishPlot(m_currentPlot.Id);
                m_currentPlot = null;
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        public void StartNarration(string plotId)
        {
            if (IsNarrationActive) return;

            if (GameDesignData.GetData<PlotData>(plotId, out var plot))
            {
                m_currentPlot = plot;
                StateMachine.ChangeState(NarrationType.None);

                TypeEventSystem.Global.Send(new InitializeStoryEvent { plot = plot });
            }
            else
            {
                Debug.LogError($"Plot {plotId} not found");
            }
        }

        public void StopNarration()
        {
            StateMachine.ChangeState(NarrationType.None);
            m_currentPlot = null;
        }

        private void Update()
        {
            if (!m_CanReceiveInput) return;

            if (GameManager.Instance.nextLineAction.WasReleasedThisFrame())
            {
                if (m_CurrentComponent != null && !m_CurrentComponent.IsComplete)
                {
                    m_CurrentComponent.SkipAction?.Invoke();
                    return;
                }
                TypeEventSystem.Global.Send<RequestNewLineEvent>();
            }
        }

        private void OnLineReceived(OnLineReadEvent e)
        {
            if (e.content.StartsWith("$"))  // Situation for special line
            {
                StateMachine.ChangeState(NarrationType.Special);
                Components[NarrationType.Special].GetComponent<SpecialLineComponent>().Initialize(e);

                return;
            }

            if (!e.tags.TryGetValue("type", out var rawType))
            {
                StateMachine.ChangeState(NarrationType.None);
                return;
            }
            var type = Enum.Parse<NarrationType>(rawType);

            StateMachine.ChangeState(type);
            if (Components.TryGetValue(type, out var controller) && controller.TryGetComponent<ICanProcessLine>(out var component))
            {
                component.Initialize(e);
            }
        }

        private void OnLinesReceived(OnLinesReadEvent e)
        {
            if (!e.tags.TryGetValue("type", out var rawType))
            {
                StateMachine.ChangeState(NarrationType.None);
                return;
            }
            var type = Enum.Parse<NarrationType>(rawType);

            if (Components.TryGetValue(type, out var controller) && controller.TryGetComponent<ICanProcessLines>(out var component))
            {
                Debug.Log(type);
                component.Initialize(e);
                StateMachine.ChangeState(type);
            }
            else
            {
                var errorMessage = $"Lines cannot be processed: \nType: {e.tags["type"]}";
                foreach (var line in e.lines)
                {
                    errorMessage += $"{line.content}, {line.tags["type"]}\n";
                }
                Debug.LogError(errorMessage);
            }
        }
    }

    public partial class NarrationManager
    {
        public class NoneState : AbstractState<NarrationType, NarrationManager>
        {
            public NoneState(FSM<NarrationType> fsm, NarrationManager target) : base(fsm, target) { }
            protected override bool OnCondition() => mFSM.CurrentStateId is not NarrationType.None;
            protected override void OnEnter()
            {
                mTarget.CanvasGroup.InverseLinearTransition(0.2f, 0.5f);
                mTarget.BlackEdgeProgressable.InverseLinearTransition(0.5f);

                mTarget.m_CurrentComponent = null;
            }
            protected override void OnExit()
            {
                mTarget.CanvasGroup.LinearTransition(0.2f);
                mTarget.BlackEdgeProgressable.LinearTransition(0.5f, 0.5f);
            }
        }

        public class LeftAvatarTextState : AbstractState<NarrationType, NarrationManager>
        {
            public LeftAvatarTextState(FSM<NarrationType> fsm, NarrationManager target) : base(fsm, target) { }
            protected override bool OnCondition() => mFSM.CurrentStateId is not NarrationType.LeftAvatarText;
            protected override void OnEnter()
            {
                mTarget.Components[NarrationType.LeftAvatarText].CanvasGroup.LinearTransition(0.2f);
                mTarget.m_CurrentComponent = mTarget.Components[NarrationType.LeftAvatarText];
                mTarget.m_CurrentComponent.OnEnter();
            }
            protected override void OnExit()
            {
                mTarget.Components[NarrationType.LeftAvatarText].CanvasGroup.InverseLinearTransition(0.2f);
                mTarget.m_CurrentComponent.OnExit();
            }
        }

        public class RightAvatarTextState : AbstractState<NarrationType, NarrationManager>
        {
            public RightAvatarTextState(FSM<NarrationType> fsm, NarrationManager target) : base(fsm, target) { }
            protected override bool OnCondition() => mFSM.CurrentStateId is not NarrationType.RightAvatarText;
            protected override void OnEnter()
            {
                mTarget.Components[NarrationType.RightAvatarText].CanvasGroup.LinearTransition(0.2f);
                mTarget.m_CurrentComponent = mTarget.Components[NarrationType.RightAvatarText];
                mTarget.m_CurrentComponent.OnEnter();
            }
            protected override void OnExit()
            {
                mTarget.Components[NarrationType.RightAvatarText].CanvasGroup.InverseLinearTransition(0.2f);
                mTarget.m_CurrentComponent.OnExit();
            }
        }

        public class FullScreenTextState : AbstractState<NarrationType, NarrationManager>
        {
            public FullScreenTextState(FSM<NarrationType> fsm, NarrationManager target) : base(fsm, target) { }
            protected override bool OnCondition() => mFSM.CurrentStateId is not NarrationType.FullScreenText;
            protected override void OnEnter()
            {
                mTarget.Components[NarrationType.FullScreenText].CanvasGroup.LinearTransition(0.2f);
                mTarget.m_CurrentComponent = mTarget.Components[NarrationType.FullScreenText];
                mTarget.m_CurrentComponent.OnEnter();
            }
            protected override void OnExit()
            {
                mTarget.Components[NarrationType.FullScreenText].CanvasGroup.InverseLinearTransition(0.2f);
                mTarget.m_CurrentComponent.OnExit();
            }
        }

        public class LeftAvatarOptionsState : AbstractState<NarrationType, NarrationManager>
        {
            public LeftAvatarOptionsState(FSM<NarrationType> fsm, NarrationManager target) : base(fsm, target) { }
            protected override bool OnCondition() => mFSM.CurrentStateId is not NarrationType.LeftAvatarOptions;
            protected override void OnEnter()
            {
                mTarget.Components[NarrationType.LeftAvatarOptions].CanvasGroup.LinearTransition(0.2f);
                mTarget.m_CurrentComponent = mTarget.Components[NarrationType.LeftAvatarOptions];
                mTarget.m_CurrentComponent.OnEnter();
            }
            protected override void OnExit()
            {
                mTarget.Components[NarrationType.LeftAvatarOptions].CanvasGroup.InverseLinearTransition(0.2f);
                mTarget.m_CurrentComponent.OnExit();
            }
        }

        public class FullScreenOptionsState : AbstractState<NarrationType, NarrationManager>
        {
            public FullScreenOptionsState(FSM<NarrationType> fsm, NarrationManager target) : base(fsm, target) { }
            protected override bool OnCondition() => mFSM.CurrentStateId is not NarrationType.FullScreenOptions;
            protected override void OnEnter()
            {
                mTarget.Components[NarrationType.FullScreenOptions].CanvasGroup.LinearTransition(0.2f);
                mTarget.m_CurrentComponent = mTarget.Components[NarrationType.FullScreenOptions];
                mTarget.m_CurrentComponent.OnEnter();
            }
            protected override void OnExit()
            {
                mTarget.Components[NarrationType.FullScreenOptions].CanvasGroup.InverseLinearTransition(0.2f);
                mTarget.m_CurrentComponent.OnExit();
            }
        }

        public class SubtitleTextState : AbstractState<NarrationType, NarrationManager>
        {
            public SubtitleTextState(FSM<NarrationType> fsm, NarrationManager target) : base(fsm, target) { }
            protected override bool OnCondition() => mFSM.CurrentStateId is not NarrationType.SubtitleText;
            protected override void OnEnter()
            {
                mTarget.Components[NarrationType.SubtitleText].CanvasGroup.LinearTransition(0.2f);
                mTarget.m_CurrentComponent = mTarget.Components[NarrationType.SubtitleText];
                mTarget.m_CurrentComponent.OnEnter();
            }
            protected override void OnExit()
            {
                mTarget.Components[NarrationType.SubtitleText].CanvasGroup.InverseLinearTransition(0.2f);
                mTarget.m_CurrentComponent.OnExit();
            }
        }

        public class SpecialState : AbstractState<NarrationType, NarrationManager>
        {
            public SpecialState(FSM<NarrationType> fsm, NarrationManager target) : base(fsm, target) { }
            protected override bool OnCondition() => mFSM.CurrentStateId is not NarrationType.Special;
            protected override void OnEnter()
            {
                mTarget.m_CurrentComponent = mTarget.Components[NarrationType.Special];
                mTarget.m_CurrentComponent.OnEnter();
            }
            protected override void OnExit()
            {
                mTarget.m_CurrentComponent.OnExit();
            }
        }
    }
}
