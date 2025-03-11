using System;
using System.Collections;
using System.Collections.Generic;
using Kuchinashi;
using Kuchinashi.Utils.Progressable;
using Phosphorescence.DataSystem;
using Phosphorescence.Narration.Common;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
using UnityEngine.UI;

namespace Phosphorescence.Narration
{
    public class AvatarTextComponent : NarrationComponent , ICanProcessLine
    {
        [Header("References")]
        public Image Avatar;
        public Image Background;
        public TMP_Text Text;
        public Animator AvatarAnimator;

        private PlayableGraph _graph;
        private AnimationPlayableOutput animationOutputPlayable;

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

        public void Process(OnLineReadEvent e)
        {
            SetNarration(
                e.content,
                avatar: e.tags.TryGetValue("avatar", out var avatar) ? avatar : "",
                background: e.tags.TryGetValue("background_pic", out var backgroundPic) ? backgroundPic : ""
            );
        }

        private void SetNarration(string content, string avatar = "", string background = "")
        {
            try
            {
                if (Avatar != null && GameDesignData.GetTachiEData(avatar, out var avatarConfig))
                {
                    switch (avatarConfig.type)
                    {
                        case TachiEType.Static:
                        {
                            Avatar.sprite = avatarConfig.sprite ?? null;
                            Avatar.SetNativeSize();

                            Avatar.rectTransform.anchoredPosition = avatarConfig.positionOffset;
                            Avatar.transform.localScale = avatarConfig.scaleOffset;
                            Avatar.transform.localEulerAngles = avatarConfig.rotationOffset;

                            Avatar.color = Color.white;

                            break;
                        }
                        case TachiEType.Animation:
                        {
                            var _animClipPlayable = AnimationClipPlayable.Create(_graph, avatarConfig.animationClip);
                            animationOutputPlayable.SetSourcePlayable(_animClipPlayable);
                            _graph.Play();

                            Avatar.SetNativeSize();

                            Avatar.rectTransform.anchoredPosition = avatarConfig.positionOffset;
                            Avatar.transform.localScale = avatarConfig.scaleOffset;
                            Avatar.transform.localEulerAngles = avatarConfig.rotationOffset;

                            Avatar.color = Color.white;

                            break;
                        }
                    }
                }
                else if (Avatar != null) Avatar.color = new Color(0, 0, 0, 0);

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