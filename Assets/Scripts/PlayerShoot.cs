using UnityEngine;

namespace Somersaultbear
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerShoot : MonoBehaviour
    {
        private AudioSource[] audioSources = default;
        private Collider2D playerCollider;
        private Vector3 target;

        [SerializeField]
        private float projectileSpeed = 10f;
        [SerializeField]
        private float shootCooldown = 0.125f;
        [SerializeField]
        private int boulderLayer = 10;
        [SerializeField]
        private int projectileLayer = 11;
        private float timer;

        private void Awake()
        {
            audioSources = GetComponents<AudioSource>();
            playerCollider = GetComponent<Collider2D>();
        }

        private void Start() => InputManager.Instance.InputScheme.ShootEvent += OnShoot;

        private void Update() => timer += Time.deltaTime;

        private void OnShoot(Vector2 position)
        {
            if (timer >= shootCooldown)
            {
                timer = 0;
                TransformPositionToWorld(position);
                PlayShootSound();
                LaunchProjectile();
            }
        }

        public void OnShoot() // Mobile on shoot (from UI)
        {
            if (timer >= shootCooldown)
            {
                timer = 0;
                (bool, Vector2) boolPos = GetEnemyPositionInRadius();
                if (boolPos.Item1)
                {
                    TransformPositionToWorld(boolPos.Item2);
                    PlayShootSound();
                    LaunchProjectile();
                }
            }
        }

        private (bool, Vector2) GetEnemyPositionInRadius()
        {
            Collider2D[] results = Physics2D.OverlapCircleAll(transform.position, 20);
            if (results.Length > 0)
            {
                Vector3 currentEnemyPosition = Vector3.zero;
                float currentDistance = Mathf.Infinity;
                int resultsLength = results.Length - 1;
                for (int i = 0; i < resultsLength; i++)
                {
                    if (results[i].CompareTag("Enemy"))
                    {
                        float distance = (results[i].transform.position - transform.position).sqrMagnitude;
                        if (distance < currentDistance)
                        {
                            currentDistance = distance;
                            currentEnemyPosition = results[i].transform.position;
                        }
                    }
                }

                return (true, currentEnemyPosition);
            }

            return (false, Vector2.zero);
        }

        private void TransformPositionToWorld(Vector2 position)
        {
            target = (Camera.main.ScreenToWorldPoint(position) - gameObject.transform.position).normalized;
            target.z = 0f;
        }

        private void PlayShootSound() => audioSources[1].Play();

        private void LaunchProjectile()
        {
            Projectile projectile = ProjectilePool.Instance.Get();
            if (projectile != null)
            {
                #if UNITY_EDITOR
                Debug.Log($"Launched {projectile.name}.");
                #endif

                projectile.transform.position = transform.position;
                projectile.transform.rotation = Quaternion.identity;
                projectile.Rigidbody.AddForce(new Vector2(target.x * projectileSpeed,
                    target.y * projectileSpeed), ForceMode2D.Impulse);
                IgnoreCollisionOn(projectile);
            }
        }

        private void IgnoreCollisionOn(Projectile projectile)
        {
            Physics2D.IgnoreCollision(projectile.Collider, playerCollider);
            Physics2D.IgnoreLayerCollision(boulderLayer, projectileLayer);
        }
    }
}