using QFramework;

namespace Phosphorescence.Game
{
    public class SettingsModel : AbstractModel
    {
        # region Video
        public bool IsFullScreen;
        public int ResolutionWidth;
        public int ResolutionHeight;
        public int FrameRate;
        #endregion

        # region Audio
        public bool IsMusicOn;
        public float MusicVolume;
        public bool IsSFXOn;
        public float SFXVolume;
        #endregion

        protected override void OnInit()
        {
            IsFullScreen = false;
            ResolutionWidth = 1920;
            ResolutionHeight = 1080;
            FrameRate = 60;

            IsMusicOn = true;
            MusicVolume = 0.8f;
            IsSFXOn = true;
            SFXVolume = 0.8f;
        }
    }

    public class SettingsArchitecture : Architecture<SettingsArchitecture>
    {
        protected override void Init() => RegisterModel(new SettingsModel());
    }
}