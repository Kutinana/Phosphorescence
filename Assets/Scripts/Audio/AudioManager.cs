using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace Phosphorescence.Audio
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        public AudioSource MusicSource;
        public AudioSource AmbientSource;

        public GameObject SFXSourceTemplate;
        public SimpleObjectPool<GameObject> SFXSourcePool = new SimpleObjectPool<GameObject>(() => {
            var obj = Instantiate(Instance.SFXSourceTemplate, Instance.transform);
            var audioSource = obj.GetComponent<AudioSource>();
            Instance.SFXSources.Add(audioSource);
            return obj;
        }, obj => {
            obj.SetActive(false);
            var audioSource = obj.GetComponent<AudioSource>();
            Instance.SFXSources.Remove(audioSource);
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
        
        

        public static AudioSource PlayVoice(AudioClip clip, bool loop = false, float volume = 0.8f)
        {
            var obj = Instance.VoiceSourcePool.Allocate().GetComponent<AudioSource>();
            obj.gameObject.SetActive(true);

            obj.clip = clip;
            obj.loop = loop;
            obj.volume = volume;
            obj.GetComponent<RecycleAfterPlayed>().Play();

            return obj;
        }

        public static AudioSource PlaySFX(AudioClip clip, bool loop = false, float volume = 0.8f)
        {
            if (clip == null) return null;
            
            var obj = Instance.SFXSourcePool.Allocate().GetComponent<AudioSource>();
            obj.gameObject.SetActive(true);

            obj.clip = clip;
            obj.loop = loop;
            obj.volume = volume;
            obj.GetComponent<RecycleAfterPlayed>().Play();

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