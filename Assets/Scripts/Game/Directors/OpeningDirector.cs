using System.Collections;
using Kuchinashi.Utils.Progressable;
using Phosphorescence.DataSystem;
using QFramework;
using UnityEngine;
using UnityEngine.Playables;

namespace Phosphorescence.Game
{
    public class OpeningDirector : MonoSingleton<OpeningDirector>
    {
        private PlayableDirector playableDirector;

        private void Awake()
        {
            playableDirector = GetComponent<PlayableDirector>();
        }

        public void Climb()
        {
            StartCoroutine(ClimbCoroutine());
        }

        private IEnumerator ClimbCoroutine()
        {
            yield return new WaitForSeconds(1f);
            float counter = 0f;
            while (counter < 100f)
            {
                counter += Time.deltaTime;
                CameraStack.Instance.Offset += new Vector3(0, 0.3f * Time.deltaTime);
                yield return null;
            }
        }
        
        public void Play()
        {
            playableDirector.Play();
            GameManager.Instance.ContinuePlot("0.5");
        }
    }

}