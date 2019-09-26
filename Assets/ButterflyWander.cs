using System.Collections;
using UnityEngine;

public class ButterflyWander : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float radius = 2.5f;

    private float distance;

    Vector2 target;

    private enum State
    {
        Wander,
        Stop
    }

    private State currentState;

    private void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        distance = Vector2.Distance(transform.position, target);
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        yield return new WaitUntil(() => distance <= 0.2f);
        GetNewPoint();
    }

    private void GetNewPoint()
    {
        target = Random.insideUnitCircle * radius;
    }

    private void ChangeState(State state)
    {
        currentState = state;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, target);
    }
}
