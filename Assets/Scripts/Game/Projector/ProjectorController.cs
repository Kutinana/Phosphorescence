using Kuchinashi.Utils.Progressable;
using Phosphorescence.DataSystem;
using Phosphorescence.Narration;
using QFramework;
using UnityEngine;

namespace Phosphorescence.Game
{
    public enum ProjectorState
    {
        Hide,    // Invisible in the scene
        TransitToOff,
        Off,     // Lying on the ground
        TransitToOn,
        On,
        TransitToStandby,
        Standby  // Floating without hologram
    }
    
    public class ProjectorController : MonoSingleton<ProjectorController>
    {
        public FSM<ProjectorState> StateMachine = new FSM<ProjectorState>();

        private SpriteRenderer spriteRenderer;
        private Animator animator;
        private Progressable progressable;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            progressable = GetComponent<Progressable>();

            StateMachine.AddState(ProjectorState.TransitToOff, new TransitToOffState(StateMachine, this));
            StateMachine.AddState(ProjectorState.TransitToOn, new TransitToOnState(StateMachine, this));
            // StateMachine.AddState(ProjectorState.TransitToStandby, new TransitToStandbyState(StateMachine, this));
            StateMachine.AddState(ProjectorState.Hide, new HideState(StateMachine, this));
            StateMachine.AddState(ProjectorState.On, new OnState(StateMachine, this));
            StateMachine.AddState(ProjectorState.Off, new OffState(StateMachine, this));
            StateMachine.AddState(ProjectorState.Standby, new StandbyState(StateMachine, this));

            StateMachine.StartState(ProjectorState.Hide);

            TypeEventSystem.Global.Register<OnStoryEventTriggerEvent>(e => {
                if (e.eventName == "projector_on")
                {
                    StateMachine.ChangeState(ProjectorState.On);
                }
                else if (e.eventName == "projector_transit_to_on")
                {
                    StateMachine.ChangeState(ProjectorState.TransitToOn);
                }
                else if (e.eventName == "projector_off")
                {
                    StateMachine.ChangeState(ProjectorState.Off);
                }
                else if (e.eventName == "projector_transit_to_off")
                {
                    StateMachine.ChangeState(ProjectorState.TransitToOff);
                }
                else if (e.eventName == "projector_standby")
                {
                    StateMachine.ChangeState(ProjectorState.Standby);
                }
                else if (e.eventName == "projector_transit_to_standby")
                {
                    StateMachine.ChangeState(ProjectorState.TransitToStandby);
                }
                else if (e.eventName == "grab_the_projector") {
                    progressable.InverseLinearTransition(0.2f);
                }
                else if (e.eventName == "grab_the_projector_finished") {
                    progressable.LinearTransition(0.2f);
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        void Start()
        {
            switch (GameProgressData.Instance.CurrentPlotProgress)
            {
                case "4.0":
                    StateMachine.ChangeState(ProjectorState.On);
                    break;
                case "3.2":
                    StateMachine.ChangeState(ProjectorState.Off);
                    break;
                case "3.1":
                    StateMachine.ChangeState(ProjectorState.On);
                    break;
                case "3.0":
                    StateMachine.ChangeState(ProjectorState.On);
                    break;
                case "2.9":
                    StateMachine.ChangeState(ProjectorState.Off);
                    break;
                default:
                    StateMachine.ChangeState(ProjectorState.Hide);
                    break;
            }
        }

        void Update()
        {
            if (GameProgressData.Instance.CurrentPlotProgress is "3.0" && GameManager.Instance.Timer > 30f)
            {
                GameManager.Instance.StopTimer();
                GameManager.Instance.ContinuePlot("3.1");
            }

            if (GameProgressData.Instance.CurrentPlotProgress == "4.0" && GameManager.Instance.Timer > 5f)
            {
                GameManager.Instance.StopTimer();
                GameManager.Instance.ContinuePlot("4.5");
            }
        }

        public void PlayActivateSound()
        {
            Audio.AudioManager.PlaySFX("projector_activate");
        }

        public class HideState : AbstractState<ProjectorState, ProjectorController>
        {
            public HideState(FSM<ProjectorState> fsm, ProjectorController target) : base(fsm, target) { }
            protected override bool OnCondition() => mFSM.CurrentStateId is not ProjectorState.Hide;
            protected override void OnEnter()
            {
                mTarget.spriteRenderer.enabled = false;
                mTarget.animator.Play("Off");
            }
        }

        public class TransitToOffState : AbstractState<ProjectorState, ProjectorController>
        {
            public TransitToOffState(FSM<ProjectorState> fsm, ProjectorController target) : base(fsm, target) { }
            protected override bool OnCondition() => mFSM.CurrentStateId is not ProjectorState.TransitToOff;
            protected override void OnEnter()
            {
                mTarget.spriteRenderer.enabled = true;
                mTarget.animator.SetTrigger("Off");
            }
        }

        public class TransitToOnState : AbstractState<ProjectorState, ProjectorController>
        {
            public TransitToOnState(FSM<ProjectorState> fsm, ProjectorController target) : base(fsm, target) { }
            protected override bool OnCondition() => mFSM.CurrentStateId is not ProjectorState.TransitToOn;
            protected override void OnEnter()
            {
                mTarget.spriteRenderer.enabled = true;
                mTarget.animator.SetTrigger("On");
            }
        }

        public class OnState : AbstractState<ProjectorState, ProjectorController>
        {
            public OnState(FSM<ProjectorState> fsm, ProjectorController target) : base(fsm, target) { }
            protected override bool OnCondition() => mFSM.CurrentStateId is not ProjectorState.On;
            protected override void OnEnter()
            {
                mTarget.spriteRenderer.enabled = true;
                mTarget.animator.Play("On");
            }
        }

        public class OffState : AbstractState<ProjectorState, ProjectorController>
        {
            public OffState(FSM<ProjectorState> fsm, ProjectorController target) : base(fsm, target) { }
            protected override bool OnCondition() => mFSM.CurrentStateId is not ProjectorState.Off;
            protected override void OnEnter()
            {
                mTarget.spriteRenderer.enabled = true;
                mTarget.animator.Play("Off");
            }
        }

        public class StandbyState : AbstractState<ProjectorState, ProjectorController>
        {
            public StandbyState(FSM<ProjectorState> fsm, ProjectorController target) : base(fsm, target) { }
            protected override bool OnCondition() => mFSM.CurrentStateId is not ProjectorState.Standby;
            protected override void OnEnter()
            {
                mTarget.spriteRenderer.enabled = true;
                mTarget.animator.Play("Standby");
            }
        }
        
        
    }
}
