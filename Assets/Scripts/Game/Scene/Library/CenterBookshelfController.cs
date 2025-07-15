using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Phosphorescence.Game
{
    public class CenterBookshelfController : Interactable
    {
        public Sprite normalSprite;
        public Sprite onHoverSprite;

        private SpriteRenderer spriteRenderer;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            InteractAction = new Dictionary<InputAction, System.Action> {
                { GameManager.Instance.interactAction, () => {
                    Debug.Log("Interact with center bookshelf");
                    spriteRenderer.sprite = normalSprite;
                } }
            };
            HoverAction = () => {
                spriteRenderer.sprite = onHoverSprite;
            };
            UnhoverAction = () => {
                spriteRenderer.sprite = normalSprite;
            };
        }
    }

}