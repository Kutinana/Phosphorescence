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
            if (GameProgressData.Instance.CompareInfoWith("IsKeyAppeared"))
            {
                if (GameProgressData.Instance.CompareInfoWith("IsKeyTaken"))
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    spriteRenderer.enabled = true;
                    animator.enabled = true;
                }
            }

            HoverAction = () => {
                spriteRenderer.sprite = HoverSprite;
                animator.enabled = false;
            };
            UnhoverAction = () => {
                spriteRenderer.sprite = NormalSprite;
                animator.enabled = true;
            };
            InteractAction = new Dictionary<InputAction, System.Action>() {
                {
                    GameManager.Instance.interactAction, () => {
                        BackpackManager.Instance.Obtain("bunker_key");
                        gameObject.SetActive(false);

                        GameProgressData.Instance.SetInfo("IsKeyTaken", "true");
                    }
                }
            };
        }

        void Update()
        {
            if (GameProgressData.Instance.CurrentPlotProgress == "3.1"
                && (PlayerController.Instance.transform.position.y < 10f || PlayerController.Instance.IsOutside)
                && !GameProgressData.Instance.HasInfo("IsKeyAppeared"))
            {
                StartCoroutine(ShowKeyCoroutine());
                GameProgressData.Instance.SetInfo("IsKeyAppeared", "true");
                GameProgressData.Instance.SetInfo("IsKeyTaken", "false");
            }
        }

        private IEnumerator ShowKeyCoroutine()
        {
            yield return new WaitForSeconds(1f);
            spriteRenderer.enabled = true;
            animator.enabled = true;
        }
    }
}
