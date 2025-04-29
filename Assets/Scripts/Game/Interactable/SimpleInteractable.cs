using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Phosphorescence.Game
{   
    public class SimpleInteractable : Interactable
    {
        public UnityEvent OnInteractEvent;
        public UnityEvent OnHoverEnterEvent;
        public UnityEvent OnHoverExitEvent;

        private void Start()
        {
            InteractAction = () =>
            {
                OnInteractEvent?.Invoke();
                Debug.Log("Interacted with " + this);
            };

            HoverAction = () =>
            {
                OnHoverEnterEvent?.Invoke();
                Debug.Log("Hovered with " + this);
            };

            UnhoverAction = () =>
            {
                OnHoverExitEvent?.Invoke();
                Debug.Log("Unhovered with " + this);
            };
        }
    }
}