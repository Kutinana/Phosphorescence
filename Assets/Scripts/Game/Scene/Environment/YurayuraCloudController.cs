using System;
using System.Collections;
using UnityEngine;

namespace Phosphorescence.Game
{
    public class YurayuraCloudController : MonoBehaviour
    {
        [Header("Parameters")]
        public float TransitionTime = 1f;
        public Vector2 Threshold = new Vector2(0, 0);
        public bool RandomSpeed = false;

        private Vector2 m_TargetPosition;
        private Coroutine m_Coroutine;

        private void Update()
        {
            if (m_Coroutine == null)
            {
                m_TargetPosition = new Vector2(
                    UnityEngine.Random.Range(-Threshold.x, Threshold.x),
                    UnityEngine.Random.Range(-Threshold.y, Threshold.y)
                );
                m_Coroutine = StartCoroutine(MoveCloud());
            }
        }

        private IEnumerator MoveCloud()
        {
            Vector2 refVelocity = Vector2.zero;

            while (Mathf.Abs(transform.position.x - m_TargetPosition.x) > 0.01f
                && Mathf.Abs(transform.position.y - m_TargetPosition.y) > 0.01f)
            {
                transform.position = Vector2.SmoothDamp(transform.position, m_TargetPosition, ref refVelocity, TransitionTime);
                yield return null;
            }

            m_Coroutine = null;
        }
    }
}