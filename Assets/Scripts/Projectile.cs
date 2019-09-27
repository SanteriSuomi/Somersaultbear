using UnityEngine;

public class Projectile : MonoBehaviour
{
    private EnemyWaspAI enemyWaspAI;
    private Vector2 screenBounds;

    private void Update()
    {
        // Get the screen bounds from pixel size to game world size.
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        // Deactivate projectile if it goes too far from screen bounds.
        if (transform.position.x > screenBounds.x || transform.position.y > screenBounds.y)
        {
            gameObject.SetActive(false);

            #if UNITY_EDITOR
            Debug.Log($"{gameObject.name} has been deactivated.");
            #endif
        }
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