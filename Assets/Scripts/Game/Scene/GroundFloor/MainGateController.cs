using System.Collections.Generic;
using Phosphorescence.DataSystem;
using Phosphorescence.Narration;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Phosphorescence.Game
{
    public class MainGateController : Interactable
    {
        public bool isOpen = false;

        public Sprite closedSprite;
        public Sprite closedOnHoverSprite;
        public Sprite openedSprite;
        public Sprite openedOnHoverSprite;

        private SpriteRenderer spriteRenderer;
        public Collider2D collider;

        public UnityEvent onOpenInteract;
        public UnityEvent onCloseInteract;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            InteractAction = new Dictionary<InputAction, System.Action> {
                { GameManager.Instance.interactAction, () => {
                    if (isOpen)  // Now is open, so close it
                    {
                        spriteRenderer.sprite = closedOnHoverSprite;
                        collider.enabled = true;
                        isOpen = false;
                    }
                    else
                    {
                        spriteRenderer.sprite = openedOnHoverSprite;
                        collider.enabled = false;
                        isOpen = true;
                    }
                } }
            };
            HoverAction = () => {
                spriteRenderer.sprite = isOpen ? openedOnHoverSprite : closedOnHoverSprite;
            };
            UnhoverAction = () => {
                spriteRenderer.sprite = isOpen ? openedSprite : closedSprite;
            };
        }
    }
}