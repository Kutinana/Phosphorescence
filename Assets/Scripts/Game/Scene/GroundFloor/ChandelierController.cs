using Phosphorescence.DataSystem;
using Phosphorescence.Narration;
using QFramework;
using UnityEngine;
using UnityEngine.Rendering.Universal;
namespace Phosphorescence.Game
{
    public class ChandelierController : MonoSingleton<ChandelierController>
    {
        public bool isActivated = true;

        private Light2D light;
    
        void Start()
        {
            light = GetComponent<Light2D>();

            isActivated = GameManager.Instance.GlobalPower;
            light.enabled = isActivated;

            TypeEventSystem.Global.Register<OnGlobalPowerChangedEvent>(e => {
                if (e.value)
                {
                    isActivated = true;
                    light.enabled = true;
                }
                else
                {
                    isActivated = false;
                    light.enabled = false;
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
    }
}