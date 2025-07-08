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

        protected override void OnInit()
        {
            IsFullScreen = false;
            ResolutionWidth = 1920;
            ResolutionHeight = 1080;
            FrameRate = 60;
        }
    }

    public class SettingsArchitecture : Architecture<SettingsArchitecture>
    {
        protected override void Init() => RegisterModel(new SettingsModel());
    }
}