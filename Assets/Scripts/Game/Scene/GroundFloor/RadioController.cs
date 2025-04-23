using Phosphorescence.DataSystem;
using Phosphorescence.Narration;
using UnityEngine;

namespace Phosphorescence.Game
{
    public class RadioController : Interactable
    {
        public override System.Action InteractAction { get; set; }
        public override System.Action HoverAction { get; set; }
        public override System.Action UnhoverAction { get; set; }
        public Sprite defaultSprite;
        public Sprite onHoverSprite;
        public SpriteRenderer spriteRenderer;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            InteractAction = () => {
                GameManager.Instance.ContinuePlot();
            };
            HoverAction = () => {
                spriteRenderer.sprite = onHoverSprite;
            };
            UnhoverAction = () => {
                spriteRenderer.sprite = defaultSprite;
            };
        }
    }
}