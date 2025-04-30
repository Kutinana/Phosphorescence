using Kuchinashi.DataSystem;
using UnityEngine;

namespace Phosphorescence.DataSystem
{
    [CreateAssetMenu(fileName = "New Obtainable Object", menuName = "Scriptable Objects/Obtainable Object")]
    public class ObtainableObject : ScriptableObject , IHaveId
    {
        public string Id => _id;
        [SerializeField] private string _id;
        public string Name;
        [TextArea(3, 10)] public string Description;
        public int MaxSize;
        public Sprite Icon;
        public GameObject Prefab;
    }
}
