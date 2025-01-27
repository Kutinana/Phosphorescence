using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

namespace Common
{
    public class TransitionCanvasManager : MonoSingleton<TransitionCanvasManager>
    {
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}