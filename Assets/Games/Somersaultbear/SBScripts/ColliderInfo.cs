using UnityEngine;

namespace Somersaultbear
{
	public class ColliderInfo : MonoBehaviour
	{
		private SpawnManager spawnManager;
		private bool alreadyHit;

		private void Awake() => spawnManager = FindObjectOfType<SpawnManager>();

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
				spawnManager.SpawnNewScenePrefab();
			}
		}
	}
}