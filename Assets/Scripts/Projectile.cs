using UnityEngine;

namespace Somersaultbear
{
    public class Projectile : MonoBehaviour
    {
        public Rigidbody2D Rigidbody2D { get; private set; }

        private void Awake() => Rigidbody2D = GetComponent<Rigidbody2D>();
        
        // Deactivate the projectile after it becomes invisible to the main camera.
        private void OnBecameInvisible() => gameObject.SetActive(false);

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                // Get the hit enemy's Hitpoints value and decrease it every hit.
                EnemyWaspAI enemyWaspAI = collision.gameObject.transform.parent.gameObject.GetComponent<EnemyWaspAI>();
                enemyWaspAI.HitPoints -= 1;
            }
        }
    }
}