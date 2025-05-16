using QFramework;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Phosphorescence.Game
{
    public partial class PlayerController
    {
        [Header("UpDownStair Reference")]
        public bool IsOnStair = false;
        public Sprite[] UpDownSprites;
        public SpriteRenderer UpDownSpriteRenderer;

        private int m_SpriteCounter = 0;

        private void UpstairPrepare()
        {
            if (IsOnStair) return;

            IsOnStair = true;
            UpDownSpriteRenderer.sprite = UpDownSprites[0];
            UpDownSpriteRenderer.enabled = true;
            spriteRenderer.enabled = false;
            m_SpriteCounter = 0;
        }

        public void Upstair()
        {
            if (!IsOnStair)
            {
                UpstairPrepare();
                return;
            }

            if (m_SpriteCounter >= UpDownSprites.Length) return;

            m_SpriteCounter++;
            UpDownSpriteRenderer.sprite = UpDownSprites[m_SpriteCounter];
        }

        private void DownstairPrepare()
        {
            if (IsOnStair) return;

            IsOnStair = true;
            UpDownSpriteRenderer.enabled = false;
            spriteRenderer.enabled = true;
            m_SpriteCounter = UpDownSprites.Length - 1;
        }

        public void Downstair()
        {
            if (!IsOnStair)
            {
                DownstairPrepare();
                return;
            }

            if (m_SpriteCounter <= 0) return;

            m_SpriteCounter--;
            UpDownSpriteRenderer.sprite = UpDownSprites[m_SpriteCounter];
        }

        private void OffStair()
        {
            if (!IsOnStair) return;

            IsOnStair = false;
            UpDownSpriteRenderer.enabled = false;
            spriteRenderer.enabled = true;
        }
    }

}