﻿using System.Collections;
using UnityEngine;

namespace Somersaultbear
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ButterflyWanderAI : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        private Vector2 target;

        [SerializeField]
        private float moveSpeed = 5f;
        private float distance;
        private int randomMoveRange = 8;

        private const float MAX_DISTANCE_FROM_TARGET = 0.2f;
        private const float DESTROY_TIME = 15f;

        private void Awake() => spriteRenderer = GetComponent<SpriteRenderer>();

        private void Start()
        {
            // Start the destroy timer when prefab gets instantiated.
            Invoke(nameof(DestroyTimer), DESTROY_TIME);
            target = transform.position;
            // Start the object with new position.
            GetNewPoint();
        }

        private void DestroyTimer() => Destroy(gameObject);

        // AI states.
        private enum State
        {
            Wander,
            Stop
        }

        private State currentState;

        // Method for changing the state.
        private void ChangeState(State state) => currentState = state;

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

            FlipSprite();
        }

        private void FlipSprite()
        {
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
            randomMoveRange = Random.Range(0, 6);
            // Assign random values to the X and Y vectors of the target.
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
            // Draw a gizmo towards earch new target.
            Gizmos.DrawLine(transform.position, target);
        }
        #endif
    }
}