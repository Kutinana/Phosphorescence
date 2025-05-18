using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Phosphorescence.Game
{
    public class StairInteractionController : Interactable
    {
        public bool CanUpstair = true;
        public bool CanDownstair = true;
        public Transform UpstairFinishPoint;
        public Transform DownstairFinishPoint;

        void Start()
        {
            // InteractAction = new Dictionary<InputAction, System.Action> {
            //     { GameManager.Instance.upStairAction, () => {
            //         PlayerController.Instance.Upstair();
            //     } },
            //     { GameManager.Instance.downStairAction, () => {
            //         PlayerController.Instance.Downstair();
            //     } }
            // };
        }

        public override void OnInteract(IInteractor interactor, InputAction inputAction)
        {
            if (interactor is not PlayerController playerController) return;

            if (inputAction == GameManager.Instance.upStairAction && CanUpstair)
            {
                playerController.Upstair();
            }
            else if (inputAction == GameManager.Instance.downStairAction && CanDownstair)
            {
                playerController.Downstair();
            }
        }
    }
}
