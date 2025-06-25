using Kuchinashi.Utils.Progressable;
using Phosphorescence.DataSystem;
using Phosphorescence.Narration;
using QFramework;
using UnityEngine;

namespace Phosphorescence.Game
{
    public class AuroraController : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        public Animator animator;
        public Progressable progressable;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            progressable = GetComponent<Progressable>();

            spriteRenderer.enabled = false;
            animator.enabled = false;

            TypeEventSystem.Global.Register<OnStoryEventTriggerEvent>(e => {
                if (e.eventName == "aurora_start")
                {
                    spriteRenderer.enabled = true;
                    progressable.LinearTransition(2f);
                    animator.enabled = true;

                    Audio.AudioManager.PlayMusic(GameDesignData.GetAudioData("main_theme", out var audioData) ? audioData.clip : null);
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
            
    }
}
