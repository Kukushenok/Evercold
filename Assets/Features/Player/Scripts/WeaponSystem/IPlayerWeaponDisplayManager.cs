using Zenject;
namespace Feature.Player.WeaponManager
{
    public interface IPlayerWeaponDisplayManager
    {
        public void CreateView(IWeapon gm, DiContainer instantiator);
    }
}