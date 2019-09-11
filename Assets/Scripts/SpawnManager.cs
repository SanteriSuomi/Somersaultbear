using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	// Starting scene prefab, to get it's tranform for later use.
	[SerializeField]
	private GameObject prefabStart = null;

	// Listing all the prefabs that will be added to the scene prefab object pool.
	[SerializeField]
	private GameObject prefabOne = null;

	[SerializeField]
	private GameObject prefabTwo = null;

	// Current scene prefab's transform to keep track of instantiating new scenes.
	private Transform currentPositionTransform;

	// Prefab object pool.
	private List<GameObject> prefabList = new List<GameObject>();

	void Start()
	{
		// Initialize prefab pool.
		prefabList.Add(prefabOne);
		prefabList.Add(prefabTwo);

		// Initialize current position transform variable to start with the first scene in the game, for easier instantiating of objects.
		currentPositionTransform = prefabStart.transform;
	}

	public void SetNewScene(bool hitCollider)
	{
		// Loop through the prefab pool (list) and select a scene that is not active.
		foreach (var prefab in prefabList)
		{
			// If prefab is not active, select it.
			if (!prefab.activeSelf)
			{
				print($"Activating {prefab}.");

				// Set the spawned prefab's transform position to the currentPositionTransform transform's position, plus 39.5f X vector, so they will spawn in line.
				prefab.transform.position = currentPositionTransform.transform.position + new Vector3(39.5f, 0f);

				// Make the currentPositionTransform field's value the transform of the spawned prefab, to make it easier to spawn new prefabs (scenes).
				currentPositionTransform = prefab.transform;

				// Finally make the prefab active.
				prefab.SetActive(true);

				// When found a inactive prefab, break away from the foreach loop to stop making all prefabs active.
				break;
			}
		}
	}
}
