using UnityEngine;

public class EnemyWaspAI : MonoBehaviour
{
    public int HitPoints { get; set; } = 2;

    private UIManager uiManager;
    private SpriteRenderer spriteRenderer;
    private AudioSource audSource;

    private Vector3 target;

    [SerializeField]
    private float moveSpeed = 6f;

    private const float DESTROY_TIME = 15f;

    private void Awake()
    {
        uiManager = GameObject.Find("PRE_UIManager").GetComponent<UIManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Destroy(gameObject, DESTROY_TIME);
    }

    private void Update()
    {
        // Deactivate enemy when hitpoints are zero.
        if (HitPoints <= 0)
        {
            Destroy(gameObject);
        }
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
        if (!audSource.isPlaying)
        {
            audSource.Play();
        }
        // Target is the player's position.
        target = collision.transform.position;
        // Smoothly move the enemy towards player.
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
    }
}