using UnityEngine;
using UnityEngine.Assertions;

public class ColliderDeath : MonoBehaviour
{
    [SerializeField]
    private UIManager uiManager = default;

    // Assert that the reference is not null, and only run this in the Unity editor.
    #if UNITY_EDITOR
    private void Start()
    {
        Assert.IsNotNull(uiManager);
    }
    #endif

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            uiManager.ShowMenuItemsDeath();
        }
    }
}
