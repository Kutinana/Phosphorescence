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
                    else if (GameProgressData.Instance.CurrentPlotProgress == "1.0") {
                        GameManager.Instance.ContinuePlot("1.1");
                    }
                    else if (GameProgressData.Instance.CurrentPlotProgress == "1.1") {
                        GameManager.Instance.ContinuePlot("1.2");
                    }
                    else if (GameProgressData.Instance.CurrentPlotProgress == "1.2") {
                        GameManager.Instance.ContinuePlot("1.3");
                    }
                    else if (GameProgressData.Instance.CurrentPlotProgress == "1.3") {
                        GameManager.Instance.ContinuePlot("2.0");
                    }
                    else if (GameProgressData.Instance.CurrentPlotProgress == "2.0") {
                        GameManager.Instance.ContinuePlot("2.5");
                    }
                    else if (GameProgressData.Instance.CurrentPlotProgress == "2.5") {
                        if (!GameProgressData.Instance.GetPlotState("FinishedCanExperiment"))
                        {
                            GameManager.Instance.ContinuePlot("2.5");
                        }
                        else if (GameManager.Instance.Timer < 20f)
                        {
                            GameManager.Instance.ContinuePlot("2.6");
                        }
                        else
                        {
                            GameManager.Instance.ContinuePlot("2.9");
                        }
                    }
                    else if (GameProgressData.Instance.CurrentPlotProgress == "2.6") {
                        if (GameManager.Instance.Timer < 20f)
                        {
                            GameManager.Instance.ContinuePlot("2.6");
                        }
                        else
                        {
                            GameManager.Instance.ContinuePlot("2.9");
                        }
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