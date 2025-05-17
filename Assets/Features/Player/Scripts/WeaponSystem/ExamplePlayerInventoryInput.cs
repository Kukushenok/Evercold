using UnityEngine;
using Zenject;
namespace Feature.Player.WeaponManager
{
    public class ExamplePlayerInventoryInput : IInventoryInput, ITickable
    {
        public int SelectedItemIndex { get
            {
                return idx;
            }
        }
        int idx;

        public void Tick()
        {
            for (int i = 0; i < 9; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha0 + i))
                {
                    idx = i;
                    break;
                }
            }
        }
    }
}