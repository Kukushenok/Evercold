using UnityEngine;
using Zenject;

namespace Feature.EnemyComboLogic
{
    public class SampleEnemyInstaller : MonoInstaller
    {
        [SerializeField] private float yLevelToBeGround = -0.95f;
        [SerializeField] private EnemyDamageReactor.Settings sample;
        public override void InstallBindings()
        {
            Container.Bind<Transform>().FromComponentOnRoot().AsSingle();
            Container.Bind<Rigidbody>().FromComponentOnRoot().AsSingle();
            Container.BindInterfacesTo<EnemyDamageReactor>().FromNewComponentOnRoot().AsSingle().WithArguments(sample).NonLazy();
            Container.Bind<IGroundChecker>().To<SampleGroundChecker>().AsSingle().WithArguments(yLevelToBeGround).NonLazy();
            Container.Bind<IEnemyDamageReactorCallbackReciever>().FromComponentInChildren();
            //Container.BindInterfacesAndSelfTo<SampleBeatdownEmulator>().AsCached().NonLazy();
        }
    }
}
