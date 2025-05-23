using System.Collections;
using Phosphorescence.Audio;
using Phosphorescence.DataSystem;
using Phosphorescence.Narration;
using QFramework;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Phosphorescence.Game
{
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
                    gameObject.SetActive(false);
                }
            });
        }

        private void Start()
        {
            if (float.Parse(GameProgressData.Instance.CurrentPlotProgress) > 2.5f)
            {
                gameObject.SetActive(false);
            }
            
            HoverAction = () => {
                if (GameProgressData.Instance.CurrentObject == "can")
                {
                    spriteRenderer.enabled = true;
                    GameProgressData.Instance.PlotTags.Add("CanSet");

                    BackpackManager.Instance.Clear();
                    // Audio.AudioManager.PlaySFX("put_box");
                }
            };
        }
    }
}
