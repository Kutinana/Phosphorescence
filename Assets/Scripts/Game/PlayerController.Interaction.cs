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
            IInteractable interactable = null;
            var lastInteractTarget = m_CurrentInteractTarget;

            if (m_Interactables.Count == 0 || m_Interactables.All(i => !i.IsInteractable))
            {
                if (m_CurrentInteractTarget != null && m_CurrentInteractTarget.TryGetComponent<IInteractable>(out interactable))
                {
                    interactable.OnUnhover(this);
                }

                m_CurrentInteractTarget = null;
                return;
            }

            foreach (var i in m_Interactables.Where(i => i.IsInteractable))
            {
                if (m_CurrentInteractTarget == null)
                {
                    m_CurrentInteractTarget = i;
                }
                else if (Vector2.Distance(transform.position, i.transform.position) <
                         Vector2.Distance(transform.position, m_CurrentInteractTarget.transform.position))
                {
                    m_CurrentInteractTarget = i;
                }
            }

            if (m_CurrentInteractTarget != null && m_CurrentInteractTarget.IsInteractable
                && m_CurrentInteractTarget.TryGetComponent<IInteractable>(out interactable))
            {
                if (lastInteractTarget != m_CurrentInteractTarget)
                {
                    lastInteractTarget?.OnUnhover(this);
                    interactable.OnHover(this);
                }
                else
                {
                    interactable.OnHold(this);
                }
            }
        }

        public void Interact(InputAction inputAction)
        {
            if (m_CurrentInteractTarget == null) return;

            if (m_CurrentInteractTarget.IsInteractable)
            {
                m_CurrentInteractTarget.OnInteract(this, inputAction);
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

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<Interactable>(out var interactable))
            {
                m_Interactables.Add(interactable);
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<Interactable>(out var interactable))
            {
                m_Interactables.Remove(interactable);
            }
        }
    }

}