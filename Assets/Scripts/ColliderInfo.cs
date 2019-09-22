using UnityEngine;

public class ColliderInfo : MonoBehaviour
{
    [SerializeField]
	private SpawnManager spawnManager = default;

	// Prevent spawning duplicates by using a boolean lock.
	private bool alreadyHit = false;

	private void Start()
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
		if (collision.CompareTag("Player") && !alreadyHit)
		{
            #if UNITY_EDITOR
			print($"Hit {collision.gameObject.name}.");
            #endif

			// Change alreadyHit to true to prevent this from activating again in this instance of the prefab.
			alreadyHit = true;

			spawnManager.SpawnNewScenePrefab();
		}
	}
}