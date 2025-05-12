using System.Collections;
using Phosphorescence.DataSystem;
using Phosphorescence.Narration.Common;
using QFramework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace Phosphorescence.Narration
{
    public class AvatarOptionsComponent : NarrationComponent , ICanProcessLines
    {
        [Header("References")]
        public Image Avatar;
        public Animator AvatarAnimator;
        public Image Background;
        public Transform OptionParent;
        public GameObject OptionTemplate;

        [Header("Settings")]
        public bool IsLeft = false;

        private PlayableGraph _graph;
        private AnimationPlayableOutput animationOutputPlayable;

        private float m_SleepTime = 0f;
        private OnLinesReadEvent m_CurrentLineEvent;

        private void OnEnable()
        {
            _graph = PlayableGraph.Create();
            animationOutputPlayable = AnimationPlayableOutput.Create(_graph, "AvatarAnimation", AvatarAnimator);
        }

        private void OnDisable()
        {
            _graph.Destroy();
        }

        public void Initialize(OnLinesReadEvent e)
        {
            m_CurrentLineEvent = e;

            IsComplete = false;
            IsSkipped = false;
            m_SleepTime = e.tags.TryGetValue("sleep", out var sleepTag) ? float.Parse(sleepTag) : 0f;

            Clear();
            int index = 0;
            foreach (var line in e.lines)
            {
                SetOption(line, index++);
            }

            this.SetAvatar(e.tags.TryGetValue("avatar", out var avatarTag) ? avatarTag : "")
                .SetBackground(e.tags.TryGetValue("background_pic", out var backgroundPicTag) ? backgroundPicTag : "")
                .Process();
            
        }

        public void Process() => StartCoroutine(ProcessCoroutine());
        private IEnumerator ProcessCoroutine()
        {
            yield return new WaitUntil(() => IsComplete);

            yield return new WaitForSeconds(m_SleepTime);

            IsComplete = true;
        }

        private AvatarOptionsComponent SetAvatar(string avatar = "")
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

                        Avatar.rectTransform.anchoredPosition = IsLeft ? avatarConfig.positionOffsetForLeft : avatarConfig.positionOffsetForRight;
                        Avatar.transform.localScale = IsLeft ? avatarConfig.scaleOffsetForLeft : avatarConfig.scaleOffsetForRight;
                        Avatar.transform.localEulerAngles = IsLeft ? avatarConfig.rotationOffsetForLeft : avatarConfig.rotationOffsetForRight;

                        Avatar.color = Color.white;

                        break;
                    }
                    case TachiEType.Animation:
                    {
                        var _animClipPlayable = AnimationClipPlayable.Create(_graph, avatarConfig.animationClip);
                        animationOutputPlayable.SetSourcePlayable(_animClipPlayable);
                        _graph.Play();

                        Avatar.SetNativeSize();

                        Avatar.rectTransform.anchoredPosition = IsLeft ? avatarConfig.positionOffsetForLeft : avatarConfig.positionOffsetForRight;
                        Avatar.transform.localScale = IsLeft ? avatarConfig.scaleOffsetForLeft : avatarConfig.scaleOffsetForRight;
                        Avatar.transform.localEulerAngles = IsLeft ? avatarConfig.rotationOffsetForLeft : avatarConfig.rotationOffsetForRight;

                        Avatar.color = Color.white;

                        break;
                    }
                }
            }
            else Avatar.color = new Color(0, 0, 0, 0);

            return this;
        }

        private AvatarOptionsComponent SetBackground(string background = "")
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

        private void SetOption(OnLineReadEvent line, int index)
        {
            var option = Instantiate(OptionTemplate, OptionParent);

            option.GetComponent<TMP_Text>().SetText(line.content);
            option.GetComponent<Button>().onClick.AddListener(() => {
                TypeEventSystem.Global.Send(new SelectOptionEvent() { index = index });
                IsComplete = true;
            });

            option.SetActive(true);
        }

        private void Clear()
        {
            foreach (Transform child in OptionParent)
            {
                if (child == OptionTemplate.transform) continue;
                Destroy(child.gameObject);
            }
        }
    }
}