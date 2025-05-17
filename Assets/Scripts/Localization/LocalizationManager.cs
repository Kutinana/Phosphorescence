using Kuchinashi.DataSystem;
using QFramework;
using UnityEngine;

namespace Phosphorescence.Localization
{
    public enum Language
    {
        zh_CN,
        en_US,
    }
    
    
    public class LocalizationManager : Singleton<LocalizationManager>
    {
        public Language CurrentLanguage { get; private set; }

        public override void OnSingletonInit()
        {
            base.OnSingletonInit();
            CurrentLanguage = Language.zh_CN;
        }

        public string CommonStringPath => Resources.Load<TextAsset>("I18n/CommonString").text;
        
    }
}
