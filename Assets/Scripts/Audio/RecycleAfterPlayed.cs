using System.Collections;
using UnityEngine;

namespace Phosphorescence.Audio
{
    public class RecycleAfterPlayed : MonoBehaviour
    {
        public AudioSource AudioSource;

        public void Play()
        {
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

            AudioManager.Instance.VoiceSourcePool.Recycle(gameObject);
            AudioManager.Instance.SFXSourcePool.Recycle(gameObject);
        }
    }
}