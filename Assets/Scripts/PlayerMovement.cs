using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
	[SerializeField]
	private float verticalSpeed = 3f;

	[SerializeField]
	private float maxVerticalSpeed = 6.5f;

	[SerializeField]
	private float jumpModifier = 8f;

	[SerializeField]
	private float jumpDetectionHeight = 0.7f;

	[SerializeField]
	private LayerMask groundLayer;

	private Rigidbody2D rigidBody;

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
		Debug.DrawRay(transform.position, Vector2.down, Color.green);
		
		// Ask if rayhit and rayhit collider return true before asking for input.
		if (rayHit && rayHit.collider) 
		{
			if (Input.GetButtonDown("Jump"))
			{
				// Add impulse force for the jump.
				rigidBody.AddForce(Vector2.up * jumpModifier, ForceMode2D.Impulse);
			}
		}
	}
}