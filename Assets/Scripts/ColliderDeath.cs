using UnityEngine;
using UnityEngine.Assertions;

public class ColliderDeath : MonoBehaviour
{
    private UILogic uiLogic = default;

    private UIManager uiManager = default;

    private void Start()
    {
        uiLogic = GameObject.Find("PRE_UILogicManager").GetComponent<UILogic>();

        uiManager = GameObject.Find("PRE_UIManager").GetComponent<UIManager>();

        Assert.IsNotNull(uiLogic);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            uiManager.ShowMenuItemsDeath();
        }
    }
}
