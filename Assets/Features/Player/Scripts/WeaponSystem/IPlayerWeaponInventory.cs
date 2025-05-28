namespace Feature.Player.WeaponManager
{
    public interface IPlayerWeaponInventory
    {
        public bool AddWeapon(IWeapon weapon);
        public bool RemoveWeapon(IWeapon weapon);
        
    }
}