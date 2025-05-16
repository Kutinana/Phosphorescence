using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Phosphorescence.Game
{
    public class StairInteractionController : Interactable
    {
        void Start()
        {
            InteractAction = new Dictionary<InputAction, System.Action> {
                { GameManager.Instance.upStairAction, () => {
                    PlayerController.Instance.Upstair();
                } },
                { GameManager.Instance.downStairAction, () => {
                    PlayerController.Instance.Downstair();
                } }
            };
        }
    }
}
