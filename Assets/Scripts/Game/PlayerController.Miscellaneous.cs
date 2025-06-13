using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Phosphorescence.Game
{
    public partial class PlayerController
    {
        public void OnOpeningSwitchToAnimation()
        {
            TransportTo(new Vector3(-2.7f, 8.44f, 0));
        }

        public void OnAfterOpeningStart()
        {
            GameManager.Instance.moveAction.Disable();
        }

        public void OnAfterOpeningEnd()
        {
            GameManager.Instance.moveAction.Enable();
        }
    }

}