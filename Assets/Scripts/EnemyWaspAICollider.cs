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
            // Compare the gameObject's name for this instance of the script.
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
