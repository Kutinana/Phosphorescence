using System;
using System.Collections;
using System.Collections.Generic;
using Kuchinashi;
using Kuchinashi.Utils.Progressable;
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
        RightAvatarOptions
    }

    public partial class NarrationManager : MonoSingleton<NarrationManager>
    {
        public FSM<NarrationType> StateMachine => Instance._stateMachine ??= new();
        private FSM<NarrationType> _stateMachine;

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
            StateMachine.StartState(NarrationType.None);

            TypeEventSystem.Global.Register<OnLineReadEvent>(e => {
                OnLineReceived(e);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            TypeEventSystem.Global.Register<OnLinesReadEvent>(e => {
                OnLinesReceived(e);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            TypeEventSystem.Global.Register<OnStoryEndEvent>(e => {
                StateMachine.ChangeState(NarrationType.None);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void OnLineReceived(OnLineReadEvent e)
        {
            if (string.IsNullOrEmpty(e.content))  // Situation for tag-only line
            {
                TypeEventSystem.Global.Send<RequestNewLineEvent>();
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
                component.Process(e);
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

            StateMachine.ChangeState(type);
            if (Components.TryGetValue(type, out var controller) && controller.TryGetComponent<ICanProcessLines>(out var component))
            {
                component.Process(e);
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
                mTarget.CanvasGroup.InverseLinearTransition(0.2f);
            }
            protected override void OnExit()
            {
                mTarget.CanvasGroup.LinearTransition(0.2f);
            }
        }

        public class LeftAvatarTextState : AbstractState<NarrationType, NarrationManager>
        {
            public LeftAvatarTextState(FSM<NarrationType> fsm, NarrationManager target) : base(fsm, target) { }
            protected override bool OnCondition() => mFSM.CurrentStateId is not NarrationType.LeftAvatarText;
            protected override void OnEnter()
            {
                mTarget.Components[NarrationType.LeftAvatarText].CanvasGroup.LinearTransition(0.2f);
            }
            protected override void OnExit()
            {
                mTarget.Components[NarrationType.LeftAvatarText].CanvasGroup.InverseLinearTransition(0.2f);
            }
        }

        public class RightAvatarTextState : AbstractState<NarrationType, NarrationManager>
        {
            public RightAvatarTextState(FSM<NarrationType> fsm, NarrationManager target) : base(fsm, target) { }
            protected override bool OnCondition() => mFSM.CurrentStateId is not NarrationType.RightAvatarText;
            protected override void OnEnter()
            {
                mTarget.Components[NarrationType.RightAvatarText].CanvasGroup.LinearTransition(0.2f);
            }
            protected override void OnExit()
            {
                mTarget.Components[NarrationType.RightAvatarText].CanvasGroup.InverseLinearTransition(0.2f);
            }
        }

        public class FullScreenTextState : AbstractState<NarrationType, NarrationManager>
        {
            public FullScreenTextState(FSM<NarrationType> fsm, NarrationManager target) : base(fsm, target) { }
            protected override bool OnCondition() => mFSM.CurrentStateId is not NarrationType.FullScreenText;
            protected override void OnEnter()
            {
                mTarget.Components[NarrationType.FullScreenText].CanvasGroup.LinearTransition(0.2f);
            }
            protected override void OnExit()
            {
                mTarget.Components[NarrationType.FullScreenText].CanvasGroup.InverseLinearTransition(0.2f);
            }
        }

        public class LeftAvatarOptionsState : AbstractState<NarrationType, NarrationManager>
        {
            public LeftAvatarOptionsState(FSM<NarrationType> fsm, NarrationManager target) : base(fsm, target) { }
            protected override bool OnCondition() => mFSM.CurrentStateId is not NarrationType.LeftAvatarOptions;
            protected override void OnEnter()
            {
                mTarget.Components[NarrationType.LeftAvatarOptions].CanvasGroup.LinearTransition(0.2f);
            }
            protected override void OnExit()
            {
                mTarget.Components[NarrationType.LeftAvatarOptions].CanvasGroup.InverseLinearTransition(0.2f);
            }
        }
    }
}
