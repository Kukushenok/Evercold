using System;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
namespace Feature.Player.WeaponManager
{
    [CreateAssetMenu(menuName = "Game/Basic weapon")]
    public class BasicWeaponScriptableObject : ScriptableObject, IWeapon
    {
        [field: SerializeField] public WeaponInfo WeaponInfo { get; private set; }
        public class Logic : IWeaponLogic, IWeaponShootCallback
        {
            private IShootButtonInput shootInput;
            public UnityEvent _event = new UnityEvent();
            public Logic(IShootButtonInput shootInput)
            {
                this.shootInput = shootInput;
            }
            public bool Enabled { get; set; }

            UnityEvent IWeaponShootCallback.OnShootButtonPressed => _event;

            public void Dispose()
            {
                Debug.Log("Removed weapon");
            }

            public void Initialize()
            {
                Debug.Log("Weapon added");
            }

            public void Tick()
            {
                if (shootInput.RightButtonDown() && Enabled)
                {
                    _event?.Invoke();
                    Debug.Log("Shoot button pressed!");
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