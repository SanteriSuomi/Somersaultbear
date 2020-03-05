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
        private int scoreToGive = 100;

        private const float DESTROY_DELAY = 0.5f;
        private const float DESTROY_TIME = 15f;

        private void Awake()
        {
            scoreManager = FindObjectOfType<ScoreManager>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            // Randomise the sprite when the object gets instantiated, and start the destroy timer.
            RandomiseSprite();
            // Start a destroy timer.
            Invoke(nameof(DestroyTimer), DESTROY_TIME);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                // On trigger, add score to the player's total.
                scoreManager.CurrentScore += scoreToGive;
                PlayPickupSound();
                // Destroy the object with small delay to allow the audiosource time to play.
                Invoke(nameof(DestroyTimer), DESTROY_DELAY);
            }
        }

        private void PlayPickupSound() => audioSource.Play();

        private void DestroyTimer() => Destroy(gameObject);

        private void RandomiseSprite()
        {
            // Randomise the sprite to a random sprite in the sprites array.
            int random = Random.Range(0, sprites.Length);
            spriteRenderer.sprite = sprites[random];
        }
    }
}