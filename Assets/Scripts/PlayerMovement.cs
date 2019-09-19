using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float verticalSpeed = 3f;

    [SerializeField]
    private float maxVerticalSpeed = 6.5f;

    [SerializeField]
    private float jumpModifier = 8f;

    [SerializeField]
    private float jumpDetectionHeight = 0.715f;

    private bool pressedSpace;

    private const float rbYVelocityMax = 0.5f;

    [SerializeField]
    private LayerMask groundLayer = default;

    private Rigidbody2D rigidBody = default;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Ask for player input and store it in pressedSpace as a boolean.
        pressedSpace = Input.GetButton("Jump");
    }

    private void FixedUpdate()
    {
        if (rigidBody.velocity.x < maxVerticalSpeed)
        {
            rigidBody.AddForce(Vector2.right * verticalSpeed, ForceMode2D.Force);
        }

        // Detect if there is a collider below player object and store it in raycasthit2d. Only detect objects with ground layer applied to them.
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector2.down, jumpDetectionHeight, groundLayer);

        Debug.DrawRay(transform.position, Vector2.down * jumpDetectionHeight, Color.green);

        // Jump when ray hits the specified target, space is being pressed and rigidbody Y velocity is less than the constant.
        if (rayHit && rayHit.collider && pressedSpace && rigidBody.velocity.y < rbYVelocityMax)
        {
            rigidBody.AddForce(Vector2.up * jumpModifier, ForceMode2D.Impulse);
        }
    }
}