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
        public bool IsActivating => LightAnimator.GetCurrentAnimatorStateInfo(0).IsName("Starting");

        public Animator LightAnimator;
        public Animator BeaconRotationAnimator;
        public Animator MachineAnimator;
        public Animator GearsAnimator;

        public AudioSource audioSource;
        private AudioSource m_StartingAudioSource;

        void Awake()
        {
            TypeEventSystem.Global.Register<OnStoryEventTriggerEvent>(e => {
                if (e.eventName == "beacon_stop")
                {
                    Stop();
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        void Start()
        {
            IsOn = GameProgressData.Instance.CompareInfoWith("IsBeaconOn");
            UpdateStatus();
        }

        private void UpdateStatus()  // Know the status, update the behavior
        {
            if (!GameManager.Instance.GlobalPower)
            {
                IsOn = false;
                GameProgressData.Instance.SetInfo("IsBeaconOn", "false");
            }
            
            audioSource.enabled = IsOn;

            if (IsOn)
            {
                LightAnimator.Play("Running", 0, 0);
                // LightAnimator.speed = 1f;

                BeaconRotationAnimator.Play("BeaconRotation", 0, 0);
                BeaconRotationAnimator.SetFloat("Speed", 1f);

                MachineAnimator.speed = 1f;
                GearsAnimator.speed = 1f;

                audioSource.enabled = true;
            }
            else
            {
                LightAnimator.Play("Stopped", 0, 0);
                // LightAnimator.speed = 0f;

                BeaconRotationAnimator.Play("BeaconRotation", 0, 0);
                BeaconRotationAnimator.SetFloat("Speed", 0f);

                MachineAnimator.speed = 0f;
                GearsAnimator.speed = 0f;

                audioSource.enabled = false;
            }
        }

        public void Calibrate()
        {
            LightAnimator.Play("Running", 0, 0);
            LightAnimator.speed = 1f;
        }

        public void Play()  // Update the status to Play and set the status
        {
            IsOn = true;
            GameProgressData.Instance.SetInfo("IsBeaconOn", "true");

            m_StartingAudioSource = Audio.AudioManager.PlaySFX(GameDesignData.GetAudioData("beacon_start", out var audioData) ? audioData.clip : null);

            StartCoroutine(PlayCoroutine());
        }

        private IEnumerator PlayCoroutine()
        {
            audioSource.enabled = true;
            audioSource.volume = 0f;
            audioSource.Play();

            LightAnimator.Play("Starting", 0, 0);

            var speed = 0f;
            while (speed < 1f)
            {
                speed += Time.deltaTime * 0.5f;
                BeaconRotationAnimator.SetFloat("Speed", speed);

                MachineAnimator.speed = speed;
                GearsAnimator.speed = speed;

                yield return null;
            }
            BeaconRotationAnimator.SetFloat("Speed", 1f);

            MachineAnimator.speed = 1f;
            GearsAnimator.speed = 1f;

            while (audioSource.volume < 0.5f)
            {
                audioSource.volume += Time.deltaTime * 0.2f;
                yield return null;
            }
            audioSource.volume = 0.5f;

            yield return new WaitUntil(() => BeaconRotationAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime * 100 % 100 <= 1);
            LightAnimator.SetTrigger("Run");
        }

        public void Stop()
        {
            IsOn = false;
            LightAnimator.SetTrigger("Stop");

            if (m_StartingAudioSource != null)
            {
                m_StartingAudioSource.Stop();
            }

            StartCoroutine(StopCoroutine());

            GameProgressData.Instance.SetInfo("IsBeaconOn", "false");
        }

        private IEnumerator StopCoroutine()
        {
            var speed = 1f;
            while (speed > 0f)
            {
                speed -= Time.deltaTime;

                BeaconRotationAnimator.SetFloat("Speed", speed);
                audioSource.volume = speed / 2f;

                MachineAnimator.speed = speed;
                GearsAnimator.speed = speed;

                yield return null;
            }

            BeaconRotationAnimator.SetFloat("Speed", 0f);

            MachineAnimator.speed = 0f;
            GearsAnimator.speed = 0f;

            audioSource.volume = 0f;
            audioSource.Stop();
        }
    }
}
