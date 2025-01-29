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
    public class FullScreenTextComponent : NarrationComponent , ICanProcessLine
    {
        [Header("References")]
        public Image Background;
        public TMP_Text Text;

        private Coroutine m_CurrentTypeTextCoroutine;

        public void Process(OnLineReadEvent e)
        {
            SetNarration(
                e.content,
                background: e.tags.TryGetValue("background_pic", out var backgroundPic) ? backgroundPic : ""
            );
        }

        private void SetNarration(string content, string background = "")
        {
            try
            {
                if (Background != null && GameDesignData.GetBackgroundPicData(background, out var backgroundConfig))
                {
                    Background.sprite = backgroundConfig.sprite ?? null;

                    Background.transform.localPosition = backgroundConfig.positionOffset;
                    Background.transform.localScale = backgroundConfig.scaleOffset;
                    Background.transform.localEulerAngles = backgroundConfig.rotationOffset;

                    Background.color = Color.white;
                }
                else if (Background != null) Background.color = new Color(0, 0, 0, 0);

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