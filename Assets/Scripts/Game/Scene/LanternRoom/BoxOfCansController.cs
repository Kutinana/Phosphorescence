using System;
using System.Collections;
using Phosphorescence.Audio;
using Phosphorescence.DataSystem;
using Phosphorescence.Narration;
using QFramework;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Phosphorescence.Game
{
    /// <summary>
    /// CanBox Object put in the lantern room for displaying.
    /// </summary>
    public class BoxOfCansController : Interactable
    {
        public SpriteRenderer spriteRenderer;
        // private AudioSource audioSource;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            // audioSource = GetComponent<AudioSource>();

            spriteRenderer.enabled = false;

            TypeEventSystem.Global.Register<OnStoryEventTriggerEvent>(e => {
                if (e.eventName == "hakumei_departed_to_reach_can")
                {
                    GameProgressData.Instance.SetInfo("IsCanBoxTakenByHakumei", "true");
                    gameObject.SetActive(false);
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void Start()
        {
            if (GameProgressData.Instance.CompareInfoWith("FinishedCanExperiment"))
            {
                GameProgressData.Instance.SetInfo("IsCanBoxTakenByHakumei", "true");
                spriteRenderer.enabled = false;
            }
            else
            {
                spriteRenderer.enabled = GameProgressData.Instance.CompareInfoWith("IsCanBoxSet");
            }
            
            // Put the can successfully
            HoverAction = () => {
                if (GameProgressData.Instance.CurrentObject == "can")
                {
                    spriteRenderer.enabled = true;

                    GameProgressData.Instance.SetInfo("IsCanBoxSet", "true");
                    GameProgressData.Instance.SetInfo("IsCanBoxTakenByHakumei", "false");

                    BackpackManager.Instance.Clear();
                    // Audio.AudioManager.PlaySFX("put_box");
                }
            };
        }
    }
}
