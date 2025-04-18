using QFramework;
using UnityEngine;

namespace Phosphorescence.Game
{
    public class FloorManager : MonoSingleton<FloorManager>
    {
        public FloorStateController[] Floors;

        public void Start()
        {
            SwitchTo(PlayerController.Instance.ImAtFloor);
        }

        public void SwitchTo(int floorIndex)
        {
            if (floorIndex < 0 || floorIndex >= Floors.Length)
            {
                Debug.LogError($"Invalid floor index: {floorIndex}");
                return;
            }

            Floors[PlayerController.Instance.ImAtFloor].StateMachine.ChangeState(FloorStateController.FloorState.Paused);
            Floors[floorIndex].StateMachine.ChangeState(FloorStateController.FloorState.Active);

            PlayerController.Instance.ImAtFloor = floorIndex;
        }
    }
}
