using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using Newtonsoft.Json;
using Kuchinashi.DataSystem;
using System.Linq;

namespace Phosphorescence.DataSystem
{
    public partial class GameProgressData : ReadableAndWriteableData, ISingleton
    {
        [JsonIgnore] public override string Path { get => System.IO.Path.Combine(Application.persistentDataPath, "save"); }
        public static GameProgressData Instance
        {
            get => _instance ??= new GameProgressData().DeSerialize<GameProgressData>();
            private set => _instance = value;
        }
        private static GameProgressData _instance;
        public void OnSingletonInit() { }

        [JsonProperty] public int LastFloorIndex
        {
            get => _lastFloorIndex;
            set
            {
                _lastFloorIndex = value;
                Serialize();
            }
        }
        [JsonIgnore] private int _lastFloorIndex = 1;

        [JsonProperty] public string CurrentObject = "";
        [JsonProperty] public List<string> PlotProgress = new();
        [JsonProperty] public Dictionary<string, int> ObtainedObjects = new();

        [JsonProperty] public Dictionary<string, string> GameInfo = new();
    }

    public partial class GameProgressData
    {
        [JsonIgnore] public bool IsNewGame => !System.IO.File.Exists(Path);
        [JsonIgnore] public string CurrentPlotProgress => PlotProgress.LastOrDefault() ?? "";
        public bool IsPlotFinished(string plotId) => PlotProgress.Contains(plotId);
        public void FinishPlot(string plotId)
        {
            if (IsPlotFinished(plotId)) return;
            PlotProgress.Add(plotId);
            Serialize();
        }
        public void ResetPlotProgress()
        {
            PlotProgress.Clear();
            Serialize();
        }

        public void ObtainObject(string objectId, int amount = 1)
        {
            if (GameDesignData.GetData(objectId, out ObtainableObject obtainableObject))
            {
                ObtainedObjects[objectId] = obtainableObject.MaxSize > ObtainedObjects.GetValueOrDefault(objectId, 0) + amount ?
                    ObtainedObjects.GetValueOrDefault(objectId, 0) + amount : obtainableObject.MaxSize;
                
                Serialize();
            }
        }

        public string GetInfo(string key)
        {
            return GameInfo.TryGetValue(key, out var value) ? value : "";
        }

        public void SetInfo(string key, bool value)
        {
            GameInfo[key] = value ? "true" : "false";
            Serialize();
        }

        public void SetInfo(string key, string value)
        {
            GameInfo[key] = value;
            Serialize();
        }

        public bool HasInfo(string key) => GameInfo.ContainsKey(key);
        public bool CompareInfoWith(string key, string value = "true")
        {
            return GameInfo.TryGetValue(key, out var storedValue) && storedValue == value;
        }
    }
}