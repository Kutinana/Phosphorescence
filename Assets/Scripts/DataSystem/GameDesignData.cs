using System.Collections;
using System.Collections.Generic;
using Kuchinashi.DataSystem;
using Phosphorescence.Narration.Common;
using QFramework;
using UnityEngine;

namespace Phosphorescence.DataSystem
{
    public partial class GameDesignData : ISingleton
    {
        public void OnSingletonInit() {}
        
        protected static Dictionary<string, TachiEData> TachiEConfigs
        {
            get
            {
                if (_tachiEConfigs == null)
                {
                    _tachiEConfigs = new Dictionary<string, TachiEData>();
                    var tachiEData = Resources.LoadAll<TachiEData>("ScriptableObjects/TachiEData");
                    foreach (var tachiE in tachiEData)
                    {
                        _tachiEConfigs.Add(tachiE.Id, tachiE);
                    }
                }

                return _tachiEConfigs;
            }
        }
        private static Dictionary<string, TachiEData> _tachiEConfigs;

        protected static Dictionary<string, BackgroundPicData> BackgroundPicConfigs
        {
            get
            {
                if (_backgroundPicConfigs == null)
                {
                    _backgroundPicConfigs = new Dictionary<string, BackgroundPicData>();
                    var narrationData = Resources.LoadAll<BackgroundPicData>("ScriptableObjects/BackgroundPicData");
                    foreach (var narration in narrationData)
                    {
                        _backgroundPicConfigs.Add(narration.Id, narration);
                    }
                }

                return _backgroundPicConfigs;
            }
        }
        private static Dictionary<string, BackgroundPicData> _backgroundPicConfigs;
    }

    public partial class GameDesignData
    {
        public static bool GetTachiEData(string id, out TachiEData data) => TachiEConfigs.TryGetValue(id, out data);
        public static bool GetBackgroundPicData(string id, out BackgroundPicData data) => BackgroundPicConfigs.TryGetValue(id, out data);
    }
}