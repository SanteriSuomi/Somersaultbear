using UnityEngine;

namespace Somersaultbear
{
    public class Projectile : MonoBehaviour
    {
        public Rigidbody2D Rigidbody { get; private set; }
        public Collider2D Collider { get; private set; }

        [SerializeField]
        private float maxTimeAlive = 5;
        [SerializeField]
        private int projectileDamage = 1;

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
            if (collision.CompareTag("Enemy") 
                && collision.transform.parent.TryGetComponent(out EnemyWaspAI enemy))
            {
                enemy.HitPoints -= projectileDamage;
            }
        }

        private void ReturnObject()
        {
            CancelInvoke();
            ProjectilePool.Instance.Return(this);
        } 
    }
}