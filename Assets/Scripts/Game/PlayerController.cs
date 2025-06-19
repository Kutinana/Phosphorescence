using Kuchinashi;
using Kuchinashi.Utils;
using Kuchinashi.Utils.Progressable;
using Phosphorescence.DataSystem;
using Phosphorescence.Narration;
using Phosphorescence.Narration.Common;
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
        public bool IsOnStair = false;
        public bool IsOutside
        {
            get => _isOutside;
            set
            {
                _isOutside = value;
                if (value)
                {
                    Audio.AudioManager.SetAmbientVolume("sea", 1f, 2f);
                }
                else
                {
                    Audio.AudioManager.SetAmbientVolume("sea", 0.2f, 2f);
                }
            }
        }
        private bool _isOutside = false;

        [Header("Raise Up Related References")]
        public Animator RaiseUpAnimator;
        public SpriteRenderer RaiseUpSpriteRenderer;
        public Progressable RaiseUpProgressable;

        [Header("Audio")]
        public float FootstepInterval = 0.5f;
        public SerializableDictionary<string, AudioClipRandomPicker> AudioClipRandomPickers;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            col = GetComponent<Collider2D>();
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();

            if (!GameProgressData.Instance.IsPlotFinished("0.5") || GameProgressData.Instance.CurrentPlotProgress == "4.9") {
                NormalSpriteProgressable.Progress = 0f;
            }

            TypeEventSystem.Global.Register<OnStoryEndEvent>(e => {
                if (e.plot.Id == "0.0") {
                    NormalSpriteProgressable.LinearTransition(1f);
                }
            });

            TypeEventSystem.Global.Register<OnStoryEventTriggerEvent>(e => {
                if (e.eventName == "grab_the_projector") {
                    NormalSpriteProgressable.InverseLinearTransition(0.2f);
                    RaiseUpSpriteRenderer.enabled = true;
                    RaiseUpProgressable.LinearTransition(0.2f);
                    RaiseUpAnimator.enabled = true;
                }
                else if (e.eventName == "grab_the_projector_finished") {
                    RaiseUpProgressable.InverseLinearTransition(0.2f);
                    RaiseUpAnimator.enabled = false;
                    NormalSpriteProgressable.LinearTransition(0.2f);
                }
            });
        }

        // Update is called once per frame
        void Update()
        {
            if (CanInteract)
            {
                SelectInteractable();

                if (GameManager.Instance.interactAction.WasPressedThisFrame()) Interact(GameManager.Instance.interactAction);

                if (GameManager.Instance.upStairAction.WasPressedThisFrame()) Interact(GameManager.Instance.upStairAction);
                if (GameManager.Instance.downStairAction.WasPressedThisFrame()) Interact(GameManager.Instance.downStairAction);
            }
        }

        private void FixedUpdate()
        {
            Move();
        }

        void Move()
        {
            if (GameManager.Instance.moveAction == null || GameManager.Instance.moveAction.enabled == false) return;
            if (IsOnStair) return;

            var direction = Mathf.RoundToInt(GameManager.Instance.moveAction.ReadValue<Vector2>().x);
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

        private void Move(float direction)
        {
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

        public void StopMoving(int direction = 1)
        {
            rb.linearVelocity = Vector2.zero;
            transform.localScale = new Vector3(direction, 1, 1);
            spriteRenderer.material.SetFloat("_FlipGreen", direction);
            animator.SetBool("isMoving", false);
        }

        public void TransportTo(Transform target)
        {
            transform.position = target.position;
        }

        public void TransportTo(Vector3 target)
        {
            transform.position = target;
        }

        public void PlayFootstep()
        {
            Audio.AudioManager.PlaySFX(AudioClipRandomPickers["footstep"].Pick(), volume: 0.5f);
        }

        public void PlayFootstepOnStair()
        {
            Audio.AudioManager.PlaySFX(AudioClipRandomPickers["onstair"].Pick(), volume: 0.4f);
        }
    }

}