using Phosphorescence.DataSystem;
using Phosphorescence.Narration;
using QFramework;
using UnityEngine;

namespace Phosphorescence.Game
{
    public class VirtualDoorController : Interactable
    {
        public Collider2D Collider;

        void Start()
        {
            Collider = GetComponent<Collider2D>();

            HoverAction = () => {
                if (GameProgressData.Instance.CurrentPlotProgress == "3.0")
                {
                    Collider.isTrigger = true;
                    GameManager.Instance.ContinuePlot("wait_for_key");
                    PlayerController.Instance.StopMoving(1);
                }
            };

            TypeEventSystem.Global.Register<OnStoryEndEvent>(e => {
                if (e.plot.Id == "wait_for_key")
                {
                    Collider.isTrigger = false;
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        void Update()
        {
            
        }
    }

}