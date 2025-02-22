using UnityEngine;
using UnityEngine.InputSystem;

namespace Phosphorescence.Game
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody2D rb;
        private Collider2D col;
        private Animator animator;

        InputAction moveAction;
        InputAction interactAction;

        [Header("Parameters")]
        public float speed = 5f;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            col = GetComponent<Collider2D>();
            animator = GetComponent<Animator>();
        }

        void Start()
        {
            moveAction = InputSystem.actions.FindAction("Move");
            interactAction = InputSystem.actions.FindAction("Interact");
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void FixedUpdate()
        {
            Move();
        }

        void Move()
        {
            var direction = moveAction.ReadValue<Vector2>().x;
            rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocityY);

            if (direction != 0)
            {
                transform.localScale = new Vector3(direction, 1, 1);
                animator.SetBool("isMoving", true);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
        }
    }

}