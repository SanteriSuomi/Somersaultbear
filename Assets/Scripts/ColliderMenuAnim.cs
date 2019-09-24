using UnityEngine;
using UnityEngine.Assertions;

public class ColliderMenuAnim : MonoBehaviour
{
    private MainMenuAnim mmAnim;

    private void Start()
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

            // New direction is the opposite direction to the current one.
            mmAnim.Direction = !mmAnim.Direction;
        }
    }
}
