using UnityEngine;

namespace Somersaultbear
{
    [RequireComponent(typeof(SpriteRenderer), typeof(AudioSource))]
    public class Pickups : MonoBehaviour
    {
        [SerializeField]
        private Sprite[] sprites = default;
        private SpriteRenderer spriteRenderer;
        private AudioSource audioSource;
        private ScoreManager scoreManager;

        [SerializeField]
        private float selfDestroyDelay = 0.5f;
        [SerializeField]
        private float selfDestroyTime = 15;
        [SerializeField]
        private int scoreToGive = 100;

        private void Awake()
        {
            scoreManager = FindObjectOfType<ScoreManager>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            RandomiseSprite();
            Invoke(nameof(DestroyTimer), selfDestroyTime);
        }

        private void RandomiseSprite()
        {
            int random = Random.Range(0, sprites.Length);
            spriteRenderer.sprite = sprites[random];
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                scoreManager.CurrentScore += scoreToGive;
                PlayPickupSound();
                Invoke(nameof(DestroyTimer), selfDestroyDelay);
            }
        }

        private void PlayPickupSound() => audioSource.Play();

        private void DestroyTimer() => Destroy(gameObject);
    }
}