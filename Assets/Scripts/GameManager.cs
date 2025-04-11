using QFramework;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Phosphorescence.Game
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public InputAction moveAction;
        public InputAction interactAction;
        public InputAction climbAction;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            moveAction = InputSystem.actions.FindAction("Move");
            interactAction = InputSystem.actions.FindAction("Interact");
            climbAction = InputSystem.actions.FindAction("Climb");
        }

        private void Update()
        {
            if (interactAction.WasPressedThisFrame()) PlayerController.Instance.Interact();
        }
    }
}
