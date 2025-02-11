using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class CameraStack : MonoBehaviour
    {
        public Camera[] cameraStack;

        private void Awake()
        {
            foreach (var camera in cameraStack)
            {
                BaseCameraManager.AddToCameraStack(camera);
            }
        }

        private void OnDestroy()
        {
            foreach (var camera in cameraStack)
            {
                BaseCameraManager.RemoveFromCameraStack(camera);
            }
        }
    }
}