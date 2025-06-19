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
    public class FullScreenOptionsComponent : NarrationComponent , ICanProcessLines
    {
        [Header("References")]
        public Image Background;
        public Image BackgroundOverlay;
        public Transform OptionParent;
        public GameObject OptionTemplate;

        private float m_SleepTime = 0f;
        private OnLinesReadEvent m_CurrentLinesEvent;

        public void Initialize(OnLinesReadEvent e)
        {
            m_CurrentLinesEvent = e;

            IsComplete = false;
            IsSkipped = false;
            m_SleepTime = e.tags.TryGetValue("sleep", out var sleepTag) ? float.Parse(sleepTag) : 0f;

            Clear();
            int index = 0;
            foreach (var line in e.lines)
            {
                Debug.Log(line.content);
                SetOption(line, index++);
            }

            this.SetBackground(e.tags.TryGetValue("background_pic", out var backgroundPicTag) ? backgroundPicTag : "")
                .SetOpacity(e.tags.TryGetValue("opacity", out var opacityTag) ? float.Parse(opacityTag) : 0.8f)
                .Process();
        }

        public void Process() => StartCoroutine(ProcessCoroutine());
        private IEnumerator ProcessCoroutine()
        {
            yield return new WaitUntil(() => IsComplete);

            yield return new WaitForSeconds(m_SleepTime);

            IsComplete = true;
        }

        private FullScreenOptionsComponent SetBackground(string background = "")
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

        private FullScreenOptionsComponent SetOpacity(float opacity = 0.8f)
        {
            if (BackgroundOverlay != null) BackgroundOverlay.color = new Color(0, 0, 0, opacity);
            return this;
        }

        private FullScreenOptionsComponent SetOption(OnLineReadEvent line, int index)
        {
            var option = Instantiate(OptionTemplate, OptionParent);

            option.GetComponentInChildren<TMP_Text>().SetText(line.content);
            option.GetComponent<Button>().onClick.AddListener(() => {
                TypeEventSystem.Global.Send(new SelectOptionEvent() { index = index });
                IsComplete = true;
            });

            option.SetActive(true);

            return this;
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