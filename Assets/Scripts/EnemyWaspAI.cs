using UnityEngine;

namespace Somersaultbear
{
    public class EnemyWaspAI : EnemyBase
    {
        public int HitPoints { get; set; }
        [SerializeField]
        private int hitPoints = 2;

        private SpriteRenderer spriteRenderer;
        private AudioSource audioSource;
        private Vector3 target;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            audioSource = GetComponent<AudioSource>();
            HitPoints = hitPoints;
        }

        private void Start() => Invoke(nameof(DestroyTimer), DESTROY_TIME);

        private void DestroyTimer() => Destroy(gameObject);

        private void Update()
        {
            DestroyOnHitpointsZero();
            FlipSprite();
        }

        private void DestroyOnHitpointsZero()
        {
            // Deactivate enemy when hitpoints are zero.
            if (HitPoints <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void FlipSprite()
        {
            // Flip the sprite direction.
            if (target.x > transform.position.x)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }

        // Methods that get called from the child colliders.
        public void ColliderBody() => PlayerCollisionEvent();

        public void ColliderFollow(Collider2D collision)
        {
            PlayFollowSound();
            target = collision.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        }

        private void PlayFollowSound()
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}