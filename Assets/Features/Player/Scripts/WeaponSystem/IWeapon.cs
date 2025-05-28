using System;
using Zenject;
namespace Feature.Player.WeaponManager
{
    public interface IWeapon
    {
        public void Install(DiContainer instantiator);
        public WeaponInfo WeaponInfo { get; }

    }
}