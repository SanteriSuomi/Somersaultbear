using UnityEngine;

namespace Somersaultbear
{
    public class InputSchemePC : InputSchemeBase
    {
        protected override bool GetJumpInput() => Input.GetButton("Jump");
        protected override (bool, Vector2) GetShootInput() => (Input.GetMouseButtonDown(0), Input.mousePosition);
        protected override bool GetMenuInput() => Input.GetButtonDown("Cancel");
    }
}