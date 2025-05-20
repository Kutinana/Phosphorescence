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
        public SpriteRenderer UpstairSpriteRenderer;
        public SpriteRenderer DownstairSpriteRenderer;
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
            m_IsUpstairing = true;

            rb.linearVelocity = Vector2.zero;

            UpstairSpriteRenderer.sprite = UpStairSprites[0];
            UpstairSpriteRenderer.enabled = true;
            UpstairSpriteRenderer.sortingLayerName = "Front";
            UpstairSpriteRenderer.sortingOrder = 2;

            spriteRenderer.enabled = false;
            m_SpriteCounter = 0;
        }

        public void Upstair(bool isHalfFloor = false)
        {
            if (IsOnStair && m_IsDownstairing) return;
            if (!IsOnStair) UpstairPrepare();

            if (m_UpstairCoroutine != null)
            {
                StopCoroutine(m_UpstairCoroutine);
            }
            m_UpstairCoroutine = StartCoroutine(UpstairCoroutine(isHalfFloor));
        }

        private Coroutine m_UpstairCoroutine = null;
        private bool m_IsUpstairing = false;
        private IEnumerator UpstairCoroutine(bool isHalfFloor)
        {
            while (m_SpriteCounter < (isHalfFloor ? 75 : UpStairSprites.Length - 1))
            {
                m_SpriteCounter++;
                UpstairSpriteRenderer.sprite = UpStairSprites[m_SpriteCounter];
                CameraStack.Instance.Offset += new Vector3(0, 0.065f);
                UpstairProgressable.Progress += isHalfFloor ? 0.015f : 0.01f;
                yield return new WaitForSeconds(0.05f);

                if (m_SpriteCounter == 75)
                {
                    UpstairSpriteRenderer.sortingLayerName = "Middle";
                    UpstairSpriteRenderer.sortingOrder = 0;
                }
            }

            // m_IsUpstairing = false;
            m_UpstairCoroutine = null;

            OffStair();
        }

        public void UpstairForOpening() => StartCoroutine(UpstairCoroutineForOpening());
        private IEnumerator UpstairCoroutineForOpening()
        {
            var target = new Vector3(-transform.position.x, 0f);
            while (CameraStack.Instance.Offset != target)
            {
                CameraStack.Instance.Offset = Vector3.MoveTowards(CameraStack.Instance.Offset, target, 0.01f);
                yield return new WaitForFixedUpdate();
            }
            yield return new WaitForSeconds(2f);

            for (int i = 0; i < 4; i++)
            {
                if (transform.localScale.x == -1)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    spriteRenderer.material.SetFloat("_FlipGreen", 1);
                }
                rb.linearVelocity = Vector2.zero;

                UpstairSpriteRenderer.sprite = UpStairSprites[0];
                UpstairSpriteRenderer.enabled = true;
                UpstairSpriteRenderer.sortingLayerName = "Front";
                UpstairSpriteRenderer.sortingOrder = 2;

                NormalSpriteProgressable.InverseLinearTransition(0.2f, 0f);
                m_SpriteCounter = 0;
                UpstairProgressable.InverseLinearTransition(0.2f, 0f);

                while (m_SpriteCounter < UpStairSprites.Length - 1)
                {
                    m_SpriteCounter++;
                    UpstairSpriteRenderer.sprite = UpStairSprites[m_SpriteCounter];
                    CameraStack.Instance.Offset += new Vector3(0, 0.075f);
                    UpstairProgressable.Progress += 0.01f;
                    yield return new WaitForSeconds(0.15f);

                    if (m_SpriteCounter == 75)
                    {
                        UpstairSpriteRenderer.sortingLayerName = "Middle";
                        UpstairSpriteRenderer.sortingOrder = 0;
                    }
                }
                TransportTo(new Vector3(transform.position.x, transform.position.y + 7.6f, 0));
                NormalSpriteProgressable.LinearTransition(1f, 0f);
                CameraStack.Instance.Offset = new Vector3(-transform.position.x, 0f);

                yield return new WaitForSeconds(1.5f);
            }
        }

        private void DownstairPrepare(bool isHalfFloor = false)
        {
            if (IsOnStair && m_IsUpstairing) return;
            if (IsOnStair) return;

            if (transform.localScale.x == -1)
            {
                transform.localScale = new Vector3(1, 1, 1);
                spriteRenderer.material.SetFloat("_FlipGreen", 1);
            }

            IsOnStair = true;
            m_IsDownstairing = true;

            DownstairSpriteRenderer.sprite = isHalfFloor ? DownStairSprites[45] : DownStairSprites[0];
            DownstairSpriteRenderer.enabled = true;
            DownstairSpriteRenderer.sortingLayerName = "Middle";
            DownstairSpriteRenderer.sortingOrder = 0;

            NormalSpriteProgressable.InverseLinearTransition(0.2f, 0f);
            DownstairProgressable.Progress = 1;
            m_SpriteCounter = isHalfFloor ? 45 : 0;

            DownstairSpriteRenderer.transform.localPosition = isHalfFloor ? new Vector3(-2.48f, -1.8f, 0) : new Vector3(3.13f, -4.15f, 0);
        }

        public void Downstair(bool isHalfFloor = false)
        {
            if (!IsOnStair) DownstairPrepare(isHalfFloor);
            
            if (m_DownstairCoroutine != null)
            {
                StopCoroutine(m_DownstairCoroutine);
            }
            m_DownstairCoroutine = StartCoroutine(DownstairCoroutine(isHalfFloor));
        }

        private Coroutine m_DownstairCoroutine = null;
        private bool m_IsDownstairing = false;
        private IEnumerator DownstairCoroutine(bool isHalfFloor)
        {
            while (m_SpriteCounter < DownStairSprites.Length - 1)
            {
                m_SpriteCounter++;
                DownstairSpriteRenderer.sprite = DownStairSprites[m_SpriteCounter];
                CameraStack.Instance.Offset -= new Vector3(0, 0.065f);
                DownstairProgressable.Progress -= isHalfFloor ? 0.015f : 0.01f;
                yield return new WaitForSeconds(0.05f);

                if (m_SpriteCounter == (isHalfFloor ? 105 : 45))
                {
                    DownstairSpriteRenderer.sortingLayerName = "Front";
                    DownstairSpriteRenderer.sortingOrder = 2;
                }
            }

            // m_IsDownstairing = false;
            m_DownstairCoroutine = null;

            OffStair();
        }

        // As a function of finishing a complete upstair or downstair
        private void OffStair()
        {
            if (IsOnStair)
            {
                if (m_UpstairCoroutine != null || m_DownstairCoroutine != null)
                {
                    return;
                }

                if (m_IsUpstairing)
                {
                    TransportTo(m_CurrentInteractTarget.GetComponent<StairInteractionController>().UpstairFinishPoint);
                }
                else if (m_IsDownstairing)
                {
                    TransportTo(m_CurrentInteractTarget.GetComponent<StairInteractionController>().DownstairFinishPoint);
                }
                else
                {
                    return;
                }
            }

            IsOnStair = false;
            UpstairSpriteRenderer.enabled = false;
            DownstairSpriteRenderer.enabled = false;
            UpstairProgressable.Progress = 0;
            DownstairProgressable.Progress = 0;

            spriteRenderer.enabled = true;
            NormalSpriteProgressable.LinearTransition(0.2f, 0f);
            
            CameraStack.Instance.Offset = new Vector3(0, 0f);

            m_IsUpstairing = false;
            m_IsDownstairing = false;

            GameManager.Instance.moveAction.Enable();

            if (m_UpstairCoroutine != null) StopCoroutine(m_UpstairCoroutine);
            if (m_DownstairCoroutine != null) StopCoroutine(m_DownstairCoroutine);
        }
        
    }

}