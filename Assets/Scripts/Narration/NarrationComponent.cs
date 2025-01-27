using System;
using System.Collections;
using System.Collections.Generic;
using Kuchinashi;
using Kuchinashi.Utils.Progressable;
using Phosphorescence.DataSystem;
using Phosphorescence.Narration.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Phosphorescence.Narration
{
    public class NarrationComponent : MonoBehaviour
    {
        [Header("References")]
        public CanvasGroupAlphaProgressable CanvasGroup;
        public Image Avatar;
        public Image Background;
        public TMP_Text Text;

        private Coroutine m_CurrentTypeTextCoroutine;

        public void SetNarration(string avatar, string content)
        {
            try
            {
                if (GameDesignData.GetTachiEData(avatar, out var config))
                {
                    Avatar.sprite = config.sprite ?? null;

                    Avatar.transform.localPosition = config.positionOffset;
                    Avatar.transform.localScale = config.scaleOffset;
                    Avatar.transform.localEulerAngles = config.rotationOffset;

                    Avatar.color = Color.white;
                }
                else Avatar.color = new Color(0, 0, 0, 0);

                Background.color = new Color(0, 0, 0, 0f);

                Text.SetText("");
                if (m_CurrentTypeTextCoroutine != null) StopCoroutine(m_CurrentTypeTextCoroutine);
                m_CurrentTypeTextCoroutine = StartCoroutine(TypeTextCoroutine(Text, content));
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        private IEnumerator TypeTextCoroutine(TMP_Text textfield, string text, float speed = 1 / 24f)
        {
            var len = text.Length;

            for (var i = 0; i < len; i++)
            {
                textfield.text += text[i];
                // if (text[i] is not ' ' or '\r' or '\n') AudioKit.PlaySound("InteractClick");
                yield return new WaitForSeconds(speed);
            }
            textfield.SetText(text);

            m_CurrentTypeTextCoroutine = null;
        }
    }
}