using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private float verticalSpeed = 4f;

    [SerializeField]
    private float hitDetectionDistance = 1f;

    [SerializeField]
    private bool oneDirection = false;

    [SerializeField]
    private float startSpeed = 3f;

    [SerializeField]
    private Vector2 startDirection = Vector2.left;

    [SerializeField]
    private LayerMask groundLayer = default;

    private UILogic uiLogic = default;

    private Rigidbody2D rigidBody = default;

    private void Start()
    {
        uiLogic = GameObject.FindWithTag("UILogicManager").GetComponent<UILogic>();

        Assert.IsNotNull(uiLogic);

        rigidBody = GetComponent<Rigidbody2D>();

        // Add slight force to the object on spawn so it's not stationary.
        rigidBody.AddForce(startDirection * startSpeed, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        // If the object should only go in one direction when spawning (using the Start() AddForce).
        if (!oneDirection)
        {
            RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, hitDetectionDistance, groundLayer);

            RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, hitDetectionDistance, groundLayer);

            Debug.DrawRay(transform.position, Vector2.right * hitDetectionDistance, Color.white);

            Debug.DrawRay(transform.position, Vector2.left * hitDetectionDistance, Color.white);

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
            uiLogic.ChangeScene("SCE_GameLoop");
        }
    }
}
