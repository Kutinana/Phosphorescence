using System.Collections;
using System.Collections.Generic;
using Kuchinashi;
using Kuchinashi.DataSystem;
using QFramework;
using UnityEngine;
using UnityEngine.Audio;

namespace Phosphorescence.Audio
{
    public partial class AudioManager
    {
        [Header("Audio Mixer")]
        public AudioMixer AudioMixer;

        public static float MixerGlobalVolume => UserConfig.ReadWithDefaultValue<float>("GlobalVolume", 0.8f);
        public static void SetMixerGlobalVolume(float percentage, float duration = 0f)
        {
            UserConfig.Write("GlobalVolume", percentage);

            if (duration > 0)
            {
                Instance.StartCoroutine(Instance.FadeMixerVolume("MasterVolume", percentage, duration));
                return;
            }
            
            Instance.AudioMixer.SetFloat("MasterVolume", percentage == 0 ? -60 : Mathf.Log10(percentage + 0.2f) * 80);
        }

        public static float MixerAmbientVolume => UserConfig.ReadWithDefaultValue<float>("AmbientVolume", 0.8f);
        public static void SetMixerAmbientVolume(float percentage, float duration = 0f)
        {
            UserConfig.Write("AmbientVolume", percentage);

            if (duration > 0)
            {
                Instance.StartCoroutine(Instance.FadeMixerVolume("AmbientVolume", percentage, duration));
                return;
            }
            
            Instance.AudioMixer.SetFloat("AmbientVolume", percentage == 0 ? -60 : Mathf.Log10(percentage + 0.2f) * 80);
        }

        public static float MixerMusicVolume => UserConfig.ReadWithDefaultValue<float>("MusicVolume", 0.8f);
        public static void SetMixerMusicVolume(float percentage, float duration = 0f)
        {
            UserConfig.Write("MusicVolume", percentage);

            if (duration > 0)
            {
                Instance.StartCoroutine(Instance.FadeMixerVolume("MusicVolume", percentage, duration));
                return;
            }
            
            Instance.AudioMixer.SetFloat("MusicVolume", percentage == 0 ? -60 : Mathf.Log10(percentage + 0.2f) * 80);
        }

        public static float MixerSFXVolume => UserConfig.ReadWithDefaultValue<float>("SFXVolume", 0.8f);
        public static void SetMixerSFXVolume(float percentage, float duration = 0f)
        {
            UserConfig.Write("SFXVolume", percentage);

            if (duration > 0)
            {
                Instance.StartCoroutine(Instance.FadeMixerVolume("SFXVolume", percentage, duration));
                return;
            }
            
            Instance.AudioMixer.SetFloat("SFXVolume", percentage == 0 ? -60 : Mathf.Log10(percentage + 0.2f) * 80);
        }

        public static float MixerVoiceVolume => UserConfig.ReadWithDefaultValue<float>("VoiceVolume", 0.8f);
        public static void SetMixerVoiceVolume(float percentage, float duration = 0f)
        {
            UserConfig.Write("VoiceVolume", percentage);

            if (duration > 0)
            {
                Instance.StartCoroutine(Instance.FadeMixerVolume("VoiceVolume", percentage, duration));
                return;
            }
            
            Instance.AudioMixer.SetFloat("VoiceVolume", percentage == 0 ? -60 : Mathf.Log10(percentage + 0.2f) * 80);
        }

        private IEnumerator FadeMixerVolume(string key, float percentage, float duration)
        {
            float time = 0f;
            Instance.AudioMixer.GetFloat(key, out var startVolume);
            var targetVolume = percentage == 0 ? -60 : Mathf.Log10(percentage + 0.2f) * 80;

            while (time < duration)
            {
                Instance.AudioMixer.SetFloat(key, Mathf.Lerp(startVolume, targetVolume, time / duration));
                time += Time.deltaTime;
                yield return null;
            }

            Instance.AudioMixer.SetFloat(key, targetVolume);
        }
    }
}