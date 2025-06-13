using System.Collections;
using Phosphorescence.Audio;
using Phosphorescence.DataSystem;
using Phosphorescence.Narration;
using QFramework;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Phosphorescence.Game
{
    public class BeaconController : MonoSingleton<BeaconController>
    {
        public bool IsOn;

        private Animator animator;
        private Light2D[] lights;
        public AudioSource audioSource;

        void Awake()
        {
            animator = GetComponent<Animator>();
            lights = GetComponentsInChildren<Light2D>();

            TypeEventSystem.Global.Register<OnStoryEventTriggerEvent>(e => {
                if (e.eventName == "beacon_stop")
                {
                    IsOn = false;
                    GameProgressData.Instance.SetState("IsBeaconOn", false);

                    UpdateStatus();
                }
            });
        }

        void Start()
        {
            IsOn = GameProgressData.Instance.GetState("IsBeaconOn");
            UpdateStatus();
        }

        private void UpdateStatus()  // Know the status, update the behavior
        {
            audioSource.enabled = IsOn;

            if (IsOn)
            {
                foreach (var light in lights)
                {
                    light.enabled = true;
                }
                animator.enabled = true;
                animator.Play("lens rotation");
                audioSource.enabled = true;
            }
            else
            {
                animator.enabled = false;
                audioSource.enabled = false;
                foreach (var light in lights)
                {
                    light.enabled = false;
                }
            }
        }

        public void Play()  // Update the status to Play and set the status
        {
            IsOn = true;
            animator.SetTrigger("Play");
            GameProgressData.Instance.SetState("IsBeaconOn", true);

            Audio.AudioManager.PlaySFX(GameDesignData.GetAudioData("beacon_start", out var audioData) ? audioData.clip : null);

            foreach (var light in lights)
            {
                light.enabled = true;
            }

            StartCoroutine(PlayCoroutine());
        }

        private IEnumerator PlayCoroutine()
        {
            animator.enabled = true;
            audioSource.enabled = true;
            audioSource.volume = 0f;

            var speed = 0f;
            while (speed < 1f)
            {
                speed += Time.deltaTime;

                animator.SetFloat("StartingSpeed", speed);
                audioSource.volume = speed / 2f;

                yield return null;
            }
            animator.SetFloat("StartingSpeed", 1f);
            audioSource.volume = 0.5f;

            yield return new WaitForSeconds(4f);
        }

        public void Stop()
        {
            IsOn = false;
            animator.SetTrigger("Stop");
            audioSource.enabled = false;

            GameProgressData.Instance.SetState("IsBeaconOn", false);
        }
    }
}
