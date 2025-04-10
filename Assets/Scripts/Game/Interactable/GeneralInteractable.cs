using UnityEngine;
using UnityEngine.Events;

namespace Phosphorescence.Game
{   
    public class GeneralInteractable : MonoBehaviour , IInteractable
    {
        public bool IsInteractable => _interactable;
        public bool _interactable = true;

        public UnityEvent onInteract;
        public void OnInteract(IInteractor interactor)
        {
            if (!IsInteractable) throw new System.Exception("Not Interactable");

            Debug.Log("Interacted with " + interactor);
            onInteract?.Invoke();
        }
    }
}