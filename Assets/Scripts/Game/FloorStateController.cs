using UnityEngine;
using QFramework;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace Phosphorescence.Game
{
    public class FloorStateController : MonoBehaviour
    {
        public enum FloorState
        {
            Active,
            Paused,
        }

        public FSM<FloorState> StateMachine { get; private set; } = new();
        public int SceneIndex;
        public string SceneName;

        public List<Animator> Animators = new();
        public List<Light2D> Lights = new();
        public List<AudioSource> AudioSources = new();
        public GameObject Mask;

        public void Awake()
        {
            StateMachine.AddState(FloorState.Active, new ActiveState(StateMachine, this));
            StateMachine.AddState(FloorState.Paused, new PausedState(StateMachine, this));
            StateMachine.StartState(FloorState.Paused);
        }

        public class ActiveState : AbstractState<FloorState, FloorStateController>
        {
            public ActiveState(FSM<FloorState> fsm, FloorStateController target) : base(fsm, target) { }
            protected override bool OnCondition() => mFSM.CurrentStateId is not FloorState.Active;
            protected override void OnEnter()
            {
                foreach (var animator in mTarget.Animators)
                {
                    animator.enabled = true;
                }
                foreach (var light in mTarget.Lights)
                {
                    light.enabled = true;
                }

                mTarget.Mask.SetActive(false);
            }
        }

        public class PausedState : AbstractState<FloorState, FloorStateController>
        {
            public PausedState(FSM<FloorState> fsm, FloorStateController target) : base(fsm, target) { }
            protected override bool OnCondition() => mFSM.CurrentStateId is not FloorState.Paused;
            protected override void OnEnter()
            {
                foreach (var animator in mTarget.Animators)
                {
                    animator.enabled = false;
                }
                foreach (var light in mTarget.Lights)
                {
                    light.enabled = false;
                }

                mTarget.Mask.SetActive(true);
            }
        }
    }

}