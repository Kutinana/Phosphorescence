using System.Collections.Generic;
using Phosphorescence.Audio;
using Phosphorescence.DataSystem;
using Phosphorescence.Narration;
using QFramework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

namespace Phosphorescence.Game
{
    public class BasementShelfController : Interactable
    {
        public bool isTaken = false;

        public Sprite defaultSprite;
        public Sprite onHoverSprite;
        public Sprite takenSprite;

        private SpriteRenderer spriteRenderer;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            if (GameProgressData.Instance.CompareInfoWith("BasementShelfTaken"))
            {
                spriteRenderer.sprite = takenSprite;
                isTaken = true;
                IsInteractable = false;
            }
            else
            {
                spriteRenderer.sprite = defaultSprite;
                isTaken = false;
                IsInteractable = true;
            }

            InteractAction = new Dictionary<InputAction, System.Action> {
                { GameManager.Instance.interactAction, () => {
                    if (!isTaken && BackpackManager.IsEmpty)
                    {
                        spriteRenderer.sprite = takenSprite;
                        isTaken = true;

                        BackpackManager.Instance.Obtain("diesel");
                        GameProgressData.Instance.SetInfo("BasementShelfTaken", "true");
                    }
                } }
            };
            HoverAction = () => {
                spriteRenderer.sprite = isTaken ? takenSprite : onHoverSprite;
            };
            UnhoverAction = () => {
                spriteRenderer.sprite = isTaken ? takenSprite : defaultSprite;
            };
        }
    }
}