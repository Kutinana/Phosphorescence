using System.Linq;
using Kuchinashi.DataSystem;
using QFramework;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Phosphorescence.Game
{
    public class VideoSettingsController : MonoSingleton<VideoSettingsController>
    {
        public TMP_Dropdown ResolutionDropdown;
        private Resolution[] m_AllowedResolutions = new Resolution[] {
            new Resolution { width = 1920, height = 1080 },
            new Resolution { width = 1280, height = 720 },
            new Resolution { width = 800, height = 600 },
        };
        public Toggle FullscreenToggle;
        public Toggle NotFullscreenToggle;
        private bool m_IsFullscreen = false;

        private void Start()
        {
            ResolutionDropdown.options = m_AllowedResolutions.Select(resolution => new TMP_Dropdown.OptionData(resolution.width + "x" + resolution.height)).ToList();
            ResolutionDropdown.value = UserConfig.ReadWithDefaultValue<int>("Resolution", 0);
            FullscreenToggle.isOn = UserConfig.ReadWithDefaultValue<bool>("Fullscreen", true);
            NotFullscreenToggle.isOn = !FullscreenToggle.isOn;
            
            ResolutionDropdown.onValueChanged.AddListener(value => {
                UserConfig.Write("Resolution", value);
                UpdateResolution();
            });

            FullscreenToggle.onValueChanged.AddListener(value => {
                m_IsFullscreen = value;
                UserConfig.Write("Fullscreen", value);
                UpdateFullscreen();
            });
        }

        private void Initialize()
        {
            UpdateResolution();
            UpdateFullscreen();
        }

        private void UpdateResolution()
        {
            UserConfig.Write("Resolution", ResolutionDropdown.value);
            Screen.SetResolution(m_AllowedResolutions[ResolutionDropdown.value].width, m_AllowedResolutions[ResolutionDropdown.value].height, m_IsFullscreen);
        }

        private void UpdateFullscreen()
        {
            UserConfig.Write("Fullscreen", m_IsFullscreen);
            Screen.fullScreen = m_IsFullscreen;
        }
    }
}