using System.Collections;
using System.Collections.Generic;
using Common;
using Kuchinashi.Utils;
using QFramework;
using UnityEngine;

namespace Phosphorescence.Game
{
    public class CameraStack : MonoSingleton<CameraStack>
    {
        public Camera[] cameraStack;
        private FollowTransform m_FollowTransform;

        public float Size = 5f;
        private float _lastSize = 5f;
        public Vector3 Offset = Vector3.zero;
        private Coroutine m_DampToSizeCoroutine;

        public AudioListener AudioListener;

        private void Awake()
        {
            foreach (var camera in cameraStack)
            {
                BaseCameraManager.AddToCameraStack(camera);
            }
            m_FollowTransform = GetComponent<FollowTransform>();
            AudioListener = GetComponent<AudioListener>();

            AudioListener.enabled = false;
        }

        private void FixedUpdate()
        {
            m_FollowTransform.offset = Offset;

            if (cameraStack.Length > 0 && cameraStack[0].orthographicSize != Size)
            {
                if (_lastSize != Size)
                {
                    foreach (var camera in cameraStack)
                    {
                        camera.orthographicSize = Size;
                    }
                }
            }
            _lastSize = Size;
        }

        public void DampToSize(float targetSize) => DampToSize(targetSize, 1f, 0f);
        public Coroutine DampToSize(float targetSize, float duration = 1f, float delay = 0f)
        {
            if (m_DampToSizeCoroutine != null)
            {
                StopCoroutine(m_DampToSizeCoroutine);
            }
            return m_DampToSizeCoroutine = StartCoroutine(DampToSizeCoroutine(targetSize, duration, delay));
        }
        private IEnumerator DampToSizeCoroutine(float targetSize, float duration, float delay)
        {
            if (cameraStack.Length == 0)
            {
                m_DampToSizeCoroutine = null;
                yield break;
            }
            yield return new WaitForSeconds(delay);

            float velocity = 0f;
            Size = cameraStack[0].orthographicSize;
            while (Mathf.Abs(Size - targetSize) > 0.01f)
            {
                Size = Mathf.SmoothDamp(Size, targetSize, ref velocity, duration);
                yield return new WaitForFixedUpdate();
            }
            
            Size = targetSize;

            m_DampToSizeCoroutine = null;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            foreach (var camera in cameraStack)
            {
                BaseCameraManager.RemoveFromCameraStack(camera);
            }
        }
    }
}