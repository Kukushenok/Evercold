using Features.MainMenu;
using UnityEngine;
using Zenject;

public class DummyGameManagerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IGameManager>().To<DummyGameManager>().AsSingle();
    }
}