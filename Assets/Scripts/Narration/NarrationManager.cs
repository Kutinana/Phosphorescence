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
        LeftAvatarText
    }

    public partial class NarrationManager : MonoSingleton<NarrationManager>
    {
        public FSM<NarrationType> StateMachine => Instance._stateMachine ??= new();
        private FSM<NarrationType> _stateMachine;

        public CanvasGroupAlphaProgressable CanvasGroup;

        public SerializableDictionary<NarrationType, NarrationComponent> NarrationControllers = new();

        private void Awake()
        {
            CanvasGroup = GetComponent<CanvasGroupAlphaProgressable>();
            CanvasGroup.Progress = 0f;

            StateMachine.AddState(NarrationType.None, new NoneState(StateMachine, this));
            StateMachine.AddState(NarrationType.LeftAvatarText, new LeftAvatarTextState(StateMachine, this));
            StateMachine.StartState(NarrationType.None);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                StateMachine.ChangeState(NarrationType.LeftAvatarText);
                NarrationControllers[StateMachine.CurrentStateId].SetNarration("sample_tachi_e", "Test Text");
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                StateMachine.ChangeState(NarrationType.None);
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
                mTarget.NarrationControllers[NarrationType.LeftAvatarText].CanvasGroup.LinearTransition(0.2f);
            }
            protected override void OnExit()
            {
                mTarget.NarrationControllers[NarrationType.LeftAvatarText].CanvasGroup.InverseLinearTransition(0.2f);
            }
        }
    }
}
