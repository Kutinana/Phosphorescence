using System;
using System.Collections;
using Common.SceneControl;
using Phosphorescence.DataSystem;
using Phosphorescence.Game;
using Phosphorescence.Others;
using QFramework;
using TMPro;
using UnityEngine;

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
            var content = m_CurrentLineEvent.content.Replace(" ", "").Replace("\n", "").Replace("\r", "").Split("$")[1].Split(":");
            var command = content[0];
            
            string[] args = Array.Empty<string>();
            if (content.Length > 1) args = content[1].Split(",");

            Debug.Log($"Command: {command} Args: {string.Join(", ", args)}");

            switch(command.ToLower())
            {
                case "enable_interaction":
                    GameManager.Instance.upStairAction.Enable();
                    GameManager.Instance.downStairAction.Enable();
                    GameManager.Instance.interactAction.Enable();
                    break;
                case "disable_interaction":
                    GameManager.Instance.upStairAction.Disable();
                    GameManager.Instance.downStairAction.Disable();
                    GameManager.Instance.interactAction.Disable();
                    break;
                case "enable_movement":
                    GameManager.Instance.moveAction.Enable();
                    break;
                case "disable_movement":
                    GameManager.Instance.moveAction.Disable();
                    break;
                case "enable_all_actions":
                    GameManager.Instance.moveAction.Enable();
                    GameManager.Instance.interactAction.Enable();
                    GameManager.Instance.upStairAction.Enable();
                    GameManager.Instance.downStairAction.Enable();
                    break;
                case "disable_all_actions":
                    GameManager.Instance.moveAction.Disable();
                    GameManager.Instance.interactAction.Disable();
                    GameManager.Instance.upStairAction.Disable();
                    GameManager.Instance.downStairAction.Disable();
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
                case "stop_music":
                    AudioKit.StopMusic();
                    break;
                case "sfx" when args.Length == 1:
                    GameDesignData.GetAudioData(args[0], out var sfxConfig);
                    if (sfxConfig != null)
                    {
                        AudioKit.PlaySound(sfxConfig.clip);
                    }
                    break;
                case "stop_sfx":
                    AudioKit.StopAllSound();
                    break;
                case "event" when args.Length == 1:
                    TypeEventSystem.Global.Send(new OnStoryEventTriggerEvent { eventName = args[0] });
                    break;
                case "read_variable" when args.Length == 1:
                    OnRequestReadVariable(args[0]);
                    break;
                case "set_tag" when args.Length == 1:
                    GameProgressData.Instance.AddPlotTag(args[0]);
                    break;
                case "read_tag" when args.Length == 1:
                    TypeEventSystem.Global.Send(new RequestSetVariableEvent {
                        variableName = args[0],
                        value = GameProgressData.Instance.HasPlotTag(args[0])
                    });
                    break;
                case "finish_ending_a":
                    FinishEndingA();
                    break;
                case "thank_page":
                    yield return SplashScreenController.Instance.MaskProgressable.LinearTransition(0.2f, 0f);
                    yield return SplashScreenController.Instance.ThanksProgressable.SmoothDamp(0.2f, out var coroutine);
                    yield return new WaitForSeconds(3f);
                    yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
                    yield return SplashScreenController.Instance.ThanksProgressable.InverseSmoothDamp(0.5f, out coroutine);
                    break;
            }
            IsComplete = true;
            if (IsAuto) TypeEventSystem.Global.Send<RequestNewLineEvent>();
        }

        private void OnRequestReadVariable(string variableName)
        {
            switch(variableName)
            {
                case "isBeaconOn":
                    TypeEventSystem.Global.Send(new RequestSetVariableEvent { variableName = "isBeaconOn", value = BeaconController.Instance.IsOn });
                    break;
            }
        }

        private void FinishEndingA()
        {
            GameProgressData.Instance.AddPlotTag("FinishedEndingA");
            SceneControl.Instance.FinishEndingA();
        }
    }
}