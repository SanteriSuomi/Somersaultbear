using System.Collections;
using UnityEngine;

public class ButterflyWanderAI : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private Vector2 target;

    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField] [Range(0, 15)]
    private float randomMoveRange = 8f;
    private float distance;

    private const float MAX_DISTANCE_FROM_TARGET = 0.2f;
    private const float DESTROY_TIME = 15f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        // Start the destroy timer when prefab gets instantiated.
        Destroy(gameObject, DESTROY_TIME);
        target = transform.position;
        // Start the object with new position.
        GetNewPoint();
    }

    // AI states.
    private enum State
    {
        Wander,
        Stop
    }

    private State currentState;

    // Method for changing the state.
    private void ChangeState(State state)
    {
        currentState = state;
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Wander:
                StartCoroutine(Move());
                break;
            case State.Stop:
                // No state logic here as of yet.
                break;
        }
        // Flip the sprite depending on it's direction.
        if (target.x > transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    private IEnumerator Move()
    {
        // Calculate the distance between the butterfly and the target point.
        distance = Vector2.Distance(transform.position, target);
        // Start moving (Lerp) towards the new target.
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        yield return new WaitUntil(() => distance <= MAX_DISTANCE_FROM_TARGET);
        // Get a new target point.
        GetNewPoint();
    }

    private void GetNewPoint()
    {
        randomMoveRange = Random.Range(0, 8);
        // Assign random values to the X and Y vectors of the target.
        if (randomMoveRange <= randomMoveRange / 2)
        {
            target.x = transform.position.x - randomMoveRange;
            target.y = transform.position.y + randomMoveRange;
        }
        else
        {
            target.x = transform.position.x + randomMoveRange;
            target.y = transform.position.y - randomMoveRange;
        }
    }

    private void OnDrawGizmos()
    {
        // Draw a gizmo towards earch new target.
        Gizmos.DrawLine(transform.position, target);
    }
}