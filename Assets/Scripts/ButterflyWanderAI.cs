using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

public class ButterflyWanderAI : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    Vector2 target;

    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float radius = 2.5f;
    private float distance;

    private const float MAX_DISTANCE_FROM_TARGET = 0.2f;
    // Character AI states.
    private enum State
    {
        Wander,
        Stop
    }

    private State currentState;

    private void ChangeState(State state)
    {
        currentState = state;
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        #if UNITY_EDITOR
        Assert.IsNotNull(spriteRenderer);
        #endif
    }

    private void OnEnable()
    {
        // Get a starting point when activated.
        GetNewPoint();
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Wander:
                StartCoroutine(Move());
                break;
            case State.Stop:
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
        GetNewPoint();
    }

    private void GetNewPoint()
    {
        // Assign a random vector value to target around a circle.
        target = Random.insideUnitCircle * radius;
    }

    private void OnDrawGizmos()
    {
        // Draw a gizmo towards earch new target.
        Gizmos.DrawLine(transform.position, target);
    }
}
