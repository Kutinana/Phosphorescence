using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Phosphorescence.Game
{   
    public abstract class Interactable : MonoBehaviour , IInteractable
    {
        public bool IsInteractable = true;
        public System.Action InteractAction { get; set; }

        public void OnInteract(IInteractor interactor)
        {
            if (!IsInteractable) throw new System.Exception("Not Interactable");

            Debug.Log("Interacted with " + interactor);
            InteractAction?.Invoke();
        }
    }
}