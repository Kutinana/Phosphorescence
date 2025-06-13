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
    }

}