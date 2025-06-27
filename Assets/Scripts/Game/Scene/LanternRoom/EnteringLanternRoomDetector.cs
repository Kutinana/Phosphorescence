using System;
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
                else if (GameProgressData.Instance.CurrentPlotProgress == "3.0")
                {
                    var sendKeyAt = GameProgressData.Instance.GetInfo("HakumeiSendKey");
                    var sendKeyAtDateTime = DateTime.Parse(sendKeyAt);
                    var timeSpan = DateTime.Now - sendKeyAtDateTime;

                    if (timeSpan.TotalSeconds < 20)
                    {
                        GameManager.Instance.ContinuePlot("3.1");
                        PlayerController.Instance.StopMoving(-1);
                    }
                }
                else if (GameProgressData.Instance.CurrentPlotProgress == "3.1"
                    && GameProgressData.Instance.CompareInfoWith("IsKeyAppeared", "true"))
                {
                    GameManager.Instance.ContinuePlot("3.2");
                    PlayerController.Instance.StopMoving(-1);
                }
                else if (GameProgressData.Instance.CurrentPlotProgress == "3.2"
                    && GameManager.Instance.GlobalPower)
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