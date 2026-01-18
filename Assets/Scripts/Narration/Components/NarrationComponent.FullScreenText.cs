using System;
using System.Collections;
using System.Collections.Generic;
using Kuchinashi;
using Kuchinashi.Utils.Progressable;
using Phosphorescence.Audio;
using Phosphorescence.DataSystem;
using Phosphorescence.Narration.Common;
using QFramework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Phosphorescence.Narration
{
    public class FullScreenTextComponent : NarrationComponent , ICanProcessLine
    {
        [Header("References")]
        public Image Background;
        public Image BackgroundOverlay;
        public TMP_Text Text;

        private float m_SleepTime = 0f;
        private AudioData voiceConfig;

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

            Text.SetText("");

            this.SetBackground(e.tags.TryGetValue("background_pic", out var backgroundPic) ? backgroundPic : "")
                .SetOpacity(e.tags.TryGetValue("opacity", out var opacity) ? float.Parse(opacity) : 0f)
                .SetSkippable(!e.tags.TryGetValue("skippable", out var skippable) || skippable == "true")
                .Process();
        }

        public void Process() => StartCoroutine(ProcessCoroutine());
        private IEnumerator ProcessCoroutine()
        {
            if (m_CurrentTypeTextCoroutine != null) StopCoroutine(m_CurrentTypeTextCoroutine);
            m_CurrentTypeTextCoroutine = StartCoroutine(TypeTextCoroutine(Text, m_CurrentLineEvent.content));

            yield return new WaitUntil(() => IsSkipped || m_CurrentTypeTextCoroutine == null);

            yield return new WaitForSeconds(m_SleepTime);

            IsComplete = true;
            if (IsAuto) TypeEventSystem.Global.Send<RequestNewLineEvent>();
        }

        private FullScreenTextComponent SetBackground(string background = "")
        {
            if (Background == null) return this;

            if (GameDesignData.GetBackgroundPicData(background, out var backgroundConfig))
            {
                Background.sprite = backgroundConfig.sprite ?? null;
                Background.SetNativeSize();

                Background.rectTransform.localPosition = backgroundConfig.positionOffset;
                Background.transform.localScale = backgroundConfig.scaleOffset;
                Background.transform.localEulerAngles = backgroundConfig.rotationOffset;

                Background.color = Color.white;
            }
            else Background.color = new Color(0, 0, 0, 0);

            return this;
        }

        private FullScreenTextComponent SetContent(string content)
        {
            Text.SetText(content);
            return this;
        }

        private FullScreenTextComponent SetOpacity(float opacity = 0f)
        {
            if (BackgroundOverlay != null) BackgroundOverlay.color = new Color(0, 0, 0, opacity);
            return this;
        }

        private IEnumerator TypeTextCoroutine(TMP_Text textfield, string text, float speed = 1 / 24f)
        {
            var len = text.Length;

            AudioSource source = null;
            if (voiceConfig != null) source = AudioManager.PlayVoice(voiceConfig.clip, loop: false, volume: voiceConfig.standardVolume);

            for (var i = 0; i < len; i++)
            {
                textfield.text += text[i];
                // if (text[i] is not ' ' or '\r' or '\n') AudioKit.PlaySound("InteractClick");
                yield return new WaitForSeconds(speed);
            }
            textfield.SetText(text);

            if (voiceConfig != null) yield return new WaitUntil(() => !source.isPlaying);

            m_CurrentTypeTextCoroutine = null;
        }

        private FullScreenTextComponent SetVoice(string voice = "")
        {
            voiceConfig = null;
            GameDesignData.GetAudioData(voice, out voiceConfig);

            return this;
        }

        private FullScreenTextComponent SetSkippable(bool skippable = true)
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