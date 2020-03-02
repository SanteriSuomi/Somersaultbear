using UnityEngine;

namespace Somersaultbear
{
    public class EnemyWaspAICollider : MonoBehaviour
    {
        private EnemyWaspAI enemyWaspAI;

        private void Awake() => enemyWaspAI = GetComponentInParent<EnemyWaspAI>();

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                // Call methods on the main enemyWaspAI script 
                if (gameObject.name == "Collider")
                {
                    enemyWaspAI.ColliderBody();
                }
                else if (gameObject.name == "Collider (1)")
                {
                    enemyWaspAI.ColliderFollow(collision);
                }
                else
                {
                    #if UNITY_EDITOR
                    Debug.Log("Collider name not found in EnemyWaspAICollider");
                    #endif
                }
            }
        }
    }
}