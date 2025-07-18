using System.Collections.Generic;
using Phosphorescence.DataSystem;
using Phosphorescence.Narration;
using QFramework;
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

            TypeEventSystem.Global.Register<OnStoryEndEvent>(e => {
                if (e.plot != null && e.plot.Id == "2.0")
                {
                    IsInteractable = true;
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void Start()
        {
            if (GameProgressData.Instance.CurrentPlotProgress is "2.0" or "2.5")
            {
                IsInteractable = true;
            }
            else IsInteractable = false;

            if (GameProgressData.Instance.CompareInfoWith("IsCanTakenFromShelve"))
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
                        GameProgressData.Instance.SetInfo("IsCanTakenFromShelve", "true");
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