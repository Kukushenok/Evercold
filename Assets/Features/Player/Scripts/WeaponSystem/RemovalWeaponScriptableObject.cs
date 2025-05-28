using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Feature.Player.WeaponManager
{
    public interface IWeaponShootCallback
    {
        public UnityEvent OnShootButtonPressed { get; }
    }
    [CreateAssetMenu(menuName = "Game/Removal weapon")]
    public class RemovalWeaponScriptableObject : ScriptableObject, IWeapon
    {
        [field: SerializeField] public WeaponInfo WeaponInfo { get; private set; }
        public class Logic : IWeaponLogic, IWeaponShootCallback
        {
            private IShootButtonInput shootInput;
            private IPlayerWeaponInventory weaponInventory;
            private IWeapon self;
            private UnityEvent _event = new UnityEvent();
            private int times;
            public UnityEvent OnShootButtonPressed => _event;
            public Logic(IShootButtonInput shootInput, IWeapon self, IPlayerWeaponInventory weaponInventory)
            {
                this.shootInput = shootInput;
                this.self = self;
                this.weaponInventory = weaponInventory;
            }
            public bool Enabled { get; set; }

            public void Dispose()
            {
                Debug.Log("Removed weapon");
                _event.RemoveAllListeners();
            }

            public void Initialize()
            {
                Debug.Log("Weapon added");
            }

            public void Tick()
            {
                if (shootInput.RightButtonDown() && Enabled)
                {
                    times++;
                    OnShootButtonPressed?.Invoke();
                    if (times > 5)
                    {
                        weaponInventory.RemoveWeapon(self);
                    }
                }
            }

            public void FixedTick()
            {

            }
        }

        public void Install(DiContainer instantiator)
        {
            instantiator.BindInterfacesAndSelfTo<Logic>().AsSingle().NonLazy();
        }
    }
}