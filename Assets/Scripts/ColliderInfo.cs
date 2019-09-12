using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderInfo : MonoBehaviour
{
	private SpawnManager spawnManager;

	private bool alreadyHit = false;

	void Start()
    {
		spawnManager = GameObject.Find("PRE_SpawnManager").GetComponent<SpawnManager>();
    }

	private void OnEnable()
	{
		alreadyHit = false;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") && !alreadyHit)
		{
			print($"Hit {collision.gameObject.name}.");

			alreadyHit = true;

			spawnManager.SetNewScene(true);
		}
	}
}