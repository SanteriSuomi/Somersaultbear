using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Require rigidbody for movement.
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
	private Rigidbody2D rigidBody;

	[SerializeField]
	private float verticalSpeed = 3f;

	[SerializeField]
	private float maxVerticalSpeed = 6.5f;

	[SerializeField]
	private float jumpModifier = 8f;

	private const float jumpVelocityLock = 0.1f;

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
			rigidBody.AddForce(new Vector2(verticalSpeed, 0f), ForceMode2D.Force);
		}

		// Keep checking for player space input and and limit jumping to when the player object's rigidbody's velocity is between the constant -jumpVelocityLock and +jumpVelocityLock.
		if (Input.GetButtonDown("Jump") && rigidBody.velocity.y > -jumpVelocityLock && rigidBody.velocity.y < jumpVelocityLock)
		{
			// Add impulse force to the player for the jump.
			rigidBody.AddForce(new Vector2(0f, jumpModifier), ForceMode2D.Impulse);
		}
	}
}