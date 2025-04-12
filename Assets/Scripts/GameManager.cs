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

        public InputAction nextLineAction;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            moveAction = InputSystem.actions.FindAction("Move");
            interactAction = InputSystem.actions.FindAction("Interact");
            climbAction = InputSystem.actions.FindAction("Climb");

            nextLineAction = InputSystem.actions.FindAction("NextLine");
        }

        private void Update()
        {
            if (interactAction.WasPressedThisFrame()) PlayerController.Instance.Interact();
        }
    }
}
