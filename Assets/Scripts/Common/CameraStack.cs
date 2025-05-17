using System.Collections;
using System.Collections.Generic;
using Common;
using QFramework;
using UnityEngine;

namespace Phosphorescence.Game
{
    public class CameraStack : MonoSingleton<CameraStack>
    {
        public Camera[] cameraStack;

        public float Size = 5f;
        private Coroutine m_DampToSizeCoroutine;

        private void Awake()
        {
            foreach (var camera in cameraStack)
            {
                BaseCameraManager.AddToCameraStack(camera);
            }
        }

        public void DampToSize(float targetSize)
        {
            DampToSize(targetSize, 1f, 0f);
        }
        public void DampToSize(float targetSize, float duration = 1f, float delay = 0f)
        {
            if (m_DampToSizeCoroutine != null)
            {
                StopCoroutine(m_DampToSizeCoroutine);
            }
            m_DampToSizeCoroutine = StartCoroutine(DampToSizeCoroutine(targetSize, duration, delay));
        }
        private IEnumerator DampToSizeCoroutine(float targetSize, float duration, float delay)
        {
            yield return new WaitForSeconds(delay);

            float velocity = 0f;
            while (Mathf.Abs(Size - targetSize) > 0.01f)
            {
                Size = Mathf.SmoothDamp(Size, targetSize, ref velocity, duration);
                foreach (var camera in cameraStack)
                {
                    camera.orthographicSize = Size;
                }
                yield return new WaitForFixedUpdate();
            }
            
            Size = targetSize;
            foreach (var camera in cameraStack)
            {
                camera.orthographicSize = Size;
            }

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