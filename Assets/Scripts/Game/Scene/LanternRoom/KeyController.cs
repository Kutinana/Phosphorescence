using System.Collections;
using System.Collections.Generic;
using Phosphorescence.Audio;
using Phosphorescence.DataSystem;
using Phosphorescence.Narration;
using QFramework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

namespace Phosphorescence.Game
{
    public class KeyController : Interactable
    {
        public SpriteRenderer spriteRenderer;
        public Animator animator;

        public Sprite NormalSprite;
        public Sprite HoverSprite;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            HoverAction = () => {
                spriteRenderer.sprite = HoverSprite;
                animator.enabled = false;
            };
            UnhoverAction = () => {
                spriteRenderer.sprite = NormalSprite;
                animator.enabled = true;
            };
            InteractAction = new Dictionary<InputAction, System.Action>() {
                { GameManager.Instance.interactAction, () => {
                    BackpackManager.Instance.Obtain("bunker_key");
                    gameObject.SetActive(false);
                } }
            };
        }

        void Update()
        {
            if (GameProgressData.Instance.CurrentPlotProgress == "3.1" && PlayerController.Instance.transform.position.y < 11f)
            {
                spriteRenderer.enabled = true;
                animator.enabled = true;
            }
        }
    }
}
