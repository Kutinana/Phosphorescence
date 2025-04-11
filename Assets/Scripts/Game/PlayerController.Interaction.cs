using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Phosphorescence.Game
{
    public partial class PlayerController
    {
        private List<Interactable> m_Interactables = new List<Interactable>();
        private Interactable m_CurrentInteractTarget;

        private void SelectInteractable()
        {
            if (m_Interactables.Count == 0 || m_Interactables.All(i => !i.IsInteractable))
            {
                m_CurrentInteractTarget = null;
                return;
            }

            foreach (var interactable in m_Interactables.Where(i => i.IsInteractable))
            {
                if (m_CurrentInteractTarget == null)
                {
                    m_CurrentInteractTarget = interactable;
                }
                else if (Vector2.Distance(transform.position, interactable.transform.position) <
                         Vector2.Distance(transform.position, m_CurrentInteractTarget.transform.position))
                {
                    m_CurrentInteractTarget = interactable;
                }
            }
        }

        public void Interact()
        {
            if (m_CurrentInteractTarget == null) return;

            if (m_CurrentInteractTarget.IsInteractable)
            {
                m_CurrentInteractTarget.OnInteract(this);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Interactable>(out var interactable))
            {
                m_Interactables.Add(interactable);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent<Interactable>(out var interactable))
            {
                m_Interactables.Remove(interactable);
            }
        }
    }

}