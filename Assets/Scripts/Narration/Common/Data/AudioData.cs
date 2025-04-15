using System;
using System.Collections;
using System.Collections.Generic;
using Kuchinashi.DataSystem;
using UnityEngine;

namespace Phosphorescence.Narration.Common
{
    [Serializable]
    [CreateAssetMenu(fileName = "Audio Data", menuName = "Scriptable Objects/Audio Data", order = 1)]
    public class AudioData : ScriptableObject, IHaveId
    {
        public string Id => id;
        public string id;

        public AudioType type;
        public bool isLoop;
        public AudioClip clip;
    }

    public enum AudioType
    {
        Music,
        Voice,
        SFX
    }
}