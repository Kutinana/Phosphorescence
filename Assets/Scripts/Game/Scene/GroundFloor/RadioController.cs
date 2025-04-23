using Phosphorescence.DataSystem;
using Phosphorescence.Narration;
using UnityEngine;

namespace Phosphorescence.Game
{
    public class RadioController : Interactable
    {
        public override System.Action InteractAction { get; set; }

        private void Start()
        {
            InteractAction = () => {
                GameManager.Instance.ContinuePlot();
            };
        }
    }
}