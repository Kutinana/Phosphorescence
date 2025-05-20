using System.Collections;
using Phosphorescence.Audio;
using Phosphorescence.DataSystem;
using QFramework;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Phosphorescence.Game
{
    public class BeaconController : MonoSingleton<BeaconController>
    {
        public bool IsOn;

        private AudioSource audioSource;
        private Animator animator;
        private Light2D[] lights;

        void Awake()
        {
            animator = GetComponent<Animator>();
            lights = GetComponentsInChildren<Light2D>();
            audioSource = GetComponent<AudioSource>();
        }

        void Start()
        {
            if (GameProgressData.Instance.GetState("IsBeaconOn"))
            {
                animator.enabled = true;
                animator.Play("lens rotation");
                audioSource.enabled = true;
                audioSource.Pause();

                IsOn = true;
            }
            else
            {
                Pause();

                IsOn = false;
            }
        }

        public void Play()
        {
            IsOn = true;
            Audio.AudioManager.PlaySFX(GameDesignData.GetAudioData("beacon_start", out var audioData) ? audioData.clip : null);
            GameProgressData.Instance.SetState("IsBeaconOn", true);

            foreach (var light in lights)
            {
                light.enabled = true;
            }

            StartCoroutine(PlayCoroutine());
        }

        private IEnumerator PlayCoroutine()
        {
            animator.enabled = true;
            var speed = 0f;
            while (speed < 1f)
            {
                speed += Time.deltaTime;
                animator.SetFloat("StartingSpeed", speed);
                yield return null;
            }

            yield return new WaitForSeconds(4f);
            audioSource.Play();
        }

        public void Pause()
        {
            IsOn = false;
            audioSource.Pause();
            animator.enabled = false;
            
            foreach (var light in lights)
            {
                light.enabled = false;
            }
        }
    }
}
