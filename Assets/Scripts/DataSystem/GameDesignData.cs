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
    }

    public partial class GameDesignData
    {
        public static bool GetTachiEData(string id, out TachiEData data)
        {
            data = null;
            return TachiEConfigs.TryGetValue(id, out data);
        }
    }
}