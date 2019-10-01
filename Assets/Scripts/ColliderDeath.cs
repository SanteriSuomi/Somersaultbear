using UnityEngine;
using UnityEngine.Assertions;

public class ColliderDeath : MonoBehaviour
{
    private UIManager uiManager = default;

    private void Start()
    {
        // Find the Manager prefab manually, to prevent having to select it for every instance manually.
        uiManager = GameObject.Find("PRE_UIManager").GetComponent<UIManager>();

        #if UNITY_EDITOR
        Assert.IsNotNull(uiManager);
        #endif
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
