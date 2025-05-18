using System;
using System.Collections;
using System.Collections.Generic;
using Kuchinashi;
using Kuchinashi.Utils.Progressable;
using Phosphorescence.DataSystem;
using Phosphorescence.Narration.Common;
using QFramework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Phosphorescence.Narration
{
    public class SubtitleTextComponent : NarrationComponent , ICanProcessLine
    {
        [Header("References")]
        public TMP_Text Text;

        private float m_SleepTime = 0f;
        private float m_Duration = 3f;

        private OnLineReadEvent m_CurrentLineEvent;
        private Coroutine m_CurrentTypeTextCoroutine;

        public void Initialize(OnLineReadEvent e)
        {
            m_CurrentLineEvent = e;

            IsComplete = false;
            IsSkipped = false;
            IsAuto = e.tags.TryGetValue("auto", out var auto) && auto == "true";
            SkipAction = null;

            m_SleepTime = e.tags.TryGetValue("sleep", out var sleep) ? float.Parse(sleep) : 0f;
            m_Duration = e.tags.TryGetValue("duration", out var duration) ? float.Parse(duration) : 3f;

            this.SetSkippable(!e.tags.TryGetValue("skippable", out var skippable) || skippable == "true")
                .Process();
        }

        public void Process() => StartCoroutine(ProcessCoroutine());
        private IEnumerator ProcessCoroutine()
        {
            if (m_CurrentTypeTextCoroutine != null) StopCoroutine(m_CurrentTypeTextCoroutine);
            m_CurrentTypeTextCoroutine = StartCoroutine(FadeTextCoroutine(Text, m_CurrentLineEvent.content, m_Duration));

            yield return new WaitUntil(() => IsSkipped || m_CurrentTypeTextCoroutine == null);

            yield return new WaitForSeconds(m_SleepTime);

            IsComplete = true;
            if (IsAuto) TypeEventSystem.Global.Send<RequestNewLineEvent>();
        }

        private SubtitleTextComponent SetContent(string content)
        {
            Text.SetText(content);
            return this;
        }

        private IEnumerator FadeTextCoroutine(TMP_Text textfield, string content, float duration = 3f)
        {
            textfield.alpha = 0f;
            textfield.SetText(content);

            while (!Mathf.Approximately(textfield.alpha, 1f))
            {
                textfield.alpha = Mathf.MoveTowards(textfield.alpha, 1f, 0.01f);
                yield return null;
            }
            textfield.alpha = 1f;

            var actualDuration = duration != 3f ? duration : content.Length * 0.15f > 3f ? content.Length * 0.15f : 3f;
            yield return new WaitForSeconds(actualDuration);

            while (!Mathf.Approximately(textfield.alpha, 0f))
            {
                textfield.alpha = Mathf.MoveTowards(textfield.alpha, 0f, 0.01f);
                yield return null;
            }
            textfield.alpha = 0f;

            m_CurrentTypeTextCoroutine = null;
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

        private SubtitleTextComponent SetSkippable(bool skippable = true)
        {
            if (!skippable)
            {
                SkipAction = null;
                return this;
            }

            SkipAction = () => {
                if (m_CurrentTypeTextCoroutine != null) StopCoroutine(m_CurrentTypeTextCoroutine);
                SetContent(m_CurrentLineEvent.content);
                
                IsSkipped = true;
                SkipAction = null;
            };

            return this;
        }
    }
}