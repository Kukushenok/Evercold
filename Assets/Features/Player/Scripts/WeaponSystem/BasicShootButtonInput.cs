using UnityEngine;
namespace Feature.Player.WeaponManager
{
    public class BasicShootButtonInput : IShootButtonInput
    {
        public bool LeftButtonDown() => Input.GetMouseButtonDown(0);
        public bool RightButtonDown() => Input.GetMouseButtonDown(1);
    }
}