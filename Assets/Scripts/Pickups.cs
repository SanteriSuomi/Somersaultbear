using UnityEngine;

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
    private int random;

    private const float DESTROY_DELAY = 0.5f;
    private const float DESTROY_TIME = 15f;

    private void Awake()
    {
        scoreManager = GameObject.Find("PRE_ScoreManager").GetComponent<ScoreManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        // Randomise the sprite when the object gets instantiated, and start the destroy timer.
        RandomiseSprite();
        // Start a destroy timer.
        Destroy(gameObject, DESTROY_TIME);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // On trigger, add score to the player's total.
            scoreManager.CurrentScore += scoreToGive;
            audioSource.Play();
            // Destroy the object with small delay to allow the audiosource time to play.
            Destroy(gameObject, DESTROY_DELAY);
        }
    }

    private void RandomiseSprite()
    {
        // Randomise the sprite to a random sprite in the sprites array.
        random = Random.Range(0, sprites.Length);
        spriteRenderer.sprite = sprites[random];
    }
}
