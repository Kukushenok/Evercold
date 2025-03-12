using Feature.MainMenu;
using UnityEngine;
using Zenject;
namespace Feature.MainMenu
{
    public class DummyGameManagerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameManager>().To<DummyGameManager>().AsSingle();
        }
    }
}