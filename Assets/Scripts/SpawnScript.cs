using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    private enum SpawnType
    {
        Enemy,
        Boulder,
        Pickup
    }

    [SerializeField]
    private SpawnType spawnType = SpawnType.Enemy;

    [SerializeField]
    private GameObject[] characters = default;
    [SerializeField]
    private GameObject boulder = default;
    [SerializeField]
    private GameObject pickUp = default;

    private void OnEnable()
    {
        // Determine the spawn type of this instance.
        switch (spawnType)
        {
            case SpawnType.Enemy:
                int random = Random.Range(0, characters.Length);
                GameObject character = Instantiate(characters[random]);
                character.transform.position = gameObject.transform.position;
                break;
            case SpawnType.Boulder:
                GameObject item = Instantiate(boulder);
                item.transform.position = gameObject.transform.position;
                break;
            case SpawnType.Pickup:
                GameObject pickup = Instantiate(pickUp);
                pickup.transform.position = gameObject.transform.position;
                break;
            default:
                #if UNITY_EDITOR
                Debug.Log("No spawn type specified in SpawnScript");
                #endif
                break;
        }
    }
}
