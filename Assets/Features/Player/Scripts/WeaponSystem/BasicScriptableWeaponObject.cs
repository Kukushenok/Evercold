using UnityEngine;
using Zenject;
namespace Feature.Player.WeaponManager
{

    public abstract class BasicWeaponObject : ScriptableWeaponObject
    {
        [Header("Please note that NAME of the asset should be unique")]

        [SerializeField] private GameObject weaponView;
        public override GameObject WeaponView => weaponView;
    }
    public abstract class SimpleLogicWeaponObject<T>: BasicWeaponObject where T: class, IWeaponLogic
    {
        public override void Install(DiContainer instantiator)
        {
            instantiator.BindInterfacesAndSelfTo<T>().AsSingle().NonLazy();
        }
    }
}