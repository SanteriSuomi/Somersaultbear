using UnityEngine;

namespace Somersaultbear
{
    public class ColliderDeath : MonoBehaviour
    {
        private UIManager uiManager = default;

        private void Awake() 
            => uiManager = FindObjectOfType<UIManager>();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                uiManager.DeathMenu();
            }
        }
    }
}