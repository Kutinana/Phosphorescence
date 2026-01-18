using System.Collections;
using Kuchinashi.Utils.Progressable;
using QFramework;

namespace Phosphorescence.Others
{
    public class SplashScreenController : MonoSingleton<SplashScreenController>
    {
        public ProgressableGroup FadeInGroup;
        public float FadeInDuration = 0.8f;
        public float WaitDuration = 0.5f;
        public CanvasGroupAlphaProgressable FadeOutGroup;
        public float FadeOutDuration = 0.8f;

        public ImageColorProgressable DisclaimerProgressable;
        public ImageColorProgressable PrefaceAProgressable;
        public ImageColorProgressable PrefaceBProgressable;
        public ImageColorProgressable PrefaceCProgressable;
        public ImageColorProgressable MaskProgressable;

        public ImageColorProgressable ThanksProgressable;

        public void Initialize()
        {
            FadeInGroup.Progress = 0;
            FadeOutGroup.Progress = 1;

            DisclaimerProgressable.Progress = 0;
            PrefaceAProgressable.Progress = 0;
            PrefaceBProgressable.Progress = 0;
            PrefaceCProgressable.Progress = 0;
            MaskProgressable.Progress = 0;
        }
    }
}