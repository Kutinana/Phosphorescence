using System.Collections.Generic;
using Phosphorescence.Audio;
using Phosphorescence.DataSystem;
using Phosphorescence.Narration;
using QFramework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

namespace Phosphorescence.Game
{
    public class GeneratorController : Interactable, ICanPlayAndPause
    {
        public bool isActivated = true;

        public Sprite defaultSprite;
        public Sprite onHoverSprite;

        public UnityEvent onInteract;

        private SpriteRenderer spriteRenderer;
        private Animator animator;
        private Light2D[] lights;
        private AudioSource audioSource;

        public static GeneratorController Instance { get; private set; }

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            lights = GetComponentsInChildren<Light2D>();
            audioSource = GetComponent<AudioSource>();

            TypeEventSystem.Global.Register<OnStoryEventTriggerEvent>(e => {
                if (e.eventName == "diesel_runs_out")
                {
                    isActivated = false;
                    Pause();
                }
            });

            Instance = this;
        }

        private void Start()
        {
            if (GameProgressData.Instance.CurrentPlotProgress == "3.2")
            {
                isActivated = false;
                Pause();
            }

            InteractAction = new Dictionary<InputAction, System.Action> {
                { GameManager.Instance.interactAction, () => {
                    if (isActivated) return;

                    if (GameProgressData.Instance.CurrentObject == "diesel")
                    {
                        onInteract.Invoke();
                        isActivated = true;

                        Play();
                        // Audio.AudioManager.PlaySFX(GameDesignData.GetAudioData("diesel_engine_start", out var audioData) ? audioData.clip : null);

                        BackpackManager.Instance.Clear();
                    }
                    else
                    {
                        GameManager.Instance.ContinuePlot("generator_no_diesel");
                    }
                } }
            };
            HoverAction = () => {
                if (!isActivated) spriteRenderer.sprite = onHoverSprite;
            };
            UnhoverAction = () => {
                if (!isActivated) spriteRenderer.sprite = defaultSprite;
            };
        }

        public void Play()
        {
            if (isActivated)
            {
                animator.enabled = true;
                audioSource.enabled = true;
                audioSource.Play();
                foreach (var light in lights)
                {
                    light.enabled = true;
                }
            }
            else
            {
                animator.enabled = false;
                audioSource.enabled = false;
                foreach (var light in lights)
                {
                    light.enabled = false;
                }
            }
        }

        public void Pause()
        {
            if (isActivated)
            {
                animator.enabled = false;
                audioSource.enabled = false;
                foreach (var light in lights)
                {
                    light.enabled = false;
                }
            }
        }
        
    }
}