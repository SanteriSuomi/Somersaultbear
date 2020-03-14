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
        private Transform currentTransform;
        private WaitForSeconds waitUntilActivate;

        [SerializeField]
        private float spawnXOffset = 39.9f;
        [SerializeField]
        private float activateDelayTime = 0.1f;

        private void Awake() => waitUntilActivate = new WaitForSeconds(activateDelayTime);

        private void Start() => SetInitialSpawnPos();

        private void SetInitialSpawnPos() => currentTransform = prefabStart.transform;

        public void SpawnNewScenePrefab()
        {
            int random = Random.Range(0, prefabPool.Length);
            if (!prefabPool[random].activeSelf)
            {
                ActivatePrefab(prefabPool[random]);
            }
            else
            {
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
            prefab.transform.position = currentTransform.position + new Vector3(spawnXOffset, 0, 0);
            currentTransform = prefab.transform;
        }

        private IEnumerator WaitUntilActivate(GameObject prefab)
        {
            yield return waitUntilActivate;
            prefab.SetActive(true);
        }
    }
}