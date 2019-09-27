using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Rigidbody2D))]
public class MainMenuAnim : MonoBehaviour
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

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        #if UNITY_EDITOR
        Assert.IsNotNull(rigidBody);
        Assert.IsNotNull(audioSource);
        Assert.IsNotNull(spriteRenderer);
        #endif
    }

    // Add a random force to the character when clicked on.
    private void OnMouseDown()
    {
        int random = Random.Range(-RANDOM_MOUSECLICK_FORCE, RANDOM_MOUSECLICK_FORCE);
        rigidBody.AddForce(new Vector2(random, random), ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector2.down, jumpDetectionHeight, groundLayer);

        #if UNITY_EDITOR
        Debug.DrawRay(transform.position, Vector2.down * jumpDetectionHeight, Color.green);
        #endif

        if (!Direction)
        {
            rigidBody.AddForce(Vector2.left * verticalSpeed, ForceMode2D.Force);
        }
        else
        {
            rigidBody.AddForce(Vector2.right * verticalSpeed, ForceMode2D.Force);
        }
        // Jump every time the raycast hits the ground layer.
        if (rayHit)
        {
            Jump();
        }
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

    private void Jump()
    {
        rigidBody.AddForce(Vector2.up * jumpModifier, ForceMode2D.Impulse);
        audioSource.Play();
    }
}
