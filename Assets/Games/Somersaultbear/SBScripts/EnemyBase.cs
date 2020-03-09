using UnityEngine;

namespace Somersaultbear
{
    public class EnemyBase : MonoBehaviour
    {
        protected UIManager uiManager;

        [SerializeField]
        protected float moveSpeed = 3;
        protected const float DESTROY_TIME = 15;

        protected virtual void Awake() => uiManager = FindObjectOfType<UIManager>();

        protected void PlayerCollisionEvent() => uiManager.DeathMenu();
    }
}