using UnityEngine;
using UnityEngine.Assertions;

public class ColliderInfo : MonoBehaviour
{
    [SerializeField]
	private SpawnManager spawnManager = default;

	private bool alreadyHit = false;

    // Assert that the reference is not null, and only run this in the Unity editor.
    #if UNITY_EDITOR
    private void Start()
    {
        Assert.IsNotNull(spawnManager);
    }
    #endif

	private void OnEnable()
	{
        // Every time the gameObject is enabled, alreadyHit should be false.
		alreadyHit = false;
	}

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