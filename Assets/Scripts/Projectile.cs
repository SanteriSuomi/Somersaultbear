using UnityEngine;

public class Projectile : MonoBehaviour
{
    private EnemyWaspAI enemyWaspAI;

    // Deactivate the projectile after it becomes invisible to the main camera.
    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Get the hit enemy's Hitpoints value and decrease it every hit.
            enemyWaspAI = collision.gameObject.transform.parent.gameObject.GetComponent<EnemyWaspAI>();
            enemyWaspAI.HitPoints -= 1;
        }
    }
}