using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBoulderAI : MonoBehaviour
{
    [SerializeField]
    private LayerMask groundLayer = default;

    private UIManager uiManager = default;
    private Rigidbody2D rigidBody = default;

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
        uiManager = GameObject.Find("PRE_UIManager").GetComponent<UIManager>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // Run a timer for self-destroy.
        Destroy(gameObject, DESTROY_TIME);
    }

    private void FixedUpdate()
    {
        if (!addedForce)
        {
            // Add slight force to the object on spawn so it won't remain stationary.
            rigidBody.AddForce(startDirection * startSpeed, ForceMode2D.Impulse);
            addedForce = true;
        }

        if (!oneDirection)
        {
            // Draw a raycast for both right and left directions.
            RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, hitDetectionDistance, groundLayer);
            RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, hitDetectionDistance, groundLayer);

            #if UNITY_EDITOR
            Debug.DrawRay(transform.position, Vector2.right * hitDetectionDistance, Color.white);
            Debug.DrawRay(transform.position, Vector2.left * hitDetectionDistance, Color.white);
            #endif

            // Detect if the gameObject is hitting left or right using raycasts and move to the opposite direction.
            if (hitRight)
            {
                rigidBody.AddForce(Vector2.left * verticalSpeed, ForceMode2D.Impulse);
            }
            else if (hitLeft)
            {
                rigidBody.AddForce(Vector2.right * verticalSpeed, ForceMode2D.Impulse);
            }
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