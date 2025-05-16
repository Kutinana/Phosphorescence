using System.Collections;
using System.Linq;
using Common.SceneControl;
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
        public InputAction upStairAction;
        public InputAction downStairAction;

        public InputAction nextLineAction;

        public InputAction pauseResumeAction;
        public InputAction navigationAction;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            moveAction = InputSystem.actions.FindAction("Move");
            interactAction = InputSystem.actions.FindAction("Interact");
            upStairAction = InputSystem.actions.FindAction("UpStair");
            downStairAction = InputSystem.actions.FindAction("DownStair");

            nextLineAction = InputSystem.actions.FindAction("NextLine");

            pauseResumeAction = InputSystem.actions.FindAction("PauseResume");
            navigationAction = InputSystem.actions.FindAction("Navigate");
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
                case "4.9":
                    return;
            }

            NarrationManager.Instance.StartNarration(targetPlotId);
        }

        public void ContinuePlot(string targetPlotId)
        {
            NarrationManager.Instance.StartNarration(targetPlotId);
        }
    }
}
