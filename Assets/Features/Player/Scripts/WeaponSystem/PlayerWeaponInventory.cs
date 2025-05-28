using System.Collections;
using System.Collections.Generic;
using Zenject;
namespace Feature.Player.WeaponManager
{
    public class PlayerWeaponInventory : IPlayerWeaponInventory, ITickable, IFixedTickable
    {
        private DiContainer parentDIContainer;
        private Dictionary<WeaponInfo, ContainerWatcher> CurrentInventory = new Dictionary<WeaponInfo, ContainerWatcher>();
        private List<WeaponInfo> inventoryChecklist = new List<WeaponInfo>();
        private IPlayerWeaponDisplayManager displayManager;
        private ContainerWatcher currentActiveWeapon = null;
        private PlayerInventoryInputObserver observer;
        public PlayerWeaponInventory(DiContainer parentDIContainer, IPlayerWeaponDisplayManager displayManager, IInventoryInput input)
        {
            this.parentDIContainer = parentDIContainer;
            this.displayManager = displayManager;
            observer = new PlayerInventoryInputObserver(input, this);
        }

        public bool AddWeapon(IWeapon weapon)
        {
            if (!CurrentInventory.ContainsKey(weapon.WeaponInfo))
            {
                DiContainer child = parentDIContainer.CreateSubContainer();
                child.Bind<IWeapon>().FromInstance(weapon).AsSingle();
                weapon.Install(child);
                var cv = new ContainerWatcher(child);
                CurrentInventory.Add(weapon.WeaponInfo, cv);
                inventoryChecklist.Add(weapon.WeaponInfo);
                displayManager.CreateView(weapon, child);
                cv.Initialize();
                if (inventoryChecklist.Count == 1)
                {
                    TrySetWeapon(0);
                }
                return true;
            }
            return false;
        }

        public bool RemoveWeapon(IWeapon weapon)
        {
            if(CurrentInventory.TryGetValue(weapon.WeaponInfo, out ContainerWatcher cont))
            {
                int prevIdx = -1;
                if(ReferenceEquals(cont, currentActiveWeapon))
                {
                    prevIdx = inventoryChecklist.IndexOf(weapon.WeaponInfo);
                    currentActiveWeapon.Enabled = false;
                    currentActiveWeapon = null;
                }
                cont.Dispose();
                CurrentInventory.Remove(weapon.WeaponInfo);
                inventoryChecklist.Remove(weapon.WeaponInfo);
                if(prevIdx != -1)
                {
                    TrySetWeapon(prevIdx);
                }
                return true;
            }
            return false;
        }

        public void Tick()
        {
            observer.Tick();
            foreach (ContainerWatcher w in CurrentInventory.Values) w.Tick();
        }
        public void FixedTick()
        {
            foreach (ContainerWatcher w in CurrentInventory.Values) w.FixedTick();
        }

        public void TrySetWeapon(int index)
        {
            int cnt = inventoryChecklist.Count;
            if (cnt == 0) return;
            int idx = index % cnt;
            UnityEngine.Debug.Log($"ENABLED {idx} {inventoryChecklist[idx].Identifier}");
            ChangeWeapon(inventoryChecklist[idx]);

        }
        private void ChangeWeapon(WeaponInfo otherWeapon)
        {
            if (currentActiveWeapon != null) currentActiveWeapon.Enabled = false;
            currentActiveWeapon = CurrentInventory[otherWeapon];
            currentActiveWeapon.Enabled = true;
        }
        private void SetEnabled(WeaponInfo weapon, bool state)
        {
            CurrentInventory[weapon].Enabled = state;
        }
    }
}