using Phosphorescence.DataSystem;
using UnityEngine;

namespace Phosphorescence.Game
{
    public class EnteringLanternRoomDetector : Interactable
    {
        void Start()
        {
            HoverAction = () => {
                if (GameProgressData.Instance.CurrentPlotProgress == "2.9")
                {
                    GameManager.Instance.ContinuePlot("3.0");
                    PlayerController.Instance.StopMoving(-1);
                }
                else if (GameProgressData.Instance.CurrentPlotProgress == "3.1"
                    && GameProgressData.Instance.CompareInfoWith("IsKeyAppeared", "true"))
                {
                    GameManager.Instance.ContinuePlot("3.2");
                    PlayerController.Instance.StopMoving(-1);
                }
                else if (GameProgressData.Instance.CurrentPlotProgress == "3.2" && GeneratorController.Instance.isActivated)
                {
                    GameManager.Instance.ContinuePlot("4.0");
                    PlayerController.Instance.StopMoving(-1);
                }
            };
        }

        void Update()
        {
            
        }
    }

}