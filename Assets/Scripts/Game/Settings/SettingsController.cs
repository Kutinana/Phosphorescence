using System;
using Kuchinashi;
using Kuchinashi.Utils.Progressable;
using QFramework;
using UnityEngine;
using UnityEngine.UI;

namespace Phosphorescence.Game
{
    public enum SettingsState
    {
        Audio,
        Video,
        System,
    }

    public class SettingsController : MonoSingleton<SettingsController> , IController
    {
        private SettingsModel m_Model;

        public FSM<SettingsState> StateMachine { get; } = new();
        public SerializableDictionary<SettingsState, Toggle> StateToggles;
        public SerializableDictionary<SettingsState, Progressable> StateGroups;

        private void Start()
        {
            foreach (var state in StateToggles)
            {
                state.Value.onValueChanged.AddListener(e => {
                    if (e)
                    {
                        StateMachine.ChangeState(state.Key);
                    }
                });
            }

            foreach (var state in StateGroups)
            {
                state.Value.Progress = 0f;
            }

            StateMachine.AddState(SettingsState.Audio, new AudioState(StateMachine, this));
            StateMachine.AddState(SettingsState.Video, new VideoState(StateMachine, this));
            StateMachine.AddState(SettingsState.System, new SystemState(StateMachine, this));
            StateMachine.StartState(SettingsState.Audio);
        }

        private void Update()
        {
            if (MenuPanelController.Instance.StateMachine.CurrentStateId != MenuPanelState.Settings) return;

            var uiNavigation = GameManager.Instance.navigationAction.ReadValue<Vector2>();
            if (GameManager.Instance.navigationAction.WasPressedThisFrame() && uiNavigation.x < 0)
            {
                PreviousState();
            }
            else if (GameManager.Instance.navigationAction.WasPressedThisFrame() && uiNavigation.x > 0)
            {
                NextState();
            }
        }

        private void NextState()
        {
            if ((int) StateMachine.CurrentStateId == StateToggles.Count - 1) StateToggles[(SettingsState) 0].isOn = true;
            else StateToggles[StateMachine.CurrentStateId + 1].isOn = true;
        }

        private void PreviousState()
        {
            if ((int) StateMachine.CurrentStateId == 0) StateToggles[(SettingsState) (StateToggles.Count - 1)].isOn = true;
            else StateToggles[StateMachine.CurrentStateId - 1].isOn = true;
        }

        public class AudioState : AbstractState<SettingsState, SettingsController>
        {
            public AudioState(FSM<SettingsState> fsm, SettingsController target) : base(fsm, target) { }
            protected override bool OnCondition() => mTarget.StateMachine.CurrentStateId is not SettingsState.Audio;
            protected override void OnEnter()
            {
                mTarget.StateGroups[SettingsState.Audio].LinearTransition(0.2f);
            }
            protected override void OnExit()
            {
                mTarget.StateGroups[SettingsState.Audio].InverseLinearTransition(0.2f);
            }
            private void UpdateView()
            {
                
            }
        }
        
        public class VideoState : AbstractState<SettingsState, SettingsController>
        {
            public VideoState(FSM<SettingsState> fsm, SettingsController target) : base(fsm, target) { }
            protected override bool OnCondition() => mTarget.StateMachine.CurrentStateId is not SettingsState.Video;
            protected override void OnEnter()
            {
                mTarget.StateGroups[SettingsState.Video].LinearTransition(0.2f);
            }
            protected override void OnExit()
            {
                mTarget.StateGroups[SettingsState.Video].InverseLinearTransition(0.2f);
            }
        }

        public class SystemState : AbstractState<SettingsState, SettingsController>
        {
            public SystemState(FSM<SettingsState> fsm, SettingsController target) : base(fsm, target) { }
            protected override bool OnCondition() => mTarget.StateMachine.CurrentStateId is not SettingsState.System;
            protected override void OnEnter()
            {
                mTarget.StateGroups[SettingsState.System].LinearTransition(0.2f);
            }
            protected override void OnExit()
            {
                mTarget.StateGroups[SettingsState.System].InverseLinearTransition(0.2f);
            }
        }
        public IArchitecture GetArchitecture()
        {
            return SettingsArchitecture.Interface;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            m_Model = null;
        }
    }
}
