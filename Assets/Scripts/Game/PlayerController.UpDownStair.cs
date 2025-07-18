using System.Collections;
using Kuchinashi.Utils.Progressable;
using Phosphorescence.DataSystem;
using QFramework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

namespace Phosphorescence.Game
{
    public partial class PlayerController
    {
        
        [Header("Upstair Reference")]
        public SpriteRenderer UpstairSpriteRenderer;
        public Animator UpstairAnimator;
        public Light2D UpstairLight;

        [Header("Downstair Reference")]
        public SpriteRenderer DownstairSpriteRenderer;
        public Animator DownstairAnimator;
        public Light2D DownstairLight;

        [Header("Normal Sprite Reference")]
        public Progressable NormalSpriteProgressable;
        public Light2D NormalLight;

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
            StopMoving();

            UpstairSpriteRenderer.enabled = true;
            UpstairAnimator.enabled = true;
            UpstairLight.enabled = true;

            NormalSpriteProgressable.InverseLinearTransition(0.2f, 0f);
            NormalLight.enabled = false;
        }

        public void Upstair(bool isHalfFloor = false)
        {
            if (IsOnStair) return;
            else UpstairPrepare();

            if (m_UpstairCoroutine != null) StopCoroutine(m_UpstairCoroutine);
            m_UpstairCoroutine = StartCoroutine(UpstairCoroutine(isHalfFloor));
        }

        private Coroutine m_UpstairCoroutine = null;
        private bool m_IsUpstairing = false;
        private IEnumerator UpstairCoroutine(bool isHalfFloor)
        {
            var progress = 0f;
            UpstairAnimator.enabled = true;
            UpstairAnimator.speed = 0;

            var animation = isHalfFloor ? "HalfUpstair" : "FullUpstair";

            var currentFloorMask = FloorManager.Instance.GetCurrentFloor().Mask;
            var targetFloorMask = FloorManager.Instance.GetFloor(ImAtFloor + 1).Mask;

            while (progress < 1f)
            {
                CameraStack.Instance.Offset += new Vector3(0, 0.065f);
                UpstairAnimator.Play(animation, 0, progress);

                if (currentFloorMask != null) currentFloorMask.Progress = progress;
                if (targetFloorMask != null) targetFloorMask.Progress = 1 - progress;

                yield return new WaitForSeconds(0.05f);
                progress += 1f / (isHalfFloor ? 75 : 120);

                if ((Mathf.RoundToInt(progress * 240) % 10 == 0 && !isHalfFloor) ||
                    (Mathf.RoundToInt(progress * 150) % 10 == 0 && isHalfFloor))
                {
                    PlayFootstepOnStair();
                }
            }

            // m_IsUpstairing = false;
            m_UpstairCoroutine = null;

            OffStair();
        }

        private void DownstairPrepare(bool isHalfFloor = false)
        {
            if (IsOnStair) return;

            if (transform.localScale.x == -1)
            {
                transform.localScale = new Vector3(1, 1, 1);
                spriteRenderer.material.SetFloat("_FlipGreen", 1);
            }

            IsOnStair = true;
            m_IsDownstairing = true;
            StopMoving();

            DownstairSpriteRenderer.enabled = true;
            DownstairAnimator.enabled = true;
            DownstairLight.enabled = true;

            NormalSpriteProgressable.InverseLinearTransition(0.2f, 0f);
            NormalLight.enabled = false;

            DownstairSpriteRenderer.transform.localPosition = isHalfFloor ? new Vector3(-2.48f, -1.8f, 0) : new Vector3(3.13f, -4.15f, 0);
        }

        public void Downstair(bool isHalfFloor = false)
        {
            if (IsOnStair) return;
            else DownstairPrepare(isHalfFloor);

            if (m_DownstairCoroutine != null) StopCoroutine(m_DownstairCoroutine);
            m_DownstairCoroutine = StartCoroutine(DownstairCoroutine(isHalfFloor));
        }

        private Coroutine m_DownstairCoroutine = null;
        private bool m_IsDownstairing = false;
        private IEnumerator DownstairCoroutine(bool isHalfFloor)
        {
            var progress = 0f;
            DownstairAnimator.enabled = true;
            DownstairAnimator.speed = 0;

            var animation = isHalfFloor ? "HalfDownstair" : "FullDownstair";

            var currentFloorMask = FloorManager.Instance.GetCurrentFloor().Mask;
            var targetFloorMask = FloorManager.Instance.GetFloor(ImAtFloor - 1).Mask;

            while (progress < 1f)
            {
                DownstairAnimator.Play(animation, 0, progress);
                CameraStack.Instance.Offset -= new Vector3(0, 0.065f);

                if (currentFloorMask != null) currentFloorMask.Progress = progress;
                if (targetFloorMask != null) targetFloorMask.Progress = 1 - progress;

                yield return new WaitForSeconds(0.05f);
                progress += 1f / (isHalfFloor ? 75 : 120);

                if ((Mathf.RoundToInt(progress * 240) % 10 == 0 && !isHalfFloor) ||
                    (Mathf.RoundToInt(progress * 150) % 10 == 0 && isHalfFloor))
                {
                    PlayFootstepOnStair();
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
                    var stair = m_CurrentInteractTarget.GetComponent<StairInteractionController>();
                    TransportTo(stair.UpstairFinishPoint);
                    ImAtFloor = stair.FloorIndex + 1;
                }
                else if (m_IsDownstairing)
                {
                    var stair = m_CurrentInteractTarget.GetComponent<StairInteractionController>();
                    TransportTo(stair.DownstairFinishPoint);
                    ImAtFloor = stair.FloorIndex - 1;
                }
                else
                {
                    return;
                }
            }

            IsOnStair = false;
            UpstairSpriteRenderer.enabled = false;
            UpstairLight.enabled = false;
            DownstairSpriteRenderer.enabled = false;
            DownstairLight.enabled = false;

            spriteRenderer.enabled = true;
            NormalSpriteProgressable.LinearTransition(0.2f, 0f);
            NormalLight.enabled = true;
            
            CameraStack.Instance.Offset = new Vector3(0, 1.5f);

            m_IsUpstairing = m_IsDownstairing = false;

            GameManager.Instance.moveAction.Enable();
            GameProgressData.Instance.LastFloorIndex = ImAtFloor;

            if (m_UpstairCoroutine != null)
            {
                StopCoroutine(m_UpstairCoroutine);
                m_UpstairCoroutine = null;
            }
            if (m_DownstairCoroutine != null)
            {
                StopCoroutine(m_DownstairCoroutine);
                m_DownstairCoroutine = null;
            }
        }
        
    }

}