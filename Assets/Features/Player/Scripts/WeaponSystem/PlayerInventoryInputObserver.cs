using UnityEngine;
using Zenject;
namespace Feature.Player.WeaponManager
{
    public class PlayerInventoryInputObserver: ITickable
    {
        private IInventoryInput inventoryInput;
        private PlayerWeaponInventory weaponInventory;
        private int prevIndex;
        public PlayerInventoryInputObserver(IInventoryInput inventoryInput, PlayerWeaponInventory inventory)
        {
            this.inventoryInput = inventoryInput;
            prevIndex = inventoryInput.SelectedItemIndex;
            this.weaponInventory = inventory;
        }

        public void Tick()
        {
            int idx = inventoryInput.SelectedItemIndex;
            if(idx != prevIndex)
            {
                weaponInventory.TrySetWeapon(idx);
                prevIndex = idx;
            }
        }
    }
}