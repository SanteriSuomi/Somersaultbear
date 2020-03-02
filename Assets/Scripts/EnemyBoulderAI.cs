using UnityEngine;

namespace Somersaultbear
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyBoulderAI : MonoBehaviour
    {
        private UIManager uiManager = default;
        private Rigidbody2D rigidBody = default;
        [SerializeField]
        private LayerMask groundLayer = default;
        [SerializeField]
        private Vector2 startDirection = Vector2.left;

        [SerializeField]
        private float verticalSpeed = 5f;
        [SerializeField]
        private float hitDetectionDistance = 1f;
        [SerializeField]
        private float startSpeed = 3f;

        [SerializeField]
        private bool oneDirection = false;
        private bool addedForce = false;

        private const float DESTROY_TIME = 15f;

        private void Awake()
        {
            uiManager = FindObjectOfType<UIManager>();
            rigidBody = GetComponent<Rigidbody2D>();
        }

        private void Start() => Invoke(nameof(DestroyTimer), DESTROY_TIME);

        private void DestroyTimer() => Destroy(gameObject);

        private void FixedUpdate()
        {
            if (!addedForce)
            {
                AddAwakeForce();
            }
            else if (!oneDirection)
            {
                // Draw a raycast for both right and left directions.
                RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, hitDetectionDistance, groundLayer);
                RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, hitDetectionDistance, groundLayer);

                #if UNITY_EDITOR
                Debug.DrawRay(transform.position, Vector2.right * hitDetectionDistance, Color.white);
                Debug.DrawRay(transform.position, Vector2.left * hitDetectionDistance, Color.white);
                #endif

                // Detect if the gameObject is hitting left or right using raycasts and move to the opposite direction.
                MoveCharacterAccordingToHitDirection(hitRight, hitLeft);
            }
        }

        private void AddAwakeForce()
        {
            // Add slight force to the object on spawn so it won't remain stationary.
            rigidBody.AddForce(startDirection * startSpeed, ForceMode2D.Impulse);
            addedForce = true;
        }

        private void MoveCharacterAccordingToHitDirection(RaycastHit2D hitRight, RaycastHit2D hitLeft)
        {
            if (hitRight)
            {
                rigidBody.AddForce(Vector2.left * verticalSpeed, ForceMode2D.Impulse);
            }
            else if (hitLeft)
            {
                rigidBody.AddForce(Vector2.right * verticalSpeed, ForceMode2D.Impulse);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                uiManager.ShowMenuItemsDeath();
            }
        }
    }
}