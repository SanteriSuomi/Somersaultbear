using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	// Starting scene prefab, to get it's tranform for later use.
	[SerializeField]
	private GameObject prefabStart;

	[SerializeField]
	private GameObject[] prefabPool;

	private Transform currentPositionTransform;

	void Start()
	{
		// Initialize current position transform variable to start with the first scene in the game, for easier instantiating of objects.
		currentPositionTransform = prefabStart.transform;
	}

	public void SetNewScene(bool hitCollider)
	{
		var random = Random.Range(0, prefabPool.Length);

		// If the selected random index in the prefab pool is not active, activate the prefab.
		if (!prefabPool[random].activeSelf)
		{
			ActivatePrefab(prefabPool[random]);
		}
		// Else select the first deactivated prefab in the pool.
		else
		{
			foreach (var prefab in prefabPool)
			{
				if (!prefab.activeSelf)
				{
					ActivatePrefab(prefab);

					break;
				}
			}
		}
	}

	private void ActivatePrefab(GameObject prefab)
	{
		print($"Activating {prefab}.");

		// Set the spawned prefab's transform position to the currentPositionTransform transform's position, plus 39.5f X vector, so they will spawn in line.
		prefab.transform.position = currentPositionTransform.transform.position + new Vector3(39.5f, 0f);

		// Make the currentPositionTransform field's value the transform of the spawned prefab, to make it easier to spawn new prefabs (scenes).
		currentPositionTransform = prefab.transform;

		prefab.SetActive(true);
	}
}