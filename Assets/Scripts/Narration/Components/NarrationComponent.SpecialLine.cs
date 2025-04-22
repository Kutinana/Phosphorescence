using System;
using System.Collections;
using Phosphorescence.DataSystem;
using Phosphorescence.Game;
using Phosphorescence.Narration.Common;
using QFramework;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.UI;

namespace Phosphorescence.Narration
{
    public class SpecialLineComponent : NarrationComponent , ICanProcessLine
    {
        private OnLineReadEvent m_CurrentLineEvent;

        public void Initialize(OnLineReadEvent e)
        {
            m_CurrentLineEvent = e;

            IsComplete = false;
            IsSkipped = false;
            IsAuto = !e.tags.TryGetValue("auto", out var auto) || auto == "true";

            SkipAction = null;

            this.Process();
        }

        public void Process() => StartCoroutine(ProcessCoroutine());
        private IEnumerator ProcessCoroutine()
        {
            var content = m_CurrentLineEvent.content.ToLower().Replace(" ", "").Replace("\n", "").Replace("\r", "").Split("$")[1].Split(":");
            var command = content[0];
            
            string[] args = Array.Empty<string>();
            if (content.Length > 1) args = content[1].Split(",");

            Debug.Log($"Command: {command} Args: {string.Join(", ", args)}");

            switch(command)
            {
                case "enable_interaction":
                    GameManager.Instance.moveAction.Enable();
                    GameManager.Instance.climbAction.Enable();
                    GameManager.Instance.interactAction.Enable();
                    break;
                case "disable_interaction":
                    GameManager.Instance.moveAction.Disable();
                    GameManager.Instance.climbAction.Disable();
                    GameManager.Instance.interactAction.Disable();
                    break;
                case "sleep" when args.Length == 1:
                    yield return new WaitForSeconds(float.Parse(args[0]));
                    break;
                case "music" when args.Length == 1:
                    GameDesignData.GetAudioData(args[0], out var audioConfig);
                    if (audioConfig != null)
                    {
                        AudioKit.PlayMusic(audioConfig.clip, loop: audioConfig.isLoop);
                    }
                    break;
                case "event" when args.Length == 1:
                    TypeEventSystem.Global.Send(new OnStoryEventTriggerEvent { eventName = args[0] });
                    break;
            }

            IsComplete = true;
            if (IsAuto) TypeEventSystem.Global.Send<RequestNewLineEvent>();
        }
    }
}