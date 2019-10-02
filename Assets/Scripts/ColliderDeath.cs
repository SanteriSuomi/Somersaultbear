using UnityEngine;
using UnityEngine.Assertions;

public class ColliderDeath : MonoBehaviour
{
    private UIManager uiManager = default;

    private void Awake()
    {
        uiManager = GameObject.Find("PRE_UIManager").GetComponent<UIManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Show the death menu on player collide.
        if (collision.CompareTag("Player"))
        {
            uiManager.ShowMenuItemsDeath();
        }
    }
}
