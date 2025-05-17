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
            return obj;
        }, obj => {
            obj.SetActive(false);
        });

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
            var obj = Instance.SFXSourcePool.Allocate().GetComponent<AudioSource>();
            obj.gameObject.SetActive(true);

            obj.clip = clip;
            obj.loop = loop;
            obj.volume = volume;
            obj.GetComponent<RecycleAfterPlayed>().Play();

            return obj;
        }
    }
}