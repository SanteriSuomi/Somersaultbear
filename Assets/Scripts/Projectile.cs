using UnityEngine;
using UnityEngine.Assertions;

public class Projectile : MonoBehaviour
{
    private EnemyWaspAI enemyWaspAI;
    private Vector2 screenBounds;

    // Deactivate the projectile after it becomes invisible to the main camera.
    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);

        #if UNITY_EDITOR
        Debug.Log($"{gameObject.name} has been deactivated.");
        #endif
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Get the hit enemy's Hitpoints value and decrease it every hit.
            enemyWaspAI = collision.gameObject.transform.parent.gameObject.GetComponent<EnemyWaspAI>();
            
            #if UNITY_EDITOR
            Assert.IsNotNull(enemyWaspAI);
            #endif

            enemyWaspAI.HitPoints -= 1;
        }
    }
}