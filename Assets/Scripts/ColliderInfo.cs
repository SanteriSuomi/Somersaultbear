using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderInfo : MonoBehaviour
{
	private SpawnManager spawnManager = default;

	// Prevent spawning duplicates by using a boolean lock.
	private bool alreadyHit = false;

	void Start()
    {
		spawnManager = GameObject.Find("PRE_SceneSpawnManager").GetComponent<SpawnManager>();
    }

	private void OnEnable()
	{
		// By default, when a scene prefab gets spawned, alreadyHit is false (player's hasn't hit the collider yet).
		alreadyHit = false;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// If collision is player, and alreadyHit is false.
		if (collision.CompareTag("Player") && !alreadyHit)
		{
			print($"Hit {collision.gameObject.name}.");

			// Change alreadyHit to true to prevent this from activating again in this instance of the prefab.
			alreadyHit = true;

			// Spawn a new scene using the spawnManager.
			spawnManager.SetNewScene(true);
		}
	}
}