using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Phosphorescence.Game
{
    public interface IInteractable
    {
        public virtual void OnHover(IInteractor interactor) {}
        public virtual void OnInteract(IInteractor interactor) {}
        public virtual void OnUnhover(IInteractor interactor) {}
    }

    public interface IInteractor
    {
        public bool CanInteract { get; }
        // public void Interact(IInteractable interactable);
        // public virtual void Interact(IInteractable interactable, InputAction.CallbackContext context) {}
    }
}