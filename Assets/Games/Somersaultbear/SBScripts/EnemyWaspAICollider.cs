using UnityEngine;

namespace Somersaultbear
{
    public class EnemyWaspAICollider : MonoBehaviour
    {
        private enum ColliderType
        {
            Body,
            Follow
        }

        private EnemyWaspAI enemyWaspAI;
        [SerializeField]
        private ColliderType colliderType = default;

        private void Awake() => enemyWaspAI = GetComponentInParent<EnemyWaspAI>();

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                // Call methods on the main enemyWaspAI script 
                switch (colliderType)
                {
                    case ColliderType.Body:
                        enemyWaspAI.ColliderBody();
                        break;

                    case ColliderType.Follow:
                        enemyWaspAI.ColliderFollow(collision);
                        break;

                    default:
                        break;
                }
            }
        }
    }
}