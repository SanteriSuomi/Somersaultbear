using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector2 screenBounds;

    private void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
    }

    private void Update()
    {
        if (transform.position.x > screenBounds.x)
        {
            gameObject.SetActive(false);
            Debug.Log($"{gameObject.name} has been deactivated");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.SetActive(false);
        }
    }
}
