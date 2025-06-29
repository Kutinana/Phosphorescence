using System;
using System.Collections;
using System.Collections.Generic;
using Kuchinashi.Utils.Progressable;
using Phosphorescence.DataSystem;
using Phosphorescence.Narration.Common;
using QFramework;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
using UnityEngine.UI;

namespace Phosphorescence.Narration
{
    public class AvatarTextComponent : NarrationComponent , ICanProcessLine
    {
        [Header("Avatar")]
        public Image Avatar;
        public ProgressableGroup AvatarProgressableGroup;
        public ImageColorProgressable AvatarColorProgressable;
        public RectPositionProgressable AvatarPositionProgressable;

        [Header("References")]
        public Image Background;
        public TMP_Text Text;
        public TMP_Text Speaker;
        public Animator AvatarAnimator;

        [Header("Settings")]
        public bool IsLeft = false;

        private PlayableGraph _graph;
        private AnimationPlayableOutput animationOutputPlayable;

        private float m_SleepTime = 0f;
        private float m_Speed = 1 / 24f;
        private AudioData voiceConfig;
        private AudioData simulatedVoiceConfig;
        private List<char> omittedChars = new List<char> { ' ', '\r', '\n', '\"', '“', '”', '…' };

        private OnLineReadEvent m_CurrentLineEvent;
        private Coroutine m_CurrentTypeTextCoroutine;

        private void OnEnable()
        {
            _graph = PlayableGraph.Create();
            animationOutputPlayable = AnimationPlayableOutput.Create(_graph, "AvatarAnimation", AvatarAnimator);
        }

        private void OnDisable()
        {
            _graph.Destroy();
        }

        public override void OnExit()
        {
            AvatarProgressableGroup.Progress = 0f;
        }

        public void Initialize(OnLineReadEvent e)
        {
            m_CurrentLineEvent = e;

            IsComplete = false;
            IsSkipped = false;
            IsAuto = e.tags.TryGetValue("auto", out var auto) && auto == "true";
            SkipAction = null;
            m_SleepTime = 0f;

            this.SetAvatar(e.tags.TryGetValue("avatar", out var avatar) ? avatar : "")
                .SetAvatarOpacity(e.tags.TryGetValue("avatar_opacity", out var avatarOpacity) ? float.Parse(avatarOpacity) : 1f)
                .SetSpeaker(e.tags.TryGetValue("speaker", out var speaker) ? speaker : "")
                .SetBackground(e.tags.TryGetValue("background_pic", out var backgroundPic) ? backgroundPic : "")
                .SetVoice(e.tags.TryGetValue("voice", out var voice) ? voice : "")
                .SetSimulatedVoice(e.tags.TryGetValue("simulated_voice", out var simulatedVoice) ? simulatedVoice : "")
                .SetSkippable(!e.tags.TryGetValue("skippable", out var skippable) || skippable == "true")
                .SetSleepTime(e.tags.TryGetValue("sleep", out var sleep) ? float.Parse(sleep) : 0f)
                .SetSpeed(e.tags.TryGetValue("speed", out var speed) ? float.Parse(speed) : 1 / 24f)
                .Process();
        }

        public void Process() => StartCoroutine(ProcessCoroutine());
        private IEnumerator ProcessCoroutine()
        {
            if (m_CurrentTypeTextCoroutine != null) StopCoroutine(m_CurrentTypeTextCoroutine);
            m_CurrentTypeTextCoroutine = StartCoroutine(TypeTextCoroutine(Text, m_CurrentLineEvent.content));

            AvatarProgressableGroup.LinearTransition(0.2f);

            yield return new WaitUntil(() => IsSkipped || m_CurrentTypeTextCoroutine == null);

            yield return new WaitForSeconds(m_SleepTime);

            IsComplete = true;

            if (IsAuto) TypeEventSystem.Global.Send<RequestNewLineEvent>();
        }

        private IEnumerator TypeTextCoroutine(TMP_Text textfield, string text)
        {
            textfield.SetText("");
            textfield.ForceMeshUpdate();

            var len = text.Length;

            AudioSource source = null;
            if (voiceConfig != null)
                source = Audio.AudioManager.PlayVoice(voiceConfig.clip, loop: false, volume: voiceConfig.standardVolume);

            for (var i = 0; i < len; i++)
            {
                textfield.text += text[i];
                if (!omittedChars.Contains(text[i]) && simulatedVoiceConfig != null)
                    Audio.AudioManager.PlayVoice(simulatedVoiceConfig.clip, loop: false, volume: simulatedVoiceConfig.standardVolume);
                yield return new WaitForSeconds(m_Speed);
            }
            textfield.SetText(text);

            if (voiceConfig != null) yield return new WaitUntil(() => !source.isPlaying);
            m_CurrentTypeTextCoroutine = null;
        }

