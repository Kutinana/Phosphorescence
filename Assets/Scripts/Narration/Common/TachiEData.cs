using System;
using System.Collections;
using System.Collections.Generic;
using Kuchinashi.DataSystem;
using UnityEngine;

namespace Phosphorescence.Narration.Common
{
    [Serializable]
    [CreateAssetMenu(fileName = "TachiEData", menuName = "Scriptable Objects/TachiEData", order = 1)]
    public class TachiEData : ScriptableObject, IHaveId
    {
        public string Id => id;
        public string id;
        public Sprite sprite;
        public Vector3 positionOffset;
        public Vector3 rotationOffset;
        public Vector3 scaleOffset;
    }
}