using Phosphorescence.DataSystem;
using Phosphorescence.Narration;
using QFramework;
using UnityEngine;
using UnityEngine.Rendering.Universal;
namespace Phosphorescence.Game
{
    public class LibraryLightController : MonoBehaviour
    {
        public bool isActivated = true;

        private Light2D libraryLight;

        void Awake()
        {
            libraryLight = GetComponentInChildren<Light2D>();
        }

        void Start()
        {
            isActivated = GameManager.Instance.GlobalPower;
            libraryLight.enabled = isActivated;

            TypeEventSystem.Global.Register<OnGlobalPowerChangedEvent>(e => {
                if (e.value)
                {
                    isActivated = true;
                    libraryLight.enabled = true;
                }
                else
                {
                    isActivated = false;
                    libraryLight.enabled = false;
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
    }
}