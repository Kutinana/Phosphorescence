using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

namespace Common
{
    public class DefaultCanvas : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}