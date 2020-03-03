using UnityEngine;

namespace Somersaultbear
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyBoulderAI : EnemyBase
    {
        private Rigidbody2D rigidBody;
        [SerializeField]
        private LayerMask groundLayer = default;
        [SerializeField]
        private Vector2 startDirection = Vector2.left;

        [SerializeField]
        private float hitDetectionDistance = 1;
        [SerializeField]
        private float startSpeed = 3;

        [SerializeField]
        private bool oneDirection = false;
        private bool addedAwakeForce;

        protected override void Awake()
        {
            base.Awake();
            rigidBody = GetComponent<Rigidbody2D>();
        }

        private void Start() => Invoke(nameof(DestroyTimer), DESTROY_TIME);

        private void DestroyTimer() => Destroy(gameObject);

        private void FixedUpdate()
        {
            if (!addedAwakeForce)
            {
                // Add slight force to the object on spawn so it won't remain stationary.
                AddForce(startDirection * startSpeed);
                addedAwakeForce = true;
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
                MoveAccordingToHitDirection(hitRight, hitLeft);
            }
        }

        private void MoveAccordingToHitDirection(RaycastHit2D hitRight, RaycastHit2D hitLeft)
        {
            if (hitRight)
            {
                AddForce(Vector2.left * moveSpeed);
            }
            else if (hitLeft)
            {
                AddForce(Vector2.right * moveSpeed);
            }
        }

        private void AddForce(Vector2 force) => rigidBody.AddForce(force, ForceMode2D.Impulse);

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                PlayerCollisionEvent();
            }
        }
    }
}