using Phosphorescence.DataSystem;
using QFramework;
using UnityEngine;

namespace Phosphorescence.Game
{
    public class FloorManager : MonoSingleton<FloorManager>
    {
        public FloorStateController[] Floors;
        public Transform[] FloorPivots;

        public void Start()
        {
            
        }

        public FloorStateController GetCurrentFloor()
        {
            return Floors[PlayerController.Instance.ImAtFloor];
        }

        public FloorStateController GetFloor(int floorIndex)
        {
            if (floorIndex < 0 || floorIndex >= Floors.Length)
            {
                Debug.LogError($"Invalid floor index: {floorIndex}");
                return null;
            }

            return Floors[floorIndex];
        }

        public FloorStateController[] GetAllFloors() => Floors;

        public void SwitchTo(int floorIndex)
        {
            if (floorIndex < 0 || floorIndex >= Floors.Length)
            {
                Debug.LogError($"Invalid floor index: {floorIndex}");
                return;
            }

            // Floors[PlayerController.Instance.ImAtFloor].StateMachine.ChangeState(FloorStateController.FloorState.Paused);
            // Floors[floorIndex].StateMachine.ChangeState(FloorStateController.FloorState.Active);

            PlayerController.Instance.ImAtFloor = floorIndex;
            GameProgressData.Instance.LastFloorIndex = floorIndex;
        }
    }
}
