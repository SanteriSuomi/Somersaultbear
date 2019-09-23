using UnityEngine;
using UnityEngine.Assertions;

public class ColliderMenuAnim : MonoBehaviour
{
    private MainMenuAnim mmAnim;

    void Start()
    {
        mmAnim = GameObject.Find("PRE_Menu_Player").GetComponent<MainMenuAnim>();

        Assert.IsNotNull(mmAnim);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            #if UNITY_EDITOR
            Debug.Log($"{gameObject} collided with {collision.gameObject.name}");
            #endif

            mmAnim.Direction = !mmAnim.Direction;
        }
    }
}