        private AvatarTextComponent SetContent(string content)
        {
            Text.SetText(content);
            return this;
        }

        private AvatarTextComponent SetSpeaker(string speaker = "")
        {
            if (Speaker == null) return this;

            Speaker.SetText(speaker);
            return this;
        }

        private AvatarTextComponent SetAvatar(string avatar = "")
        {
            if (Avatar == null) return this;

            if (GameDesignData.GetTachiEData(avatar, out var avatarConfig))
            {
                switch (avatarConfig.type)
                {
                    case TachiEType.Static:
                    {
                        Avatar.sprite = avatarConfig.sprite ?? null;
                        Avatar.SetNativeSize();

                        AvatarPositionProgressable.StartPosition = IsLeft ? avatarConfig.positionOffsetForLeft : avatarConfig.positionOffsetForRight;
                        AvatarPositionProgressable.StartPosition += new Vector2(0, -50);
                        AvatarPositionProgressable.EndPosition = IsLeft ? avatarConfig.positionOffsetForLeft : avatarConfig.positionOffsetForRight;

                        Avatar.transform.localScale = IsLeft ? avatarConfig.scaleOffsetForLeft : avatarConfig.scaleOffsetForRight;
                        Avatar.transform.localEulerAngles = IsLeft ? avatarConfig.rotationOffsetForLeft : avatarConfig.rotationOffsetForRight;

                        AvatarColorProgressable.StartColor = new Color(1, 1, 1, 0);
                        AvatarColorProgressable.EndColor = Color.white;

                        break;
                    }
                    case TachiEType.Animation:
                    {
                        var _animClipPlayable = AnimationClipPlayable.Create(_graph, avatarConfig.animationClip);
                        animationOutputPlayable.SetSourcePlayable(_animClipPlayable);
                        _graph.Play();

                        Avatar.SetNativeSize();

                        AvatarPositionProgressable.StartPosition = IsLeft ? avatarConfig.positionOffsetForLeft : avatarConfig.positionOffsetForRight;
                        AvatarPositionProgressable.StartPosition += new Vector2(0, -50);
                        AvatarPositionProgressable.EndPosition = IsLeft ? avatarConfig.positionOffsetForLeft : avatarConfig.positionOffsetForRight;

                        Avatar.transform.localScale = IsLeft ? avatarConfig.scaleOffsetForLeft : avatarConfig.scaleOffsetForRight;
                        Avatar.transform.localEulerAngles = IsLeft ? avatarConfig.rotationOffsetForLeft : avatarConfig.rotationOffsetForRight;

                        AvatarColorProgressable.StartColor = new Color(1, 1, 1, 0);
                        AvatarColorProgressable.EndColor = Color.white;

                        break;
                    }
                }
            }
            else
            {
                Avatar.sprite = null;
                AvatarColorProgressable.StartColor = new Color(0, 0, 0, 0);
                AvatarColorProgressable.EndColor = new Color(0, 0, 0, 0);
            }

            return this;
        }

        private AvatarTextComponent SetBackground(string background = "")
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

        private AvatarTextComponent SetAvatarOpacity(float opacity = 1f)
        {
            if (Avatar == null || Avatar.sprite == null) return this;
            
            AvatarColorProgressable.EndColor = new Color(1, 1, 1, opacity);
            return this;
        }

        private AvatarTextComponent SetVoice(string voice = "")
        {
            voiceConfig = null;
            GameDesignData.GetAudioData(voice, out voiceConfig);

            return this;
        }

        private AvatarTextComponent SetSimulatedVoice(string simulatedVoice = "")
        {
            simulatedVoiceConfig = null;
            GameDesignData.GetAudioData(simulatedVoice, out simulatedVoiceConfig);

            return this;
        }

        private AvatarTextComponent SetSkippable(bool skippable = true)
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

        private AvatarTextComponent SetSleepTime(float sleepTime = 0f)
        {
            m_SleepTime = sleepTime;
            return this;
        }

        private AvatarTextComponent SetSpeed(float speed = 1 / 24f)
        {
            m_Speed = speed;
            return this;
        }
    }
}