using System.Collections;
using Kuchinashi.Utils.Progressable;
using QFramework;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Phosphorescence.Game
{
    public partial class PlayerController
    {
        [Header("UpDownStair Reference")]
        public bool IsOnStair = false;
        public Sprite[] UpStairSprites;
        public Sprite[] DownStairSprites;
        public SpriteRenderer UpDownSpriteRenderer;
        public Progressable NormalSpriteProgressable;
        public Progressable UpstairProgressable;
        public Progressable DownstairProgressable;

        private int m_SpriteCounter = 0;

        private void UpstairPrepare()
        {
            if (IsOnStair) return;

            if (transform.localScale.x == -1)
            {
                transform.localScale = new Vector3(1, 1, 1);
                spriteRenderer.material.SetFloat("_FlipGreen", 1);
            }

            IsOnStair = true;
            m_IsUpstairFinished = false;

            rb.linearVelocity = Vector2.zero;

            UpDownSpriteRenderer.sprite = UpStairSprites[0];
            UpDownSpriteRenderer.enabled = true;
            UpDownSpriteRenderer.sortingLayerName = "Front";
            UpDownSpriteRenderer.sortingOrder = 2;

            spriteRenderer.enabled = false;
            m_SpriteCounter = 0;
        }

        public void Upstair()
        {
            if (!IsOnStair) UpstairPrepare();

            if (m_UpstairCoroutine != null)
            {
                StopCoroutine(m_UpstairCoroutine);
            }
            m_UpstairCoroutine = StartCoroutine(UpstairCoroutine());
        }

        private Coroutine m_UpstairCoroutine = null;
        private bool m_IsUpstairFinished = false;
        private IEnumerator UpstairCoroutine()
        {
            while (m_SpriteCounter < UpStairSprites.Length - 1)
            {
                m_SpriteCounter++;
                UpDownSpriteRenderer.sprite = UpStairSprites[m_SpriteCounter];
                CameraStack.Instance.Offset += new Vector3(0, 0.05f);
                UpstairProgressable.Progress += 0.01f;
                yield return new WaitForSeconds(0.05f);

                if (m_SpriteCounter == 75)
                {
                    UpDownSpriteRenderer.sortingLayerName = "Middle";
                    UpDownSpriteRenderer.sortingOrder = 0;
                }
            }

            m_IsUpstairFinished = true;
            m_UpstairCoroutine = null;

            OffStair();
        }

        private void DownstairPrepare()
        {
            if (IsOnStair) return;

            if (transform.localScale.x == -1)
            {
                transform.localScale = new Vector3(1, 1, 1);
                spriteRenderer.material.SetFloat("_FlipGreen", 1);
            }

            IsOnStair = true;
            m_IsDownstairFinished = false;

            UpDownSpriteRenderer.sprite = DownStairSprites[0];
            UpDownSpriteRenderer.enabled = true;
            UpDownSpriteRenderer.sortingLayerName = "Middle";
            UpDownSpriteRenderer.sortingOrder = 0;

            NormalSpriteProgressable.Progress = 0;
            m_SpriteCounter = 0;
        }

        public void Downstair()
        {
            if (!IsOnStair) DownstairPrepare();
            
            if (m_DownstairCoroutine != null)
            {
                StopCoroutine(m_DownstairCoroutine);
            }
            m_DownstairCoroutine = StartCoroutine(DownstairCoroutine());
        }

        private Coroutine m_DownstairCoroutine = null;
        private bool m_IsDownstairFinished = false;
        private IEnumerator DownstairCoroutine()
        {
            while (m_SpriteCounter > 0)
            {
                m_SpriteCounter++;
                UpDownSpriteRenderer.sprite = DownStairSprites[m_SpriteCounter];
                CameraStack.Instance.Offset -= new Vector3(0, 0.05f);
                DownstairProgressable.Progress -= 0.01f;
                yield return new WaitForSeconds(0.05f);

                if (m_SpriteCounter == 45)
                {
                    UpDownSpriteRenderer.sortingLayerName = "Front";
                    UpDownSpriteRenderer.sortingOrder = 2;
                }
            }

            m_IsDownstairFinished = true;
            m_DownstairCoroutine = null;

            OffStair();
        }

        // As a function of finishing a complete upstair or downstair
        private void OffStair()
        {
            if (IsOnStair)
            {
                if (m_UpstairCoroutine != null) StopCoroutine(m_UpstairCoroutine);
                if (m_DownstairCoroutine != null) StopCoroutine(m_DownstairCoroutine);

                if (m_IsUpstairFinished)
                {
                    TransportTo(m_CurrentInteractTarget.GetComponent<StairInteractionController>().UpstairFinishPoint);
                }
                else if (m_IsDownstairFinished)
                {
                    TransportTo(m_CurrentInteractTarget.GetComponent<StairInteractionController>().DownstairFinishPoint);
                }
                else
                {
                    return;
                }
            }

            IsOnStair = false;
            UpDownSpriteRenderer.enabled = false;
            UpstairProgressable.Progress = 0;
            DownstairProgressable.Progress = 0;

            spriteRenderer.enabled = true;
            NormalSpriteProgressable.LinearTransition(0.2f, 0f);
            
            CameraStack.Instance.Offset = new Vector3(0, 0f);

            m_IsUpstairFinished = false;
            m_IsDownstairFinished = false;

            GameManager.Instance.moveAction.Enable();

            if (m_UpstairCoroutine != null) StopCoroutine(m_UpstairCoroutine);
            if (m_DownstairCoroutine != null) StopCoroutine(m_DownstairCoroutine);
        }
        
    }

}