using System.Collections.Generic;
using System.Linq;
using Phosphorescence.DataSystem;
using Phosphorescence.Narration;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Phosphorescence.Game
{
    public class OperatorController : Interactable
    {
        public Sprite defaultSprite;
        public Sprite onHoverSprite;

        private SpriteRenderer spriteRenderer;

        private bool _isInteracted = false;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            InteractAction = new Dictionary<InputAction, System.Action> {
                { GameManager.Instance.interactAction, () => {
                    if (_isInteracted) return;

                    if (BeaconController.Instance.IsOn)
                    {
                        BeaconController.Instance.Stop();
                        spriteRenderer.sprite = defaultSprite;
                    }
                    else
                    {
                        BeaconController.Instance.Play();
                        spriteRenderer.sprite = defaultSprite;

                        if (GameProgressData.Instance.CurrentPlotProgress == "0.5")
                        {
                            AfterOpeningDirector.Instance.Play();
                        }
                    }

                    _isInteracted = true;
                } }
            };
            HoverAction = () => {
                spriteRenderer.sprite = onHoverSprite;
            };
            UnhoverAction = () => {
                spriteRenderer.sprite = defaultSprite;
                _isInteracted = false;
            };
        }
    }
}