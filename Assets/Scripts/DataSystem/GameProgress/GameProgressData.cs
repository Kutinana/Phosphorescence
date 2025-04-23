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

        [JsonProperty] public List<string> PlotProgress = new();
    }

    public partial class GameProgressData
    {
        public string CurrentPlotProgress => PlotProgress.LastOrDefault();
        public bool IsPlotFinished(string plotId) => PlotProgress.Contains(plotId);
        public void FinishPlot(string plotId)
        {
            PlotProgress.Add(plotId);
            Serialize();
        }
    }
}