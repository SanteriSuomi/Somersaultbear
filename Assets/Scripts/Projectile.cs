using UnityEngine;

namespace Somersaultbear
{
    public class Projectile : MonoBehaviour
    {
        public Rigidbody2D Rigidbody { get; private set; }
        public Collider2D Collider { get; private set; }

        [SerializeField]
        private float maxTimeAlive = 5;

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Collider = GetComponent<Collider2D>();
        }

        private void OnEnable() => Invoke(nameof(DisableTimer), maxTimeAlive);

        private void DisableTimer() => ReturnObject();

        private void OnBecameInvisible() => ReturnObject();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                // Get the hit enemy's Hitpoints value and decrease it every hit.
                EnemyWaspAI enemyWaspAI = collision.gameObject.transform.parent.gameObject.GetComponent<EnemyWaspAI>();
                enemyWaspAI.HitPoints -= 1;
            }
        }

        private void OnDisable() => CancelInvoke();

        private void ReturnObject() => ProjectilePool.Instance.Return(this);
    }
}