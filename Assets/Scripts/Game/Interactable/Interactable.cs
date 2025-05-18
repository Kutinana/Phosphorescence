using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Phosphorescence.Game
{   
    public abstract class Interactable : MonoBehaviour , IInteractable
    {
        public bool IsInteractable { get => _isInteractable; set => _isInteractable = value; }
        [SerializeField] private bool _isInteractable = true;

        public virtual Dictionary<InputAction, System.Action> InteractAction { get; set; }
        public virtual System.Action HoverAction { get; set; }
        public virtual System.Action UnhoverAction { get; set; }

        public virtual void OnInteract(IInteractor interactor, InputAction inputAction)
        {
            if (!IsInteractable) throw new System.Exception("Not Interactable");

            Debug.Log("Interacted with " + interactor);
            if (InteractAction.TryGetValue(inputAction, out var action))
            {
                action?.Invoke();
            }
        }

        public virtual void OnHover(IInteractor interactor)
        {
            if (!IsInteractable) throw new System.Exception("Not Interactable");

            Debug.Log("Hovered with " + interactor);
            HoverAction?.Invoke();
        }

        public virtual void OnUnhover(IInteractor interactor)
        {
            Debug.Log("Unhovered with " + interactor);
            UnhoverAction?.Invoke();
        }
    }
}