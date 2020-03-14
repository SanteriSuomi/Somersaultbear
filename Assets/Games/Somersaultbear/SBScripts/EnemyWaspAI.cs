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

        protected override void Awake()
        {
            base.Awake();
            spriteRenderer = GetComponent<SpriteRenderer>();
            audioSource = GetComponent<AudioSource>();
            HitPoints = hitPoints;
        }

        private void Start() => Invoke(nameof(DestroyTimer), selfDestroyTime);

        private void DestroyTimer() => Destroy(gameObject);

        private void Update()
        {
            DestroyOnHitpointsZero();
            FlipSprite();
        }

        private void DestroyOnHitpointsZero()
        {
            if (HitPoints <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void FlipSprite()
        {
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
            transform.position = Vector3.MoveTowards(transform.position, target, 
                moveSpeed * Time.deltaTime);
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