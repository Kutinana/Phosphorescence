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
using UnityEngine.Video;

namespace Phosphorescence.Narration
{
    public class FullScreenVideoComponent : NarrationComponent , ICanProcessLine
    {
        [Header("References")]
        public RawImage Renderer;
        public VideoPlayer VideoPlayer;

        private float m_SleepTime = 0f;
        private VideoData videoConfig;

        private OnLineReadEvent m_CurrentLineEvent;

        public void Initialize(OnLineReadEvent e)
        {
            m_CurrentLineEvent = e;

            IsComplete = false;
            IsSkipped = false;
            IsAuto = e.tags.TryGetValue("auto", out var auto) && auto == "true";
            SkipAction = null;
            m_SleepTime = e.tags.TryGetValue("sleep", out var sleep) ? float.Parse(sleep) : 0f;

            this.SetVideo(e.tags.TryGetValue("video", out var video) ? video : "")
                .SetOpacity(e.tags.TryGetValue("opacity", out var opacity) ? float.Parse(opacity) : 1f)
                .SetSkippable(!e.tags.TryGetValue("skippable", out var skippable) || skippable == "true")
                .Process();
        }

        public void Process() => StartCoroutine(ProcessCoroutine());
        private IEnumerator ProcessCoroutine()
        {
            VideoPlayer.Play();
            yield return new WaitUntil(() => VideoPlayer.isPlaying);

            yield return new WaitUntil(() => IsSkipped || VideoPlayer.isPlaying == false);

            yield return new WaitForSeconds(m_SleepTime);

            IsComplete = true;
            if (IsAuto) TypeEventSystem.Global.Send<RequestNewLineEvent>();
        }

        private FullScreenVideoComponent SetVideo(string video = "")
        {
            if (Renderer == null) return this;

            if (GameDesignData.GetData<VideoData>(video, out videoConfig))
            {
                VideoPlayer.clip = videoConfig.videoClip;

                Renderer.rectTransform.localPosition = videoConfig.positionOffset;
                Renderer.rectTransform.localScale = videoConfig.scaleOffset;
                Renderer.rectTransform.localEulerAngles = videoConfig.rotationOffset;

                Renderer.color = Color.white;
            }
            else Renderer.color = new Color(0, 0, 0, 0);

            return this;
        }

        private FullScreenVideoComponent SetOpacity(float opacity = 1f)
        {
            if (Renderer != null) Renderer.color = new Color(1, 1, 1, opacity);
            return this;
        }

        private FullScreenVideoComponent SetSkippable(bool skippable = true)
        {
            if (!skippable)
            {
                SkipAction = null;
                return this;
            }

            SkipAction = () => {
                VideoPlayer.Stop();
                
                IsSkipped = true;
                SkipAction = null;
            };

            return this;
        }
    }
}