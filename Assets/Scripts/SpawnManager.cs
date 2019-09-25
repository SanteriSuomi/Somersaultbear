using UnityEngine;
using System.Linq;
using UnityEngine.Assertions;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabStart = default;
    [SerializeField]
    private GameObject[] prefabPool = default;

    private Transform currentPosition;

    private const float SPAWN_X_LENGTH = 39.9f;
    private const float WAIT_ACTIVATE_TIME = 0.1f;

    private void Start()
    {
        #if UNITY_EDITOR
        Assert.IsNotNull(prefabStart);
        Assert.IsNotNull(prefabPool);
        #endif

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

            #if UNITY_EDITOR
            Assert.IsNotNull(findActive);
            #endif

            ActivatePrefab(findActive);
        }
    }

    private void ActivatePrefab(GameObject prefab)
    {
        #if UNITY_EDITOR
        Debug.Log($"Activating {prefab}.");
        #endif

        // Spawn scene prefabs ahead the player using the currentPosition.
        prefab.transform.position = currentPosition.position + new Vector3(SPAWN_X_LENGTH, 0f, 0f);
        // Re-initialise the current position again using the spawned prefab.
        currentPosition = prefab.transform;
        StartCoroutine(Wait(prefab));
    }

    private IEnumerator Wait(GameObject prefab)
    {
        yield return new WaitForSeconds(1);
        prefab.SetActive(true);
    }
}