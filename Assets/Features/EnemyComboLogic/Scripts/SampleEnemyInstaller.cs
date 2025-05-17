using UnityEngine;
using Zenject;

namespace Feature.EnemyComboLogic
{
    public class SampleEnemyInstaller : MonoInstaller
    {
        [SerializeField] private float yLevelToBeGround = -0.95f;
        public override void InstallBindings()
        {
            Container.Bind<Transform>().FromComponentOnRoot().AsSingle();
            Container.Bind<Rigidbody>().FromComponentOnRoot().AsSingle();
            Container.Bind<IDamageable>().To<EnemyDamageReactor>().AsSingle().NonLazy();
            Container.Bind<IGroundChecker>().To<SampleGroundChecker>().AsSingle().WithArguments(yLevelToBeGround).NonLazy();
            Container.BindInterfacesAndSelfTo<SampleBeatdownEmulator>().AsCached().NonLazy();
        }
    }
}
