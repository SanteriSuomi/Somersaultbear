using Mirror;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
	[SerializeField]
	private float verticalSpeed = 3f;

	[SerializeField]
	private float maxVerticalSpeed = 6.5f;

	[SerializeField]
	private float jumpModifier = 8f;

	[SerializeField]
	private float jumpDetectionHeight = 0.715f;

	[SerializeField]
	private LayerMask groundLayer = default;

	private Rigidbody2D rigidBody = default;

	void Start()
	{
		rigidBody = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate()
	{
        if (!isLocalPlayer)
        {
            return;
        }

		if (rigidBody.velocity.x < maxVerticalSpeed)
		{
			rigidBody.AddForce(Vector2.right * verticalSpeed, ForceMode2D.Force);
		}

		// Detect if there is a collider below player object and store it in raycasthit2d. Only detect objects with ground layer applied to them.
		RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector2.down, jumpDetectionHeight, groundLayer);

		Debug.DrawRay(transform.position, Vector2.down * jumpDetectionHeight, Color.red);

		if (rayHit && rayHit.collider && Input.GetButtonDown("Jump"))
		{
			rigidBody.AddForce(Vector2.up * jumpModifier, ForceMode2D.Impulse);
		}
	}
}