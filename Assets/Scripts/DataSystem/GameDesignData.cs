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
        
        protected static Dictionary<string, TachiEData> TachiEConfigs => _tachiEConfigs ??= GenerateConfig<TachiEData>("TachiEData");
        private static Dictionary<string, TachiEData> _tachiEConfigs;

        protected static Dictionary<string, BackgroundPicData> BackgroundPicConfigs => _backgroundPicConfigs ??= GenerateConfig<BackgroundPicData>("BackgroundPicData");
        private static Dictionary<string, BackgroundPicData> _backgroundPicConfigs;

        protected static Dictionary<string, AudioData> AudioConfigs => _audioConfigs ??= GenerateConfig<AudioData>("AudioData");
        private static Dictionary<string, AudioData> _audioConfigs;

        private static Dictionary<string, T> GenerateConfig<T>(string pathName) where T : ScriptableObject, IHaveId
        {
            var configs = new Dictionary<string, T>();
            var data = Resources.LoadAll<T>($"ScriptableObjects/{pathName}");
            foreach (var item in data)
            {
                configs.Add(item.Id, item);
            }

            return configs;
        }
    }

    public partial class GameDesignData
    {
        public static bool GetData<T>(string id, out T data) where T : ScriptableObject , IHaveId
        {
            data = Resources.Load<T>($"ScriptableObjects/{typeof(T).Name}/{id}");
            return data != null;
        }

        public static bool GetTachiEData(string id, out TachiEData data) => TachiEConfigs.TryGetValue(id, out data);
        public static bool GetBackgroundPicData(string id, out BackgroundPicData data) => BackgroundPicConfigs.TryGetValue(id, out data);
        public static bool GetAudioData(string id, out AudioData data) => AudioConfigs.TryGetValue(id, out data);
    }
}