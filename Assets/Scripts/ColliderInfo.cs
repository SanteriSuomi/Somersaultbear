using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderInfo : MonoBehaviour
{
	// Scene spawn manager script reference.
	private SpawnManager spawnManager;

    void Start()
    {
		// Find the spawn manager gameobject and get the script to access it's methods.
		spawnManager = GameObject.Find("PRE_SpawnManager").GetComponent<SpawnManager>();
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			print($"Hit {collision.gameObject.name}");

			spawnManager.SetNewScene(true);
		}
	}
}
