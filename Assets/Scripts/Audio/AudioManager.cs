using System.Collections;
using System.Collections.Generic;
using Kuchinashi;
using Phosphorescence.DataSystem;
using QFramework;
using UnityEngine;
using UnityEngine.Audio;

namespace Phosphorescence.Audio
{
    public partial class AudioManager : MonoSingleton<AudioManager>
    {
        public AudioSource MusicSource;
        // public AudioSource AmbientSource;

        public SerializableDictionary<string, AudioSource> AmbientSources;

        public GameObject SFXSourceTemplate;
        public SimpleObjectPool<GameObject> SFXSourcePool = new SimpleObjectPool<GameObject>(() => {
            var obj = Instantiate(Instance.SFXSourceTemplate, Instance.transform);
            var audioSource = obj.GetComponent<AudioSource>();
            Instance.SFXSources.Add(audioSource);
            return obj;
        }, obj => {
            obj.SetActive(false);
        });
        public List<AudioSource> SFXSources = new List<AudioSource>();

        public GameObject VoiceSourceTemplate;
        public SimpleObjectPool<GameObject> VoiceSourcePool = new SimpleObjectPool<GameObject>(() => {
            var obj = Instantiate(Instance.VoiceSourceTemplate, Instance.transform);
            return obj;
        }, obj => {
            obj.SetActive(false);
        });

        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            SetMixerGlobalVolume(MixerGlobalVolume);
            SetMixerMusicVolume(MixerMusicVolume);
            SetMixerSFXVolume(MixerSFXVolume);
            SetMixerVoiceVolume(MixerVoiceVolume);
        }

        public static void PlayMusic(AudioClip clip, bool loop = false)
        {
            Instance.MusicSource.clip = clip;
            Instance.MusicSource.loop = loop;
            Instance.MusicSource.Play();
        }

        public static void StopMusic()
        {
            Instance.MusicSource.Stop();
        }
        
        public static void SetAmbientVolume(string key, float volume, float duration = 0.5f)
        {
            if (Instance.FadeAmbientVolumeCoroutine != null)
            {
                Instance.StopCoroutine(Instance.FadeAmbientVolumeCoroutine);
            }

            Instance.FadeAmbientVolumeCoroutine = Instance.StartCoroutine(Instance.FadeAmbientVolume(key, volume, duration));
        }

        private Coroutine FadeAmbientVolumeCoroutine;
        private IEnumerator FadeAmbientVolume(string key, float volume, float duration)
        {
            float time = 0f;
            float startVolume = Instance.AmbientSources[key].volume;

            while (time < duration)
            {
                Instance.AmbientSources[key].volume = Mathf.Lerp(startVolume, volume, time / duration);
                time += Time.deltaTime;
                yield return null;
            }

            FadeAmbientVolumeCoroutine = null;
        }

        public static AudioSource PlayVoice(AudioClip clip, bool loop = false, float volume = 0.8f)
        {
            var obj = Instance.VoiceSourcePool.Allocate().GetComponent<AudioSource>();
            obj.gameObject.SetActive(true);

            obj.clip = clip;
            obj.loop = loop;
            obj.volume = volume;
            obj.GetComponent<RecycleAfterPlayed>().Play("voice");

            return obj;
        }

        public static AudioSource PlaySFX(string key, bool loop = false, float volume = 0.8f)
        {
            if (GameDesignData.GetAudioData(key, out var audioData) || GameDesignData.GetAudioData("sfx_" + key, out audioData))
            {
                return PlaySFX(audioData.clip, loop, volume);
            }
            return null;
        }

        public static AudioSource PlaySFX(AudioClip clip, bool loop = false, float volume = 0.8f)
        {
            if (clip == null) return null;
            
            var obj = Instance.SFXSourcePool.Allocate().GetComponent<AudioSource>();
            obj.gameObject.SetActive(true);

            obj.clip = clip;
            obj.loop = loop;
            obj.volume = volume;
            obj.GetComponent<RecycleAfterPlayed>().Play("sfx");

            return obj;
        }

        public static void StopAllSFX()
        {
            foreach (var obj in Instance.SFXSources)
            {
                obj.Stop();
            }
        }
    }
}