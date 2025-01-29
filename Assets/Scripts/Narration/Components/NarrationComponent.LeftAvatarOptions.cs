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
    public class LeftAvatarOptionsComponent : NarrationComponent , ICanProcessLines
    {
        [Header("References")]
        public Image Avatar;
        public Image Background;
        public Transform OptionParent;
        public GameObject OptionTemplate;

        public void Process(OnLinesReadEvent e)
        {
            
            if (Avatar != null && e.tags.TryGetValue("avatar", out var avatarTag) && GameDesignData.GetTachiEData(avatarTag, out var avatarConfig))
            {
                Avatar.sprite = avatarConfig.sprite ?? null;

                Avatar.transform.localPosition = avatarConfig.positionOffset;
                Avatar.transform.localScale = avatarConfig.scaleOffset;
                Avatar.transform.localEulerAngles = avatarConfig.rotationOffset;

                Avatar.color = Color.white;
            }
            else if (Avatar != null) Avatar.color = new Color(0, 0, 0, 0);

            if (Background != null && e.tags.TryGetValue("background_pic", out var backgroundPicTag) && GameDesignData.GetBackgroundPicData(backgroundPicTag, out var backgroundConfig))
            {
                Background.sprite = backgroundConfig.sprite ?? null;

                Background.transform.localPosition = backgroundConfig.positionOffset;
                Background.transform.localScale = backgroundConfig.scaleOffset;
                Background.transform.localEulerAngles = backgroundConfig.rotationOffset;

                Background.color = Color.white;
            }
            else if (Background != null) Background.color = new Color(0, 0, 0, 0);

            Clear();

            int index = 0;
            foreach (var line in e.lines)
            {
                SetOption(line, index++);
            }
        }

        private void SetOption(OnLineReadEvent line, int index)
        {
            var option = Instantiate(OptionTemplate, OptionParent);

            option.GetComponent<TMP_Text>().SetText(line.content);
            option.GetComponent<Button>().onClick.AddListener(() => TypeEventSystem.Global.Send(new SelectOptionEvent() { index = index }));

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