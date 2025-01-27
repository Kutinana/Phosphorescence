using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace Common
{
    public class EventSystemManager : MonoSingleton<EventSystemManager>
    {
        void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }

}