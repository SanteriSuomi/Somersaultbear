using System.Collections;
using System.Collections.Generic;
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

	[SerializeField]
	private LayerMask groundLayer = default;

	private Rigidbody2D rigidBody = default;

	void Start()
	{
		rigidBody = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate()
	{
		// Check that rigidbody's X vector velocity is less than the max speed before giving more speed.
		if (rigidBody.velocity.x < maxVerticalSpeed)
		{
			// Move player vertically.
			rigidBody.AddForce(Vector2.right * verticalSpeed, ForceMode2D.Force);
		}

		// Detect if there is a collider below player object and store it in raycasthit2d. Only detect objects with ground layer applied to them.
		RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector2.down, jumpDetectionHeight, groundLayer);

		// Draw a debug ray.
		Debug.DrawRay(transform.position, Vector2.down * jumpDetectionHeight, Color.red);

		// Ask if rayhit and rayhit collider return true before asking for input.
		if (rayHit && Input.GetButtonDown("Jump"))
		{
			// Add impulse force for the jump.
			rigidBody.AddForce(Vector2.up * jumpModifier, ForceMode2D.Impulse);
		}
	}
}