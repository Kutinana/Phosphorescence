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
            if (!GameProgressData.Instance.IsPlotFinished("0.5") || GameProgressData.Instance.CurrentPlotProgress == "4.5")
            {
                gameObject.SetActive(true);
                animator.enabled = false;
            }
            else
            {
                gameObject.SetActive(false);
                return;
            }

            TypeEventSystem.Global.Register<OnStoryEventTriggerEvent>(e => {
                if (e.eventName == "WakeUpFromRadio") {
                    animator.enabled = true;
                    animator.Play("Sleeping_on_radio_desk");
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            TypeEventSystem.Global.Register<OnStoryEndEvent>(e => {
                if (e.plot.Id is "0.0" or "4.9") {
                    progressable.LinearTransition(1f);
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
    }
}