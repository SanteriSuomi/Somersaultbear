using UnityEngine;

namespace Somersaultbear
{
    public class EnemyBase : MonoBehaviour
    {
        protected UIManager uiManager;

        [SerializeField]
        protected float moveSpeed = 3;
        [SerializeField]
        protected float selfDestroyTime = 15;

        protected virtual void Awake() => uiManager = FindObjectOfType<UIManager>();

        protected void PlayerCollisionEvent() => uiManager.DeathMenu();
    }
}