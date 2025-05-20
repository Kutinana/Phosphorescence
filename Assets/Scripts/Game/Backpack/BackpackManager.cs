using Kuchinashi;
using Kuchinashi.Utils.Progressable;
using Phosphorescence.DataSystem;
using QFramework;
using UnityEngine;
using UnityEngine.UI;
namespace Phosphorescence.Game
{
    public class BackpackManager : MonoSingleton<BackpackManager>
    {
        public Progressable Progressable;
        public Image Slot;

        public Sprite emptySprite;

        public static bool IsEmpty => GameProgressData.Instance.CurrentObject == "";

        private void Start()
        {
            if (GameProgressData.Instance.CurrentObject != "")
            {
                Slot.sprite = GameDesignData.GetData<ObtainableObject>(GameProgressData.Instance.CurrentObject, out var obtainableObject) ? obtainableObject.Icon : emptySprite;
            }
        }

        public void Obtain(string itemId)
        {
            if (GameProgressData.Instance.CurrentObject != "") return;

            GameProgressData.Instance.CurrentObject = itemId;
            Slot.sprite = GameDesignData.GetData<ObtainableObject>(itemId, out var obtainableObject) ? obtainableObject.Icon : emptySprite;

            GameProgressData.Instance.Serialize();
        }

        public void Clear()
        {
            GameProgressData.Instance.CurrentObject = "";
            Slot.sprite = emptySprite;
            
            GameProgressData.Instance.Serialize();
        }
    }
}