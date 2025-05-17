using System.Collections.Generic;
using UnityEngine;
using Zenject;
namespace Feature.Player.WeaponManager
{

    public class PlayerInventoryInstaller: MonoInstaller
    {
        public class WeaponAdder
        {
            
            public WeaponAdder(List<IWeapon> initWeapons, IPlayerWeaponInventory inventory)
            {
                foreach(var inv in initWeapons)
                {
                    inventory.AddWeapon(inv);
                }
            }
        }
        [SerializeField] private Transform weaponViewParentTransform;
        [SerializeField] private List<BasicWeaponScriptableObject> initWeapons;
        [SerializeField] private List<RemovalWeaponScriptableObject> removalWeapons;
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<PlayerWeaponInventory>().AsSingle().NonLazy();
            Container.Bind<IPlayerWeaponDisplayManager>().To<GameObjectViewWeaponDisplayManager>().AsSingle().WithArguments(weaponViewParentTransform).NonLazy();
            Container.BindInterfacesTo<ExamplePlayerInventoryInput>().AsSingle().NonLazy();
            Container.BindInterfacesTo<BasicShootButtonInput>().AsSingle().NonLazy();
            List<IWeapon> weapons = new List<IWeapon>();
            weapons.AddRange(initWeapons);
            weapons.AddRange(removalWeapons);
            Container.Bind<WeaponAdder>().AsSingle().WithArguments(weapons).NonLazy();
            
        }
        
    }
}