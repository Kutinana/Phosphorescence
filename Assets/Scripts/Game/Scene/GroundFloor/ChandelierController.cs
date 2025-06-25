using Phosphorescence.DataSystem;
using Phosphorescence.Narration;
using QFramework;
using UnityEngine;
using UnityEngine.Rendering.Universal;
namespace Phosphorescence.Game
{
    public class ChandelierController : MonoBehaviour
    {
        public bool isActivated = true;

        private Light2D chandelierLight;

        void Awake()
        {
            chandelierLight = GetComponentInChildren<Light2D>();
        }

        void Start()
        {
            isActivated = GameManager.Instance.GlobalPower;
            chandelierLight.enabled = isActivated;

            TypeEventSystem.Global.Register<OnGlobalPowerChangedEvent>(e => {
                if (e.value)
                {
                    isActivated = true;
                    chandelierLight.enabled = true;
                }
                else
                {
                    isActivated = false;
                    chandelierLight.enabled = false;
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
    }
}