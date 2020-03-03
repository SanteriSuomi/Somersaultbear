using UnityEngine;

namespace Somersaultbear
{
    public class InputSchemeMobile : InputSchemeBase
    {
        protected override bool GetJumpInput()
        {
            // Handled from UI
            return false;
        }

        protected override bool GetMenuInput()
        {
            // Handled from UI
            return false;
        }

        protected override (bool, Vector2) GetShootInput()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                return (true, touch.position);
            }

            return (false, Vector2.zero);
        }
    }
}