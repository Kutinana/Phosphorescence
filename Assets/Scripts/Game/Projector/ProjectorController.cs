using Kuchinashi.Utils.Progressable;
using Phosphorescence.DataSystem;
using Phosphorescence.Narration;
using QFramework;
using UnityEngine;

namespace Phosphorescence.Game
{
    public enum ProjectorState
    {
        Hide,
        On,
        Off,
        Standby
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
                else if (e.eventName == "projector_off")
                {
                    StateMachine.ChangeState(ProjectorState.Off);
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
            if (GameProgressData.Instance.CurrentPlotProgress == "3.1")
            {
                StateMachine.ChangeState(ProjectorState.On);
            }
            else if (float.Parse(GameProgressData.Instance.CurrentPlotProgress) >= 2.9f)
            {
                StateMachine.ChangeState(ProjectorState.Standby);
            }
            else
            {
                StateMachine.ChangeState(ProjectorState.Hide);
            }
        }

        void Update()
        {
            if (GameProgressData.Instance.CurrentPlotProgress is "3.0" or "3.05" && GameManager.Instance.Timer > 30f)
            {
                GameManager.Instance.StopTimer();
                GameManager.Instance.ContinuePlot("3.1");
            }
        }

        public class HideState : AbstractState<ProjectorState, ProjectorController>
        {
            public HideState(FSM<ProjectorState> fsm, ProjectorController target) : base(fsm, target) { }
            protected override bool OnCondition() => mFSM.CurrentStateId is not ProjectorState.Hide;
            protected override void OnEnter()
            {
                mTarget.spriteRenderer.enabled = false;
                mTarget.animator.enabled = false;
            }
        }

        public class OnState : AbstractState<ProjectorState, ProjectorController>
        {
            public OnState(FSM<ProjectorState> fsm, ProjectorController target) : base(fsm, target) { }
            protected override bool OnCondition() => mFSM.CurrentStateId is not ProjectorState.On;
            protected override void OnEnter()
            {
                mTarget.spriteRenderer.enabled = true;
                mTarget.animator.enabled = true;
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
                mTarget.animator.enabled = true;
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
            }
        }
        
        
    }
}
