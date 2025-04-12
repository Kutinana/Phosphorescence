using System;
using System.Collections;
using Phosphorescence.DataSystem;
using Phosphorescence.Narration.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Phosphorescence.Narration
{
    public class EmptyLineComponent : NarrationComponent , ICanProcessLine
    {
        [Header("References")]
        public Image Background;

        private OnLineReadEvent m_CurrentLineEvent;
        private Coroutine m_CurrentSleepCoroutine;

        private void OnEnable()
        {
            
        }

        public void Process(OnLineReadEvent e)
        {
            m_CurrentLineEvent = e;
            IsComplete = false;

            this.SetBackground(e.tags.TryGetValue("background_pic", out var backgroundPic) ? backgroundPic : "")
                .SetSleep(e.tags.TryGetValue("sleep", out var sleep) ? float.Parse(sleep) : 1f)
                .SetSkippable(!e.tags.TryGetValue("skippable", out var skippable) || skippable == "true");
        }

        private EmptyLineComponent SetSleep(float duration = 1f)
        {
            if (duration <= 0) return this;

            if (m_CurrentSleepCoroutine != null) StopCoroutine(m_CurrentSleepCoroutine);
            m_CurrentSleepCoroutine = StartCoroutine(SleepCoroutine(duration));

            return this;
        }

        private IEnumerator SleepCoroutine(float duration = 1f)
        {
            yield return new WaitForSeconds(duration);

            m_CurrentSleepCoroutine = null;
            IsComplete = true;
        }

        private EmptyLineComponent SetBackground(string background = "")
        {
            if (Background == null) return this;

            if (GameDesignData.GetBackgroundPicData(background, out var backgroundConfig))
            {
                Background.sprite = backgroundConfig.sprite ?? null;

                Background.transform.localPosition = backgroundConfig.positionOffset;
                Background.transform.localScale = backgroundConfig.scaleOffset;
                Background.transform.localEulerAngles = backgroundConfig.rotationOffset;

                Background.color = Color.white;
            }
            else Background.color = new Color(0, 0, 0, 0);

            return this;
        }

        private EmptyLineComponent SetSkippable(bool skippable = true)
        {
            if (!skippable)
            {
                SkipAction = null;
                return this;
            }

            SkipAction = () => {
                if (m_CurrentSleepCoroutine != null) StopCoroutine(m_CurrentSleepCoroutine);
                
                IsComplete = true;
                SkipAction = null;
            };

            return this;
        }
    }
}