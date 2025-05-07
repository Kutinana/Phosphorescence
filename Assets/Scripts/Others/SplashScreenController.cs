using System.Collections;
using Kuchinashi.Utils.Progressable;
using QFramework;
using UnityEditor;
using UnityEngine;

namespace Phosphorescence.Others
{
    public class SplashScreenController : MonoSingleton<SplashScreenController>
    {
        public ProgressableGroup FadeInGroup;
        public float FadeInDuration = 0.8f;
        public float WaitDuration = 0.5f;
        public ImageColorProgressable ImageColorProgressable;
        public CanvasGroupAlphaProgressable FadeOutGroup;
        public float FadeOutDuration = 0.8f;

        public void Initialize()
        {
            FadeInGroup.Progress = 0;
            ImageColorProgressable.Progress = 0;
            FadeOutGroup.Progress = 1;
        }
    }
}