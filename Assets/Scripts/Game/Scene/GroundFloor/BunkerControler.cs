using System.Collections.Generic;
using Phosphorescence.DataSystem;
using Phosphorescence.Narration;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Phosphorescence.Game
{
    public class BunkerController : Interactable
    {
        public bool isUnlocked = false;

        public Sprite defaultSprite;
        public Sprite onHoverSprite;
        public Sprite unlockedSprite;
        public Sprite unlockedOnHoverSprite;

        private SpriteRenderer spriteRenderer;

        public UnityEvent onUnlockedInteract;
        public UnityEvent onLockedInteract;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            if (GameProgressData.Instance.CompareInfoWith("bunker_unlocked"))
            {
                isUnlocked = true;
                spriteRenderer.sprite = unlockedSprite;
            }
            else
            {
                spriteRenderer.sprite = defaultSprite;
                isUnlocked = false;
            }

            InteractAction = new Dictionary<InputAction, System.Action> {
                { GameManager.Instance.interactAction, () => {
                    if (isUnlocked)
                    {
                        onUnlockedInteract.Invoke();
                    }
                    else
                    {
                        if (GameProgressData.Instance.CurrentObject == "bunker_key")
                        {
                            isUnlocked = true;
                            spriteRenderer.sprite = unlockedOnHoverSprite;

                            GameProgressData.Instance.SetInfo("bunker_unlocked", "true");
                            BackpackManager.Instance.Clear();
                        }
                        else
                        {
                            GameManager.Instance.ContinuePlot("bunker_locked");
                        }
                    }
                } }
            };
            HoverAction = () => {
                spriteRenderer.sprite = isUnlocked ? unlockedOnHoverSprite : onHoverSprite;
            };
            UnhoverAction = () => {
                spriteRenderer.sprite = isUnlocked ? unlockedSprite : defaultSprite;
            };
        }
    }
}