using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Phosphorescence.Game
{
    public class FireController : MonoBehaviour
    {
        private Light2D m_Light;
        public Vector2 IntensityRange = new Vector2(2f, 10f);
        public Vector2 TimeRange = new Vector2(0.02f, 0.1f);

        private void Start()
        {
            m_Light = GetComponentInChildren<Light2D>();
            StartCoroutine(SimulateFireLight());
        }

        private IEnumerator SimulateFireLight()
        {
            while (true)
            {
                float targetIntensity = Random.Range(IntensityRange.x, IntensityRange.y);
                float time = Random.Range(TimeRange.x, TimeRange.y);
                float refIntensity = 10f;

                while (Mathf.Abs(m_Light.intensity - targetIntensity) > 0.01f)
                {
                    m_Light.intensity = Mathf.SmoothDamp(m_Light.intensity, targetIntensity, ref refIntensity, time);
                    yield return null;
                }
            }
        }
    }
}