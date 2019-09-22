using UnityEngine;
using UnityEngine.Assertions;

public class ColliderInfo : MonoBehaviour
{
	private SpawnManager spawnManager = default;

	private bool alreadyHit = false;

	private void Start()
    {
        // Use GameObject.Find because otherwise you would have to reference each instance by hand.
		spawnManager = GameObject.Find("PRE_SpawnManager").GetComponent<SpawnManager>();

        Assert.IsNotNull(spawnManager);
    }

	private void OnEnable()
	{
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

			spawnManager.SpawnNewScenePrefab();
		}
	}
}