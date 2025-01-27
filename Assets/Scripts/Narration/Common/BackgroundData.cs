using System;
using System.Collections;
using System.Collections.Generic;
using Kuchinashi.DataSystem;
using UnityEngine;

namespace Phosphorescence.Narration.Common
{
    [Serializable]
    [CreateAssetMenu(fileName = "TachiEData", menuName = "Scriptable Objects/BackgroundData", order = 0)]
    public class BackgroundData : ScriptableObject, IHaveId
    {
        public string Id { get; set; }
        public Sprite sprite;
        public Vector3 positionOffset;
        public Vector3 rotationOffset;
        public Vector3 scaleOffset;
    }
}