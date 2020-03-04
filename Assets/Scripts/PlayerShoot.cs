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
        private LayerMask mobileShootMask = default;
        //[SerializeField]
        //private LayerMask mobileShootRayMask = default;
        [SerializeField]
        private Vector3 mobileShootRadiusOffset = new Vector3(3.75f, 1.5f, 0);
        [SerializeField]
        private Vector3 mobileShootOffset = new Vector3(0, 0.5f, 0);

        [SerializeField]
        private float projectileSpeed = 10f;
        [SerializeField]
        private float shootCooldown = 0.125f;
        [SerializeField]
        private int boulderLayer = 10;
        [SerializeField]
        private int projectileLayer = 11;
        [SerializeField]
        private float mobileShootRadius = 10;
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
                Shoot(position, true);
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
                    Shoot(boolPos.Item2, false);
                }
            }
        }

        private (bool, Vector2) GetEnemyPositionInRadius()
        {
            Collider2D[] circleResults = Physics2D.OverlapCircleAll(transform.position + mobileShootRadiusOffset, 
                mobileShootRadius, mobileShootMask);
            if (circleResults.Length > 0)
            {
                return (true, circleResults[0].transform.position);

                //int amountOfRayResults = Physics2D.LinecastNonAlloc(transform.position, circleResults[0].transform.position, 
                //    null, mobileShootRayMask);
                //Debug.Log("Amount of results : " + amountOfRayResults);
                //if (amountOfRayResults == 1)
                //{
                //    return (true, circleResults[0].transform.position);
                //}

                //return (false, Vector2.zero);
            }

            return (false, Vector2.zero);
        }

        private void Shoot(Vector2 position, bool transformPoint)
        {
            TransformPositionToWorld(position, transformPoint);
            PlayShootSound();
            LaunchProjectile();
        }

        private void TransformPositionToWorld(Vector2 position, bool transformPoint)
        {
            if (transformPoint)
            {
                target = (Camera.main.ScreenToWorldPoint(position) - transform.position).normalized;
            }
            else
            {
                Vector3 positionAsVector3 = new Vector2(position.x, position.y); // Convert to vector3
                target = (positionAsVector3 - transform.position).normalized;
            }
            
            if (InputManager.Instance.InputScheme.InputType == InputType.Mobile)
            {
                target += mobileShootOffset; // Offset the shoot pos to account for projectile drop (on mobile)
            }

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

        #if UNITY_EDITOR
        private void OnDrawGizmos() 
            => Gizmos.DrawWireSphere(transform.position + mobileShootRadiusOffset, mobileShootRadius);
        #endif
    }
}