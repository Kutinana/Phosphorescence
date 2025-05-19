using System.Collections.Generic;
using Kuchinashi.Utils.Progressable;
using Phosphorescence.DataSystem;
using Phosphorescence.Narration;
using QFramework;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Phosphorescence.Game
{
    public class RadioAnimationController : MonoSingleton<RadioAnimationController>
    {
        private SpriteRenderer spriteRenderer;
        private Progressable progressable;
        private Animator animator;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            progressable = GetComponent<Progressable>();
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            if (GameProgressData.Instance.CurrentPlotProgress != "")
            {
                gameObject.SetActive(false);
                return;
            }
            else
            {
                gameObject.SetActive(true);
                animator.enabled = false;
            }

            TypeEventSystem.Global.Register<OnStoryEventTriggerEvent>(e => {
                if (e.eventName == "WakeUpFromRadio") {
                    animator.enabled = true;
                }
            });

            TypeEventSystem.Global.Register<OnStoryEndEvent>(e => {
                if (e.plot.Id == "0.0") {
                    progressable.LinearTransition(1f);
                }
            });
        }
    }
}