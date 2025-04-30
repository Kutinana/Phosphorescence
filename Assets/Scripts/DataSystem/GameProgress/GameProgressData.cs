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
        [JsonIgnore] private int _lastFloorIndex;

        [JsonProperty] public string CurrentObject = "";
        [JsonProperty] public List<string> PlotProgress = new();
        [JsonProperty] public Dictionary<string, int> ObtainedObjects = new();
    }

    public partial class GameProgressData
    {
        [JsonIgnore] public string CurrentPlotProgress => PlotProgress.LastOrDefault() ?? "";
        public bool IsPlotFinished(string plotId) => PlotProgress.Contains(plotId);
        public void FinishPlot(string plotId)
        {
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
    }
}