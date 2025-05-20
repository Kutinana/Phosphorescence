using Phosphorescence.DataSystem;
using UnityEngine;

namespace Phosphorescence.Game
{
    public class EnteringLanternRoomDetector : Interactable
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            HoverAction = () => {
                if (GameProgressData.Instance.CurrentPlotProgress == "2.9")
                {
                    GameManager.Instance.ContinuePlot("3.0");
                    PlayerController.Instance.StopMoving(-1);
                }
            };
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }

}