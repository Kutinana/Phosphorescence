using UnityEngine;

namespace Phosphorescence.Game
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody2D rb;
        private Collider2D col;
        private Animator animator;

        [Header("Parameters")]
        public float speed = 5f;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            col = GetComponent<Collider2D>();
            animator = GetComponent<Animator>();
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
            var direction = Input.GetAxisRaw("Horizontal");
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