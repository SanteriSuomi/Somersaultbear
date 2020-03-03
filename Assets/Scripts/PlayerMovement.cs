﻿using UnityEngine;

namespace Somersaultbear
{
    [RequireComponent(typeof(Rigidbody2D), typeof(AudioSource), typeof(Animator))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private LayerMask groundLayer = default;
        private Rigidbody2D rigidBody = default;
        private AudioSource audioSource = default;
        private Animator animator = default;

        [SerializeField]
        private float verticalSpeed = 3f;
        [SerializeField]
        private float maxVerticalSpeed = 6.5f;
        [SerializeField]
        private float jumpModifier = 8f;
        [SerializeField]
        private float jumpDetectionHeight = 0.715f;

        // Prevent double jumping with small velocity check.
        private const float RB_Y_VELOCITY_MAX_JUMP = 0.5f;
        private const float RB_Y_VELOCITY_MAX_ANIMS = 1f;
        private const float JUMP_DRAW_REDUCTION = 1.5f;

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            audioSource = GetComponent<AudioSource>();
            animator = GetComponent<Animator>();
        }

        private void Start() => InputManager.Instance.InputScheme.JumpEvent += OnJump;

        public void OnJump() // Public because also activated from mobile jump button
        {
            // Cast a raycast downwards for ground detection.
            RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector2.down, jumpDetectionHeight, groundLayer);
            Jump(rayHit);
            PlayAnimations(rayHit);
        }

        private void FixedUpdate()
        {
            #if UNITY_EDITOR
            Debug.DrawRay(transform.position, Vector2.down * jumpDetectionHeight, Color.green);
            #endif

            MoveForward();
        }

        private void MoveForward()
        {
            // Continuously move player to the right.
            if (rigidBody.velocity.x < maxVerticalSpeed)
            {
                rigidBody.AddForce(Vector2.right * verticalSpeed, ForceMode2D.Force);
            }
        }

        private void Jump(RaycastHit2D rayHit)
        {
            if (rayHit && rigidBody.velocity.y < RB_Y_VELOCITY_MAX_JUMP)
            {
                rigidBody.AddForce(Vector2.up * jumpModifier, ForceMode2D.Impulse);
                // Add force backwards to reduce the drag on air.
                rigidBody.AddForce(new Vector3(-JUMP_DRAW_REDUCTION, 0, 0), ForceMode2D.Impulse);
                audioSource.Play();
            }
        }

        private void PlayAnimations(RaycastHit2D rayHit)
        {
            JumpAnim();
            FallAnim(rayHit);
        }

        private void JumpAnim()
        {
            if (rigidBody.velocity.y > RB_Y_VELOCITY_MAX_ANIMS)
            {
                animator.SetTrigger("Jump");
                FreezeAndResetRotation();
            }
            else
            {
                // Unfreeze after.
                rigidBody.freezeRotation = false;
            }
        }

        private void FallAnim(RaycastHit2D rayHit)
        {
            if (rigidBody.velocity.y < -RB_Y_VELOCITY_MAX_ANIMS)
            {
                animator.SetBool("Fall", true);
                FreezeAndResetRotation();
            }
            else if (rayHit)
            {
                animator.SetBool("Fall", false);
                rigidBody.freezeRotation = false;
            }
        }

        private void FreezeAndResetRotation()
        {
            // Freeze the rotation for the jump animation.
            rigidBody.freezeRotation = true;
            // Reset rotation to default for the jump animation.
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
}