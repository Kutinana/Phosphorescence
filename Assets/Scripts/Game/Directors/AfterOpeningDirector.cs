using System.Collections;
using Kuchinashi.Utils.Progressable;
using Phosphorescence.DataSystem;
using QFramework;
using UnityEngine;
using UnityEngine.Playables;

namespace Phosphorescence.Game
{
    public class AfterOpeningDirector : MonoSingleton<AfterOpeningDirector>
    {
        private PlayableDirector playableDirector;

        private void Awake()
        {
            playableDirector = GetComponent<PlayableDirector>();
        }
        
        public void Play()
        {
            playableDirector.Play();
        }

        public void OnPerformStart()
        {
            Audio.AudioManager.SetMixerAmbientVolume(0f, 3f);
        }

        public void OnPerformEnd()
        {
            Audio.AudioManager.SetMixerAmbientVolume(0.8f, 3f);
            GameManager.Instance.ContinuePlot("0.9");

            GameProgressData.Instance.LastFloorIndex = 5;
        }
    }

}