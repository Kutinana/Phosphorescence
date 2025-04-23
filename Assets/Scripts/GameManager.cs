using System.Linq;
using Phosphorescence.DataSystem;
using Phosphorescence.Narration;
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

        public void ContinuePlot()
        {
            var targetPlotId = "0.0";
            switch (GameProgressData.Instance.CurrentPlotProgress)
            {
                case "0.0":
                    targetPlotId = "0.1";
                    break;
                case "0.1":
                    targetPlotId = "0.5";
                    break;
                default:
                    Debug.LogError($"Plot {GameProgressData.Instance.CurrentPlotProgress} not found");
                    break;
            }

            NarrationManager.Instance.StartNarration(targetPlotId);
        }
    }
}
