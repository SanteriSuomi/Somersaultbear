using UnityEngine;

namespace Somersaultbear
{
    public enum InputType
    {
        PC,
        Mobile
    }

    public abstract class InputSchemeBase : MonoBehaviour
    {
        [SerializeField]
        private InputType inputType = default;
        public InputType InputType => inputType;

        private void Update()
        {
            if (GetJumpInput())
            {
                JumpEvent?.Invoke();
            }

            (bool, Vector2) shootTuple = GetShootInput();
            if (shootTuple.Item1 
                && shootTuple.Item2.sqrMagnitude != 0) // Make sure vector is not 0
            {
                ShootEvent?.Invoke(shootTuple.Item2);
            }

            if (GetMenuInput())
            {
                MenuEvent?.Invoke();
            }
        }

        protected abstract bool GetJumpInput();
        protected abstract (bool, Vector2) GetShootInput();
        protected abstract bool GetMenuInput();

        public delegate void Jump();
        public event Jump JumpEvent;
        public delegate void Shoot(Vector2 position);
        public event Shoot ShootEvent;
        public delegate void Menu();
        public event Menu MenuEvent;
    }
}