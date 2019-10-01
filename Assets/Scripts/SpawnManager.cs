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

        // Initialize the currentPosition with the first scene prefab in the game.
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
            // Linq query to get the first active prefab in the pool.
            GameObject findActive = prefabPool.Where(prefab => !prefab.activeSelf).FirstOrDefault();

            if (findActive != null)
            {
                ActivatePrefab(findActive);
            }
        }
    }

    private void ActivatePrefab(GameObject prefab)
    {
        #if UNITY_EDITOR
        Debug.Log($"Activating {prefab}.");
        #endif

        // Spawn scene prefabs ahead the currentPosition.
        prefab.transform.position = currentPosition.position + new Vector3(SPAWN_X_LENGTH, 0, 0);
        // Re-initialize the current position again using the spawned prefab.
        currentPosition = prefab.transform;
        StartCoroutine(Wait(prefab));
    }

    private IEnumerator Wait(GameObject prefab)
    {
<<<<<<< HEAD
        yield return new WaitForSeconds(WAIT_ACTIVATE_TIME);
=======
        // Wait the specified amount of seconds until activating the prefab.
        yield return new WaitForSeconds(1);
>>>>>>> 80c99525be2bd5149b4e5ffa8eb7fd962660f4d4
        prefab.SetActive(true);
    }
}