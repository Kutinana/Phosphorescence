using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

namespace Common
{
    public class CamerasManager : MonoSingleton<CamerasManager>
    {
        public BaseCameraManager BaseCamera;

        void Awake()
        {
            DontDestroyOnLoad(gameObject);

            BaseCamera = GetComponentInChildren<BaseCameraManager>();
        }
    }

}