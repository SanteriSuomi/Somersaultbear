using UnityEngine;

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
        private RaycastHit2D rayHit;

        [SerializeField]
        private float verticalSpeed = 3f;
        [SerializeField]
        private float maxVerticalSpeed = 6.5f;
        [SerializeField]
        private float jumpModifier = 8f;
        [SerializeField]
        private float jumpDetectionHeight = 0.715f;
        [SerializeField]
        private float maxJumpVelocity = 0.5f;
        [SerializeField]
        private float animationMaxVelocity = 1.5f;
        [SerializeField]
        private float jumpAirDragReduction = 1.5f;

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            audioSource = GetComponent<AudioSource>();
            animator = GetComponent<Animator>();
        }

        private void Start() => InputManager.Instance.InputScheme.JumpEvent += OnJump;

        public void OnJump() => Jump(); // Public because also activated from mobile jump button

        private void Jump()
        {
            if (rayHit && rigidBody.velocity.y < maxJumpVelocity)
            {
                AddForce(Vector2.up * jumpModifier, ForceMode2D.Impulse);
                AddForce(Vector3.right * -jumpAirDragReduction, ForceMode2D.Impulse);
                PlayJumpSound();
            }
        }

        private void PlayJumpSound() => audioSource.Play();

        private void Update() 
            => rayHit = Physics2D.Raycast(transform.position, Vector2.down, jumpDetectionHeight, groundLayer);

        private void FixedUpdate()
        {
            #if UNITY_EDITOR
            Debug.DrawRay(transform.position, Vector2.down * jumpDetectionHeight, Color.green);
            #endif

            MoveForward();
            JumpAnimation();
            FallAnimation();
        }

        private void MoveForward()
        {
            if (rigidBody.velocity.x < maxVerticalSpeed)
            {
                rigidBody.AddForce(Vector2.right * verticalSpeed, ForceMode2D.Force);
            }
        }

        private void JumpAnimation()
        {
            if (rigidBody.velocity.y > animationMaxVelocity)
            {
                animator.SetTrigger("Jump");
                FreezeAndResetRotation();
            }
            else
            {
                rigidBody.freezeRotation = false;
            }
        }

        private void FallAnimation()
        {
            if (rigidBody.velocity.y < -animationMaxVelocity)
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
            rigidBody.freezeRotation = true;
            transform.eulerAngles = Vector3.zero;
        }

        private void AddForce(Vector2 force, ForceMode2D mode) => rigidBody.AddForce(force, mode);
    }
}