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
        public FSM<ProjectorState> StateMachine;

        public SpriteRenderer spriteRenderer;
        
    }
}
