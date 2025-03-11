using System;
using System.Collections;
using System.Collections.Generic;
using Kuchinashi.DataSystem;
using UnityEngine;

namespace Phosphorescence.Narration.Common
{
    [Serializable]
    [CreateAssetMenu(fileName = "Tachi E Data", menuName = "Scriptable Objects/Tachi E Data", order = 1)]
    public class TachiEData : ScriptableObject, IHaveId
    {
        public string Id => id;
        public string id;

        public TachiEType type;

        public Sprite sprite;
        public AnimationClip animationClip;
        
        public Vector3 positionOffset;
        public Vector3 rotationOffset;
        public Vector3 scaleOffset = Vector3.one;
    }

    public enum TachiEType
    {
        Static,
        Animation
    }
}