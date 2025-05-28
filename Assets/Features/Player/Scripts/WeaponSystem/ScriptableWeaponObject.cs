using UnityEngine;
using Zenject;
namespace Feature.Player.WeaponManager
{
    public abstract class ScriptableWeaponObject : ScriptableObject, IWeapon
    {
        public WeaponInfo WeaponInfo { get => new WeaponInfo() { Identifier = name, WeaponViewPrefab = WeaponView }; }
        public abstract GameObject WeaponView { get; }

        public abstract void Install(DiContainer instantiator);
    }
}