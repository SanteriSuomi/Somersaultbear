using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyWaspAI : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    private Vector3 target;

    private bool isInArea = false;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();    
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            target = other.transform.position;

            Vector3.Lerp(transform.position, target, 0.5f);
        }
        else
        {

        }
    }

    void Update()
    {
        /*
        if (isInArea)
        {
            Vector3.Lerp(transform.position, target, 0.5f);
        }
        */
    }
}
