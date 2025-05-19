using System.Collections;
using Kuchinashi.Utils.Progressable;
using Phosphorescence.Narration;
using QFramework;
using UnityEngine;

namespace Phosphorescence.Game.Tutorials
{
    public class OperatorTutorial : MonoBehaviour
    {
        public Progressable spriteRendererProgressable;

        private void Awake()
        {
            TypeEventSystem.Global.Register<OnStoryEndEvent>(e => {
                if (e.plot && e.plot.Id == "0.5")
                {
                    StartCoroutine(StartTutorial());
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private IEnumerator StartTutorial()
        {
            yield return spriteRendererProgressable.LinearTransition(0.2f, 0f);
            var coroutine = spriteRendererProgressable.PingPong(1f, 0.5f, 1f);

            yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.E));
            StopCoroutine(coroutine);
            yield return spriteRendererProgressable.InverseLinearTransition(0.2f, 0f);
        }
    }
}