using Kuchinashi.DataSystem;
using QFramework;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Phosphorescence.Game
{
    public class AudioSettingsController : MonoSingleton<AudioSettingsController>
    {
        public Sprite MuteSprite;
        public Sprite UnmuteSprite;

        [Header("Global Volume")]
        public Slider GlobalVolumeSlider;
        public TMP_Text GlobalVolumeText;
        public Button GlobalVolumeButton;

        [Header("Music Volume")]
        public Slider MusicVolumeSlider;
        public TMP_Text MusicVolumeText;
        public Button MusicVolumeButton;

        [Header("SFX Volume")]
        public Slider SFXVolumeSlider;
        public TMP_Text SFXVolumeText;
        public Button SFXVolumeButton;

        [Header("Voice Volume")]
        public Slider VoiceVolumeSlider;
        public TMP_Text VoiceVolumeText;
        public Button VoiceVolumeButton;

        private void Start()
        {
            GlobalVolumeButton.onClick.AddListener(() => {
                var isMutting = GlobalVolumeButton.image.sprite != MuteSprite;

                Audio.AudioManager.SetMixerGlobalVolume(isMutting ? 0f : 0.8f);
                UpdateGlobalVolume();
            });

            MusicVolumeButton.onClick.AddListener(() => {
                var isMutting = MusicVolumeButton.image.sprite != MuteSprite;

                Audio.AudioManager.SetMixerMusicVolume(isMutting ? 0f : 0.8f);
                UpdateMusicVolume();
            });

            SFXVolumeButton.onClick.AddListener(() => {
                var isMutting = SFXVolumeButton.image.sprite != MuteSprite;

                Audio.AudioManager.SetMixerSFXVolume(isMutting ? 0f : 0.8f);
                UpdateSFXVolume();
            });

            VoiceVolumeButton.onClick.AddListener(() => {
                var isMutting = VoiceVolumeButton.image.sprite != MuteSprite;

                Audio.AudioManager.SetMixerVoiceVolume(isMutting ? 0f : 0.8f);
                UpdateVoiceVolume();
            });

            GlobalVolumeSlider.onValueChanged.AddListener(value => {
                Audio.AudioManager.SetMixerGlobalVolume(value / 100f);
                UpdateGlobalVolume();
            });

            MusicVolumeSlider.onValueChanged.AddListener(value => {
                Audio.AudioManager.SetMixerMusicVolume(value / 100f);
                UpdateMusicVolume();
            });

            SFXVolumeSlider.onValueChanged.AddListener(value => {
                Audio.AudioManager.SetMixerSFXVolume(value / 100f);
                UpdateSFXVolume();
            });

            VoiceVolumeSlider.onValueChanged.AddListener(value => {
                Audio.AudioManager.SetMixerVoiceVolume(value / 100f);
                UpdateVoiceVolume();
            });
            
            Initialize();
        }

        private void Initialize()
        {
            UpdateGlobalVolume();
            UpdateMusicVolume();
            UpdateSFXVolume();
            UpdateVoiceVolume();
        }

        private void UpdateGlobalVolume()
        {
            var value = Audio.AudioManager.MixerGlobalVolume;

            GlobalVolumeText.text = $"{value * 100:F0}%";
            GlobalVolumeSlider.SetValueWithoutNotify(value * 100);
            GlobalVolumeButton.image.sprite = value > 0 ? UnmuteSprite : MuteSprite;
        }

        private void UpdateMusicVolume()
        {
            var value = Audio.AudioManager.MixerMusicVolume;

            MusicVolumeText.text = $"{value * 100:F0}%";
            MusicVolumeSlider.SetValueWithoutNotify(value * 100);
            MusicVolumeButton.image.sprite = value > 0 ? UnmuteSprite : MuteSprite;
        }

        private void UpdateSFXVolume()
        {
            var value = Audio.AudioManager.MixerSFXVolume;
            
            SFXVolumeText.text = $"{value * 100:F0}%";
            SFXVolumeSlider.SetValueWithoutNotify(value * 100);
            SFXVolumeButton.image.sprite = value > 0 ? UnmuteSprite : MuteSprite;
        }

        private void UpdateVoiceVolume()
        {
            var value = Audio.AudioManager.MixerVoiceVolume;

            VoiceVolumeText.text = $"{value * 100:F0}%";
            VoiceVolumeSlider.SetValueWithoutNotify(value * 100);
            VoiceVolumeButton.image.sprite = value > 0 ? UnmuteSprite : MuteSprite;
        }
    }
}