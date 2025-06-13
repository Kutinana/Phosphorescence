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
        public AudioMixer AudioMixer;
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
                if (GlobalVolumeButton.image.sprite == MuteSprite)
                {
                    UserConfig.Write("GlobalVolume", 0.8f);
                    GlobalVolumeSlider.SetValueWithoutNotify(80);
                }
                else
                {
                    UserConfig.Write("GlobalVolume", 0f);
                    GlobalVolumeSlider.SetValueWithoutNotify(0);
                }
                UpdateGlobalVolume();
            });

            MusicVolumeButton.onClick.AddListener(() => {
                if (MusicVolumeButton.image.sprite == MuteSprite)
                {
                    UserConfig.Write("MusicVolume", 0.8f);
                    MusicVolumeSlider.SetValueWithoutNotify(80);
                }
                else
                {
                    UserConfig.Write("MusicVolume", 0f);
                    MusicVolumeSlider.SetValueWithoutNotify(0);
                }
                UpdateMusicVolume();
            });

            SFXVolumeButton.onClick.AddListener(() => {
                if (SFXVolumeButton.image.sprite == MuteSprite)
                {
                    UserConfig.Write("SFXVolume", 0.8f);
                    SFXVolumeSlider.SetValueWithoutNotify(80);
                }
                else
                {
                    UserConfig.Write("SFXVolume", 0f);
                    SFXVolumeSlider.SetValueWithoutNotify(0);
                }
                UpdateSFXVolume();
            });

            VoiceVolumeButton.onClick.AddListener(() => {
                if (VoiceVolumeButton.image.sprite == MuteSprite)
                {
                    UserConfig.Write("VoiceVolume", 0.8f);
                    VoiceVolumeSlider.SetValueWithoutNotify(80);
                }
                else
                {
                    UserConfig.Write("VoiceVolume", 0f);
                    VoiceVolumeSlider.SetValueWithoutNotify(0);
                }
                UpdateVoiceVolume();
            });

            GlobalVolumeSlider.onValueChanged.AddListener(value => {
                UserConfig.Write("GlobalVolume", value / 100f);
                UpdateGlobalVolume();
            });

            MusicVolumeSlider.onValueChanged.AddListener(value => {
                UserConfig.Write("MusicVolume", value / 100f);
                UpdateMusicVolume();
            });

            SFXVolumeSlider.onValueChanged.AddListener(value => {
                UserConfig.Write("SFXVolume", value / 100f);
                UpdateSFXVolume();
            });

            VoiceVolumeSlider.onValueChanged.AddListener(value => {
                UserConfig.Write("VoiceVolume", value / 100f);
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
            var value = UserConfig.ReadWithDefaultValue<float>("GlobalVolume", 0.8f);
            AudioMixer.SetFloat("MasterVolume", value == 0 ? -60 : Mathf.Log10(value + 0.2f) * 80);
            GlobalVolumeText.text = $"{value * 100:F0}%";
            GlobalVolumeSlider.SetValueWithoutNotify(value * 100);
            GlobalVolumeButton.image.sprite = value > 0 ? UnmuteSprite : MuteSprite;
        }

        private void UpdateMusicVolume()
        {
            var value = UserConfig.ReadWithDefaultValue<float>("MusicVolume", 0.8f);
            AudioMixer.SetFloat("MusicVolume", value == 0 ? -60 : Mathf.Log10(value + 0.2f) * 80);
            MusicVolumeText.text = $"{value * 100:F0}%";
            MusicVolumeSlider.SetValueWithoutNotify(value * 100);
            MusicVolumeButton.image.sprite = value > 0 ? UnmuteSprite : MuteSprite;
        }

        private void UpdateSFXVolume()
        {
            var value = UserConfig.ReadWithDefaultValue<float>("SFXVolume", 0.8f);
            AudioMixer.SetFloat("SFXVolume", value == 0 ? -60 : Mathf.Log10(value + 0.2f) * 80);
            SFXVolumeText.text = $"{value * 100:F0}%";
            SFXVolumeSlider.SetValueWithoutNotify(value * 100);
            SFXVolumeButton.image.sprite = value > 0 ? UnmuteSprite : MuteSprite;
        }

        private void UpdateVoiceVolume()
        {
            var value = UserConfig.ReadWithDefaultValue<float>("VoiceVolume", 0.8f);
            AudioMixer.SetFloat("VoiceVolume", value == 0 ? -60 : Mathf.Log10(value + 0.2f) * 80);
            VoiceVolumeText.text = $"{value * 100:F0}%";
            VoiceVolumeSlider.SetValueWithoutNotify(value * 100);
            VoiceVolumeButton.image.sprite = value > 0 ? UnmuteSprite : MuteSprite;
        }
    }
}