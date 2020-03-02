using System.Collections;
using System.Linq;
using UnityEngine;

namespace Somersaultbear
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject prefabStart = default;
        [SerializeField]
        private GameObject[] prefabPool = default;
        private Transform currentPosition;
        private WaitForSeconds waitUntilActivate;

        private const float SPAWN_X_LENGTH = 39.9f;
        private const float WAIT_ACTIVATE_TIME = 0.1f;

        private void Awake() => waitUntilActivate = new WaitForSeconds(WAIT_ACTIVATE_TIME);

        private void Start() => SetInitialSpawnPos();

        // Initialize the currentPosition with the first scene prefab in the game.
        private void SetInitialSpawnPos() => currentPosition = prefabStart.transform;

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
                GameObject prefab = prefabPool.FirstOrDefault(p => !p.activeSelf);

                if (prefab != null)
                {
                    ActivatePrefab(prefab);
                }
            }
        }

        private void ActivatePrefab(GameObject prefab)
        {
            #if UNITY_EDITOR
            Debug.Log($"Activating {prefab}.");
            #endif

            SetPrefabPosition(prefab);
            StartCoroutine(WaitUntilActivate(prefab));
        }

        private void SetPrefabPosition(GameObject prefab)
        {
            // Spawn scene prefabs ahead the currentPosition.
            prefab.transform.position = currentPosition.position + new Vector3(SPAWN_X_LENGTH, 0, 0);
            // Re-initialize the current position again using the spawned prefab.
            currentPosition = prefab.transform;
        }

        private IEnumerator WaitUntilActivate(GameObject prefab)
        {
            // Wait the specified amount of seconds until activating the prefab.
            yield return waitUntilActivate;
            prefab.SetActive(true);
        }
    }
}