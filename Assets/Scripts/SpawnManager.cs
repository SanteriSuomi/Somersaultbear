using System.Collections;
using UnityEngine;
using System.Linq;
using UnityEngine.Assertions;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabStart = default;

    [SerializeField]
    private GameObject[] prefabPool = default;

    private Transform currentPosition;

    private const float spawnInXAxis = 39.9f;

    private const float waitActivateTime = 0.1f;

    private void Start()
    {
        // Initialise the currentPosition with the first scene prefab in the game.
        currentPosition = prefabStart.transform;
    }

    public void SpawnNewScenePrefab()
    {
        int random = Random.Range(0, prefabPool.Length);

        // Check if the random prefab in the pool is true and activate it.
        if (!prefabPool[random].activeSelf)
        {
            ActivatePrefab(prefabPool[random]);
        }
        // Otherwise select the first deactivated prefab in the pool.
        else
        {
            var findActive = prefabPool.Where(p => !p.activeSelf).FirstOrDefault();

            Assert.IsNotNull(findActive);

            ActivatePrefab(findActive);
        }
    }

    private void ActivatePrefab(GameObject prefab)
    {
        #if UNITY_EDITOR
        Debug.Log($"Activating {prefab}.");
        #endif

        // Spawn scene prefabs ahead the player using the currentPosition.
        prefab.transform.position = currentPosition.position + new Vector3(spawnInXAxis, 0f, 0f);

        // Re-initialise the current position again using the spawned prefab.
        currentPosition = prefab.transform;

        prefab.SetActive(true);
    }
}