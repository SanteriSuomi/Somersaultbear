using UnityEngine;

namespace Somersaultbear
{
    public class ColliderMenuAnim : MonoBehaviour
    {
        [SerializeField]
        private GameObject player = default;
        private MainMenuAnim mainMenuAnim;

        private void Awake() => mainMenuAnim = player.GetComponent<MainMenuAnim>();

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                #if UNITY_EDITOR
                Debug.Log($"{gameObject} collided with {collision.gameObject.name}");
                #endif

                // New direction is the opposite direction to the current one.
                mainMenuAnim.Direction = !mainMenuAnim.Direction;
            }
        }
    }
}