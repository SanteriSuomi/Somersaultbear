using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
	[SerializeField]
	private float verticalSpeed = 4f;

	[SerializeField]
	private float hitDetectionDistance = 1f;

	// If one direction == true, when the enemy spawns it will just charge in one direction.
	[SerializeField]
	private bool oneDirection = false;

	// If true, enable OnEnable addforce.
	[SerializeField]
	private bool onEnable = true;

	// Starting speed OnEnable/On Start.
	[SerializeField]
	private float startSpeed = 2f;

	// Starting direction, X 1/-1.
	[SerializeField]
	private Vector2 startDirection = Vector2.left;

	// Layer to use raycast with.
	[SerializeField]
	private LayerMask groundLayer = default;

	private Rigidbody2D rigidBody = default;

	private void Start()
	{
		rigidBody = GetComponent<Rigidbody2D>();
		
		// If onEnable is false, activate speed at the start of the game.
		if (!onEnable)
		{
			rigidBody.AddForce(startDirection * startSpeed, ForceMode2D.Impulse);
		}
	}

	private void OnEnable()
	{
		// If onEnable true, activate speed when the prefab gets enabled/activated.
		if (onEnable)
		{
			rigidBody.AddForce(startDirection * startSpeed, ForceMode2D.Impulse);
		}
	}

	private void FixedUpdate()
	{
		// If oneDirection is false, cast 2 rays on left and right and move to the left and right according to collisions.
		if (!oneDirection)
		{
			// Draw rays left and right and store their collision info in variables.
			RaycastHit2D hitXPositive = Physics2D.Raycast(transform.position, Vector2.right, hitDetectionDistance, groundLayer);

			Debug.DrawRay(transform.position, Vector2.right * hitDetectionDistance, Color.white);

			RaycastHit2D hitXNegative = Physics2D.Raycast(transform.position, Vector2.left, hitDetectionDistance, groundLayer);

			Debug.DrawRay(transform.position, Vector2.left * hitDetectionDistance, Color.white);

			// Valuate X+ and X- and apply force to the player rigidbody according to them.
			if (hitXPositive && hitXPositive.collider)
			{
				rigidBody.AddForce(Vector2.left * verticalSpeed, ForceMode2D.Impulse);
			}
			else if (hitXNegative && hitXNegative.collider)
			{
				rigidBody.AddForce(Vector2.right * verticalSpeed, ForceMode2D.Impulse);
			}
		}
	}
}
