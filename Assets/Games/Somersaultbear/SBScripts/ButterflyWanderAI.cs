using System.Collections;
using UnityEngine;

namespace Somersaultbear
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ButterflyWanderAI : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        private WaitUntil moveWaitUntil;
        private Vector2 target;

        [SerializeField]
        private float moveSpeed = 5f;
        [SerializeField]
        private float maxDistanceFromTargetForStop = 0.2f;
        [SerializeField]
        private float selfDestroyTime = 15;
        private float distance;
        private int randomMoveRange = 8;

        private enum State
        {
            Wander,
            Stop
        }

        private State currentState;

        private void ChangeState(State state) 
            => currentState = state;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            moveWaitUntil = new WaitUntil(() => distance <= maxDistanceFromTargetForStop);
        }

        private void Start()
        {
            Invoke(nameof(DestroyTimer), selfDestroyTime);
            target = transform.position;
            GetNewPoint();
        }

        private void DestroyTimer() => Destroy(gameObject);

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

            FlipSprite();
        }

        private void FlipSprite()
        {
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
            yield return moveWaitUntil;
            GetNewPoint();
        }

        private void GetNewPoint()
        {
            randomMoveRange = Random.Range(0, 6);
            if (randomMoveRange <= 2)
            {
                target.x = transform.position.x + randomMoveRange;
                target.y = transform.position.y + randomMoveRange;
            }
            else
            {
                target.x = transform.position.x - randomMoveRange;
                target.y = transform.position.y - randomMoveRange;
            }
        }

        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position, target);
        }
        #endif
    }
}