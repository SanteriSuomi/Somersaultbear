using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector2 screenBounds;

    private Renderer spriteRenderer;

    private void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }
}
