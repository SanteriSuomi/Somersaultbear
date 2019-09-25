using UnityEngine;
using UnityEngine.Assertions;

public class EnemyWaspAI : MonoBehaviour
{
    private UIManager uiManager;
    private SpriteRenderer spriteRenderer;

    private Vector3 target;

    [SerializeField]
    private float moveSpeed = 1f;

    private void Start()
    {
        uiManager = GameObject.Find("PRE_UIManager").GetComponent<UIManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        #if UNITY_EDITOR
        Assert.IsNotNull(uiManager);
        Assert.IsNotNull(spriteRenderer);
        #endif
    }

    private void Update()
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

    public void ColliderBody()
    {
        uiManager.ShowMenuItemsDeath();
    }

    public void ColliderFollow(Collider2D collision)
    {
        target = collision.transform.position;
        transform.position = Vector3.Lerp(transform.position, target, moveSpeed * Time.deltaTime);
    }
}
