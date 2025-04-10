using UnityEngine;

namespace Phosphorescence.Game
{
    public interface IInteractable
    {
        public bool IsInteractable { get; }
        public void OnInteract(IInteractor interactor);
    }

    public interface IInteractor
    {
        public bool CanInteract { get; }
        public void Interact(IInteractable interactable);
    }
}