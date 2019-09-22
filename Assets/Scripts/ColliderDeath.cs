using UnityEngine;
using UnityEngine.Assertions;

public class ColliderDeath : MonoBehaviour
{
    private UILogic uiLogic = default;

    private void Start()
    {
        // Use GameObject.Find because otherwise you would have to reference each instance by hand.
        uiLogic = GameObject.Find("PRE_UILogicManager").GetComponent<UILogic>();

        Assert.IsNotNull(uiLogic);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            uiLogic.ChangeScene("SCE_GameLoop");
        }
    }
}
