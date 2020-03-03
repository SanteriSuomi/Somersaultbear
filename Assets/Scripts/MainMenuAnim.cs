using UnityEngine;
using UnityEngine.EventSystems;

namespace Somersaultbear
{
    [RequireComponent(typeof(Rigidbody2D), typeof(AudioSource), typeof(SpriteRenderer))]
    public class MainMenuAnim : MonoBehaviour, IPointerDownHandler, IPointerClickHandler, IPointerUpHandler
    {
        // True == right, false == left.
        public bool Direction { get; set; } = false;

        [SerializeField]
        private LayerMask groundLayer = default;
        private Rigidbody2D rigidBody;
        private AudioSource audioSource;
        private SpriteRenderer spriteRenderer;

        [SerializeField]
        private float jumpDetectionHeight = 0.715f;
        [SerializeField]
        private float verticalSpeed = 2.5f;
        [SerializeField]
        private float jumpModifier = 5f;

        private const int RANDOM_MOUSECLICK_FORCE = 20;

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            audioSource = GetComponent<AudioSource>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Add a random force to the character when clicked on.
        public void OnPointerClick(PointerEventData eventData)
        {
            int random = Random.Range(-RANDOM_MOUSECLICK_FORCE, RANDOM_MOUSECLICK_FORCE);
            AddForce(new Vector2(random, random));
        }

        private void FixedUpdate()
        {
            // Raycast for the jump
            RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector2.down, jumpDetectionHeight, groundLayer);

            #if UNITY_EDITOR
            Debug.DrawRay(transform.position, Vector2.down * jumpDetectionHeight, Color.green);
            #endif

            MoveCharacterAccordingToDirection();

            // Jump every time the raycast hits the ground layer.
            if (rayHit)
            {
                Jump();
            }

            FlipSprite();
        }

        private void MoveCharacterAccordingToDirection()
        {
            if (!Direction)
            {
                AddForce(Vector2.left * verticalSpeed);
            }
            else
            {
                AddForce(Vector2.right * verticalSpeed);
            }
        }

        private void Jump()
        {
            AddForce(Vector2.up * jumpModifier);
            audioSource.Play();
        }

        private void FlipSprite()
        {
            // Switch the sprite's direction according to the direction it's going.
            if (rigidBody.velocity.x > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (rigidBody.velocity.x < 0)
            {
                spriteRenderer.flipX = true;
            }
        }

        private void AddForce(Vector2 force) => rigidBody.AddForce(force, ForceMode2D.Impulse);

        #region Mandatory but not used
        public void OnPointerUp(PointerEventData eventData)
        {
            // Empty because IPointerClickHandler requires this to function.
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            // Empty because IPointerClickHandler requires this to function.
        }
        #endregion
    }
}