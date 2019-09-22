using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private float verticalSpeed = 4f;

    [SerializeField]
    private float hitDetectionDistance = 1f;

    // If oneDirection is true, when the enemy spawns it will just charge in one direction.
    [SerializeField]
    private bool oneDirection = false;

    // Starting speed for OnEnable or On Start.
    [SerializeField]
    private float startSpeed = 3f;

    // Starting direction, X 1/-1 or left/right.
    [SerializeField]
    private Vector2 startDirection = Vector2.left;

    [SerializeField]
    private LayerMask groundLayer = default;

    private UILogic uiLogic = default;

    private Rigidbody2D rigidBody = default;

    private void Start()
    {
        uiLogic = GameObject.Find("PRE_UILogic").GetComponent<UILogic>();

        rigidBody = GetComponent<Rigidbody2D>();

        // Add slight force to the object on spawn.
        rigidBody.AddForce(startDirection * startSpeed, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        // If oneDirection is false, cast 2 rays on left and right and move to the left and right according to collisions.
        if (!oneDirection)
        {
            // Draw rays left and right and store their collision info in in two variables.
            RaycastHit2D hitXPositive = Physics2D.Raycast(transform.position, Vector2.right, hitDetectionDistance, groundLayer);

            #if UNITY_EDITOR
            Debug.DrawRay(transform.position, Vector2.right * hitDetectionDistance, Color.white);
            #endif

            RaycastHit2D hitXNegative = Physics2D.Raycast(transform.position, Vector2.left, hitDetectionDistance, groundLayer);

            #if UNITY_EDITOR
            Debug.DrawRay(transform.position, Vector2.left * hitDetectionDistance, Color.white);
            #endif

            // Valuate X+/right and X-/left and apply force to the player rigidbody according to them.
            if (hitXPositive && hitXPositive.collider)
            {
                rigidBody.AddForce(Vector2.left * verticalSpeed, ForceMode2D.Impulse);
            }
            else if (hitXNegative && hitXNegative.collider)
            {
                rigidBody.AddForce(Vector2.right * verticalSpeed, ForceMode2D.Impulse);
            }
        }
    }

    // Reset the game when collided with the player.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            uiLogic.ChangeScene("SCE_GameLoop");
        }
    }
}
