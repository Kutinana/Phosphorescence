using System.Collections.Generic;
using Phosphorescence.DataSystem;
using Phosphorescence.Narration;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Phosphorescence.Game
{
    public class CanShelveController : Interactable
    {
        public bool isTaken = false;

        public Sprite normalSprite;
        public Sprite normalOnHoverSprite;
        public Sprite takenSprite;

        private SpriteRenderer spriteRenderer;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            if (GameProgressData.Instance.CurrentPlotProgress is "2.0" or "2.5")
            {
                IsInteractable = true;
            }
            else IsInteractable = false;


            if (GameProgressData.Instance.GetState("CanShelveTaken"))
            {
                spriteRenderer.sprite = takenSprite;
                isTaken = true;
                IsInteractable = false;
            }

            InteractAction = new Dictionary<InputAction, System.Action> {
                { GameManager.Instance.interactAction, () => {
                    if (!isTaken && BackpackManager.IsEmpty)
                    {
                        spriteRenderer.sprite = takenSprite;
                        isTaken = true;

                        BackpackManager.Instance.Obtain("can");
                        GameProgressData.Instance.SetState("CanShelveTaken", true);
                    }
                } }
            };
            HoverAction = () => {
                spriteRenderer.sprite = isTaken ? takenSprite : normalOnHoverSprite;
            };
            UnhoverAction = () => {
                spriteRenderer.sprite = isTaken ? takenSprite : normalSprite;
            };
        }
    }
}