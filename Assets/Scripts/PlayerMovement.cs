using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Require rigidbody for movement.
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
	// Rigidbody field.
	private Rigidbody2D rigidBody;

	// Vertical movement speed modifier.
	[SerializeField]
	private float verticalSpeed = 3f;

	// Maximum vertical speed of player object.
	[SerializeField]
	private float maxVerticalSpeed = 6.5f;

	// Jump modifier.
	[SerializeField]
	private float jumpModifier = 8f;

    void Start()
	{
		// Get component and store it in the field variable.
		rigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
		if (rigidBody.velocity.x < maxVerticalSpeed)
		{
			// Move player right every fame, unless maxVerticalSpeed has been achieved.
			rigidBody.AddForce(new Vector2(verticalSpeed, 0f), ForceMode2D.Force);
		}
		
		if (Input.GetKeyDown(KeyCode.Space) && Mathf.Approximately(rigidBody.velocity.y, 0))
		{
			// When player presses space, jump. Also check that player is not moving.
			rigidBody.AddForce(new Vector2(0f, jumpModifier), ForceMode2D.Impulse);
		}
    }
}
