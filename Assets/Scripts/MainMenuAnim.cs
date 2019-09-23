using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MainMenuAnim : MonoBehaviour
{
    // true == right, false == left.
    public bool Direction { get; set; } = false;

    [SerializeField]
    private LayerMask groundLayer = default;

    private Rigidbody2D rigidBody;

    [SerializeField]
    private float jumpDetectionHeight = 0.715f;

    [SerializeField]
    private float verticalSpeed = 2.5f;

    [SerializeField]
    private float jumpModifier = 5f;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector2.down, jumpDetectionHeight, groundLayer);

        Debug.DrawRay(transform.position, Vector2.down * jumpDetectionHeight, Color.green);

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
    }

    private void Jump()
    {
        rigidBody.AddForce(Vector2.up * jumpModifier, ForceMode2D.Impulse);
    }
}
