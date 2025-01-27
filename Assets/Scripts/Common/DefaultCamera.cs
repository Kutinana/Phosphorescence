using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    [RequireComponent(typeof(Camera))]
    public class DefaultCamera : MonoBehaviour
    {
        private void Awake()
        {
            BaseCameraManager.AddToCameraStack(GetComponent<Camera>());
        }

        private void OnDestroy()
        {
            if (TryGetComponent<Camera>(out var camera))
            {
                BaseCameraManager.RemoveFromCameraStack(camera);
            }
        }
    }
}
