using System;
using Kuchinashi;
using Kuchinashi.Utils.Progressable;
using QFramework;
using UnityEditorInternal;
using UnityEngine;

namespace Phosphorescence.Game
{
    public enum MenuPanelState
    {
        None,
        MainMenu,
        Settings,
        Credits,
        Quit
    }

    public class MenuPanelController : MonoSingleton<MenuPanelController>
    {
        public ProgressableGroup ProgressableGroup;
        public FSM<MenuPanelState> StateMachine { get; } = new();

        public bool IsActivated => StateMachine.CurrentStateId is not MenuPanelState.None;

        public SerializableDictionary<MenuPanelState, Progressable> StateGroups;

        private void Awake()
        {
            StateMachine.AddState(MenuPanelState.None, new NoneState(StateMachine, this));
            StateMachine.AddState(MenuPanelState.MainMenu, new MainMenuState(StateMachine, this));
            StateMachine.AddState(MenuPanelState.Settings, new SettingsState(StateMachine, this));
            StateMachine.AddState(MenuPanelState.Credits, new CreditsState(StateMachine, this));

            StateMachine.StartState(MenuPanelState.None);
        }

        private void Update()
        {
            if (GameManager.Instance.pauseResumeAction.WasPressedThisFrame())
            {
                if (IsActivated)
                {
                    switch (StateMachine.CurrentStateId)
                    {
                        case MenuPanelState.MainMenu:
                            StateMachine.ChangeState(MenuPanelState.None);
                            break;
                        case MenuPanelState.Settings:
                            StateMachine.ChangeState(MenuPanelState.MainMenu);
                            break;
                        case MenuPanelState.Credits:
                            StateMachine.ChangeState(MenuPanelState.MainMenu);
                            break;
                    }
                }
                else StateMachine.ChangeState(MenuPanelState.MainMenu);
            }
        }

        public void ChangeState(string stateId)
        {
            if (Enum.TryParse(stateId, out MenuPanelState state))
            {
                StateMachine.ChangeState(state);
            }
            else
            {
                Debug.LogError($"Invalid state ID: {stateId}");
            }
        }

        public class NoneState : AbstractState<MenuPanelState, MenuPanelController>
        {
            public NoneState(FSM<MenuPanelState> fsm, MenuPanelController target) : base(fsm, target) { }
            protected override bool OnCondition() => mTarget.StateMachine.CurrentStateId is not MenuPanelState.None;
            protected override void OnEnter()
            {
                mTarget.ProgressableGroup.InverseLinearTransition(0.5f, 0.5f);

                GameManager.Instance.moveAction.Enable();
                GameManager.Instance.interactAction.Enable();
                GameManager.Instance.climbAction.Enable();
                GameManager.Instance.nextLineAction.Enable();
            }

            protected override void OnExit()
            {
                mTarget.ProgressableGroup.LinearTransition(0.5f);

                GameManager.Instance.moveAction.Disable();
                GameManager.Instance.interactAction.Disable();
                GameManager.Instance.climbAction.Disable();
                GameManager.Instance.nextLineAction.Disable();
            }
        }

        public class MainMenuState : AbstractState<MenuPanelState, MenuPanelController>
        {
            public MainMenuState(FSM<MenuPanelState> fsm, MenuPanelController target) : base(fsm, target) { }
            protected override bool OnCondition() => mTarget.StateMachine.CurrentStateId is not MenuPanelState.MainMenu;
            protected override void OnEnter()
            {
                mTarget.StateGroups[MenuPanelState.MainMenu].LinearTransition(0.5f, 0.5f);
            }
            protected override void OnExit()
            {
                mTarget.StateGroups[MenuPanelState.MainMenu].InverseLinearTransition(0.5f);
            }
        }

        public class SettingsState : AbstractState<MenuPanelState, MenuPanelController>
        {
            public SettingsState(FSM<MenuPanelState> fsm, MenuPanelController target) : base(fsm, target) { }
            protected override bool OnCondition() => mTarget.StateMachine.CurrentStateId is not MenuPanelState.Settings;
            protected override void OnEnter()
            {
                mTarget.StateGroups[MenuPanelState.Settings].LinearTransition(0.5f, 0.5f);
            }
            protected override void OnExit()
            {
                mTarget.StateGroups[MenuPanelState.Settings].InverseLinearTransition(0.5f);
            }
        }

        public class CreditsState : AbstractState<MenuPanelState, MenuPanelController>
        {
            public CreditsState(FSM<MenuPanelState> fsm, MenuPanelController target) : base(fsm, target) { }
            protected override bool OnCondition() => mTarget.StateMachine.CurrentStateId is not MenuPanelState.Credits;
            protected override void OnEnter()
            {
                mTarget.StateGroups[MenuPanelState.Credits].LinearTransition(0.5f, 0.5f);
            }
            protected override void OnExit()
            {
                mTarget.StateGroups[MenuPanelState.Credits].InverseLinearTransition(0.5f);
            }
        }
    }

}