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
            // Handled from UI
            return (false, Vector2.zero);
        }
    }
}