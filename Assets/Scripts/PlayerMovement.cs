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

    void Start()
	{
		// Get rigidbody component and store it in the field variable.
		rigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
		// Check that rigidbody's X vector velocity is less than the max speed before giving more speed.
		if (rigidBody.velocity.x < maxVerticalSpeed)
		{
			// Move player right every fame, unless maxVerticalSpeed has been achieved.
			rigidBody.AddForce(new Vector2(verticalSpeed, 0f), ForceMode2D.Force);
		}
		
		// Keep checking for player space input and that rigidbody's Y vector velocity is close to 0 before letting the player object jump.
		if (Input.GetKeyDown(KeyCode.Space) && Mathf.Approximately(rigidBody.velocity.y, 0))
		{
			// When player presses space, jump. Also check that player is not moving.
			rigidBody.AddForce(new Vector2(0f, jumpModifier), ForceMode2D.Impulse);
		}
	}
}
