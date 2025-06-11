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

            TypeEventSystem.Global.Register<OnStoryEndEvent>(e => {
                if (e.plot.Id == "2.5")
                {
                    GameProgressData.Instance.SetState("IsCanBoxTakenByHakumei", true);
                    gameObject.SetActive(false);
                }
            });
        }

        private void Start()
        {
            if (GameProgressData.Instance.GetState("IsCanBoxTakenByHakumei"))
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(GameProgressData.Instance.GetState("IsCanBoxSet"));
            }
            
            HoverAction = () => {
                if (GameProgressData.Instance.CurrentObject == "can")
                {
                    spriteRenderer.enabled = true;
                    GameProgressData.Instance.PlotTags.Add("CanSet");
                    GameProgressData.Instance.SetState("IsCanBoxSet", true);
                    GameProgressData.Instance.SetState("IsCanBoxTakenByHakumei", false);

                    BackpackManager.Instance.Clear();
                    // Audio.AudioManager.PlaySFX("put_box");
                }
            };
        }
    }
}
