using UnityEngine;
using UnityEngine.Assertions;

public class EnemyWaspAICollider : MonoBehaviour
{
    private EnemyWaspAI enemyWaspAI;

    private void Start()
    {
        enemyWaspAI = GetComponentInParent<EnemyWaspAI>();

        #if UNITY_EDITOR
        Assert.IsNotNull(enemyWaspAI);
        #endif
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
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
                Debug.Log("Collider name not found in EnemyWaspAICollider");
            }
        }
    }

    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (gameObject.name == "Collider")
        {
            Gizmos.DrawWireSphere(transform.position, 0.35f);
        }
        else if (gameObject.name == "Collider (1)")
        {
            Gizmos.DrawWireSphere(transform.position, 10f);
        }
    }
    #endif
}
