using System.Collections;
using UnityEngine;

namespace Phosphorescence.Audio
{
    public class RecycleAfterPlayed : MonoBehaviour
    {
        public AudioSource AudioSource;

        public string Type;

        public void Play(string type)
        {
            Type = type;

            if (AudioSource == null)
                AudioSource = GetComponent<AudioSource>();

            AudioSource.Play();
            StartCoroutine(RecycleCoroutine());
        }

        private IEnumerator RecycleCoroutine()
        {
            yield return new WaitUntil(() => !AudioSource.isPlaying);
            AudioSource.Stop();
            AudioSource.clip = null;
            AudioSource.volume = 0;

            if (Type == "voice")
                AudioManager.Instance.VoiceSourcePool.Recycle(gameObject);
            else if (Type == "sfx")
                AudioManager.Instance.SFXSourcePool.Recycle(gameObject);
        }
    }
}