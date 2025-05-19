using System.Collections;
using Kuchinashi.Utils.Progressable;
using Phosphorescence.Narration;
using QFramework;
using UnityEngine;

namespace Phosphorescence.Game.Tutorials
{
    public class PressWTutorial : MonoBehaviour
    {
        public Progressable spriteRendererProgressable;

        private void Awake()
        {
            TypeEventSystem.Global.Register<OnPressADTutorialEndEvent>(e => {
                GameManager.Instance.upStairAction.Disable();
                GameManager.Instance.downStairAction.Disable();
                StartCoroutine(StartTutorial());
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private IEnumerator StartTutorial()
        {
            yield return spriteRendererProgressable.LinearTransition(0.2f, 0f);
            var coroutine = spriteRendererProgressable.PingPong(1f, 0.5f, 1f);

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.W));
            StopCoroutine(coroutine);
            yield return spriteRendererProgressable.InverseLinearTransition(0.2f, 0f);

            GameManager.Instance.ContinuePlot("0.5");
            PlayerController.Instance.UpstairForOpening();
        }
    }
}