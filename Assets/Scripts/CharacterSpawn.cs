using UnityEngine;
using UnityEngine.Assertions;

public class CharacterSpawn : MonoBehaviour
{
    // Flag for if this instance is a spawn point for a boulder.
    [SerializeField]
    private bool isBoulderSpawn = false;

    [SerializeField]
    GameObject[] characters = default;
    [SerializeField]
    GameObject boulder = default;

    private void Start()
    {
        #if UNITY_EDITOR
        Assert.IsNotNull(characters);    
        Assert.IsNotNull(boulder);    
        #endif
    }

    private void OnEnable()
    {
        if (!isBoulderSpawn)
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
            GameObject character = Instantiate(boulder);
            character.transform.position = gameObject.transform.position;
        }
    }
}
