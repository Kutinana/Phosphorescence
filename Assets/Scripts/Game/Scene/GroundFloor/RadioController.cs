using System.Collections.Generic;
using Phosphorescence.DataSystem;
using Phosphorescence.Narration;
using QFramework;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Phosphorescence.Game
{
    public class RadioController : Interactable
    {
        public Sprite defaultSprite;
        public Sprite onHoverSprite;
        public SpriteRenderer spriteRenderer;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            if (GameProgressData.Instance.CurrentPlotProgress == "")
            {
                IsInteractable = false;
            }
            TypeEventSystem.Global.Register<OnStoryEndEvent>(e => {
                if (e.plot.Id == "0.0") {
                    IsInteractable = true;
                }
            });

            InteractAction = new Dictionary<InputAction, System.Action> {
                { GameManager.Instance.interactAction, () => {
                    if (GameProgressData.Instance.CurrentPlotProgress == "0.0") {
                        GameManager.Instance.ContinuePlot("0.1");
                    }
                    else if (GameProgressData.Instance.CurrentPlotProgress == "0.9") {
                        GameManager.Instance.ContinuePlot("1.0");
                    }
                } }
            };
            HoverAction = () => {
                spriteRenderer.sprite = onHoverSprite;
            };
            UnhoverAction = () => {
                spriteRenderer.sprite = defaultSprite;
            };
        }
    }
}