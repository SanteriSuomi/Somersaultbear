using UnityEngine;

namespace Somersaultbear
{
	public class ColliderInfo : MonoBehaviour
	{
		private SpawnManager spawnManager = default;
		private bool alreadyHit = false;

		private void Awake() => spawnManager = FindObjectOfType<SpawnManager>();

		// Every time the gameObject is enabled, alreadyHit should be false.
		private void OnEnable() => alreadyHit = false;

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.CompareTag("Player") && !alreadyHit)
			{
				#if UNITY_EDITOR
				Debug.Log($"Hit {collision.gameObject.name}.");
				#endif

				// Change alreadyHit to true to prevent this from activating again in this instance of the prefab.
				alreadyHit = true;
				// Spawn a new scene prefab using the method in spawnManager.
				spawnManager.SpawnNewScenePrefab();
			}
		}
	}
}