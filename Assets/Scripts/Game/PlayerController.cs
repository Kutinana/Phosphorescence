using QFramework;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Phosphorescence.Game
{
    public partial class PlayerController : MonoSingleton<PlayerController> , IInteractor
    {
        private Rigidbody2D rb;
        private Collider2D col;
        private Animator animator;
        private SpriteRenderer spriteRenderer;

        public bool CanInteract => true;

        [Header("Parameters")]
        public float speed = 5f;
        public int ImAtFloor = -1;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            col = GetComponent<Collider2D>();
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            if (CanInteract)
            {
                SelectInteractable();

                if (GameManager.Instance.interactAction.WasPressedThisFrame()) Interact();
            }
        }

        private void FixedUpdate()
        {
            Move();
        }

        void Move()
        {
            if (GameManager.Instance.moveAction == null) return;

            var direction = GameManager.Instance.moveAction.ReadValue<Vector2>().x;
            rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocityY);

            if (direction != 0)
            {
                transform.localScale = new Vector3(direction, 1, 1);
                spriteRenderer.material.SetFloat("_FlipGreen", direction);

                animator.SetBool("isMoving", true);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
        }

        public void TransportTo(Transform target)
        {
            transform.position = target.position;
        }

        public void TransportTo(Vector3 target)
        {
            transform.position = target;
        }
    }

}