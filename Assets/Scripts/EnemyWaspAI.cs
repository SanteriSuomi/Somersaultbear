using UnityEngine;
using UnityEngine.Assertions;

public class EnemyWaspAI : MonoBehaviour
{
    public int HitPoints { get; set; } = 2;

    private UIManager uiManager;
    private SpriteRenderer spriteRenderer;

    private Vector3 target;
    private Vector3 spawnPos;

    [SerializeField]
    private float moveSpeed = 6f;

    private void Awake()
    {
        spawnPos = transform.position;
    }

    private void Start()
    {
        uiManager = GameObject.Find("PRE_UIManager").GetComponent<UIManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        #if UNITY_EDITOR
        Assert.IsNotNull(uiManager);
        Assert.IsNotNull(spriteRenderer);
        #endif
    }

    private void OnEnable()
    {
        transform.position = spawnPos;
        HitPoints = 2;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        // Deactivate enemy when hitpoints are zero.
        if (HitPoints <= 0)
        {
            gameObject.SetActive(false);
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
        // Target is the player's position.
        target = collision.transform.position;
        // Smoothly move the enemy towards player.
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
    }
}