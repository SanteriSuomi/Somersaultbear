using System.Collections;
using UnityEngine;
using System.Linq;

public class SpawnManager : MonoBehaviour
{
    private const float spawnInXAxis = 39.9f;

    private const float waitActivateTime = 0.1f;

    // Starting scene prefab, to get it's tranform for later use.
    [SerializeField]
    private GameObject prefabStart = default;

    [SerializeField]
    private GameObject[] prefabPool = default;

    private Transform currentPositionTransform;

    private void Start()
    {
        // Initialize current position transform variable to start with the first scene in the game, for easier instantiating of objects.
        currentPositionTransform = prefabStart.transform;
    }

    public void SpawnNewScenePrefab()
    {
        int random = Random.Range(0, prefabPool.Length);

        // If the selected random index in the prefab pool is not active, activate the prefab.
        if (!prefabPool[random].activeSelf)
        {
            ActivatePrefab(prefabPool[random]);
        }
        // Else select the first deactivated prefab in the pool.
        else
        {
            // Query prefab pool array.
            var findActive = prefabPool.Where(p => !p.activeSelf).FirstOrDefault();

            // Make sure that the selected prefab isn't null.
            if (findActive != null)
            {
                ActivatePrefab(findActive);
            }
            else
            {
                #if UNITY_EDITOR
                print($"{findActive} is null. Finding prefab in the pool failed.");
                #endif
            }
        }
    }

    private void ActivatePrefab(GameObject prefab)
    {
        #if UNITY_EDITOR
        print($"Activating {prefab}.");
        #endif

        // Set the spawned prefab's transform position to the currentPositionTransform transform's position,
        // plus spawnInXAxis vector, so it will spawn ahead of it in line.
        prefab.transform.position = currentPositionTransform.transform.position + new Vector3(spawnInXAxis, 0f, 0f);

        // Make the currentPositionTransform field's value the transform of the spawned prefab, to make it easier to spawn new prefabs.
        currentPositionTransform = prefab.transform;

        StartCoroutine(WaitActivate(prefab));
    }

    // Wait for specified amount of time to help prevent flashing when spawning a new prefab.
    private IEnumerator WaitActivate(GameObject prefab)
    {
        yield return new WaitForSeconds(waitActivateTime);

        prefab.SetActive(true);
    }
}