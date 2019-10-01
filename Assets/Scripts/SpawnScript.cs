using UnityEngine;
using UnityEngine.Assertions;

public class SpawnScript : MonoBehaviour
{
    [SerializeField]
    private bool isBoulderSpawn = false;
    [SerializeField]
    private bool isPickupSpawn = false;

    [SerializeField]
    private GameObject[] characters = default;
    [SerializeField]
    private GameObject boulder = default;
    [SerializeField]
    private GameObject pickUp = default;

    private void Start()
    {
        #if UNITY_EDITOR
        Assert.IsNotNull(characters);    
        Assert.IsNotNull(boulder);  
        Assert.IsNotNull(pickUp);    
        #endif
    }

    private void OnEnable()
    {
        // Determine what type this spawnscript instance should be - boulder, pick up or normal enemy spawn using the bool flags.
        if (!isBoulderSpawn && !isPickupSpawn)
        {
            // Get a random number.
            int random = Random.Range(0, characters.Length);
            // Instantiate a new random character and store it's reference.
            GameObject character = Instantiate(characters[random]);
            // Set the spawned prefab's position to the spawn point.
            character.transform.position = gameObject.transform.position;
        }
        else if (isBoulderSpawn)
        {
            GameObject item = Instantiate(boulder);
            item.transform.position = gameObject.transform.position;
        }
        else if (isPickupSpawn)
        {
            GameObject pickup = Instantiate(pickUp);
            pickup.transform.position = gameObject.transform.position;
        }
    }
}
