using UnityEngine;

namespace Somersaultbear
{
    public class EnemyWaspAI : MonoBehaviour
    {
        public int HitPoints { get; set; } = 2;

        private UIManager uiManager;
        private SpriteRenderer spriteRenderer;
        private AudioSource audioSource;
        private Vector3 target;

        [SerializeField]
        private float moveSpeed = 3;
        private const float DESTROY_TIME = 15;

        private void Awake()
        {
            uiManager = GameObject.Find("PRE_UIManager").GetComponent<UIManager>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            audioSource = GetComponent<AudioSource>();
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
        public void ColliderBody()
        {
            // Show the death menu.
            uiManager.ShowMenuItemsDeath();
        }

        public void ColliderFollow(Collider2D collision)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }

            // Target is the player's position.
            target = collision.transform.position;
            // Smoothly move the enemy towards player.
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        }
    }
}