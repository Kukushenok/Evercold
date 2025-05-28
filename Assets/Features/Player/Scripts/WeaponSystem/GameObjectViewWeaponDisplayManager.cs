using UnityEngine;
using Zenject;
namespace Feature.Player.WeaponManager
{
    public class GameObjectViewWeaponDisplayManager : IPlayerWeaponDisplayManager
    {
        private Transform transform;
        public GameObjectViewWeaponDisplayManager(Transform transform)
        {
            this.transform = transform;
        }

        public void CreateView(IWeapon gm, DiContainer instantiator)
        {
            GameObject g = instantiator.InstantiatePrefab(gm.WeaponInfo.WeaponViewPrefab, transform);
            instantiator.Bind<IWeaponView>().FromMethod(x=>g.GetComponent<IWeaponView>()).AsCached();
            g.SetActive(false);
        }
    }
}