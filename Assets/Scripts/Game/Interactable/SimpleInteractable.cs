using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Phosphorescence.Game
{   
    public class SimpleInteractable : Interactable
    {
        public UnityEvent OnInteractEvent;

        private void Start()
        {
            InteractAction = () =>
            {
                OnInteractEvent?.Invoke();
                Debug.Log("Interacted with " + this);
            };
        }
    }
}