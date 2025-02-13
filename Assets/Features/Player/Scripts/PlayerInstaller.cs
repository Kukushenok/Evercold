using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Feature.Player
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private float coyoteTime = 0.2f;
        public override void InstallBindings()
        {
            //base.InstallBindings(); // todo
            Container.Bind<PlayerInput>().AsSingle();
            Container.Bind<IPlayerLocomotionFeature>().To<MoveLocomotionFeature>().AsCached().WithArguments(5f);
            Container.Bind<IPlayerLocomotionFeature>().To<CoyoteTimeJumpLocomotionFeature>().AsCached().WithArguments(0.2f);
            Container.Bind<IPlayerLocomotionFeature>().To<CameraLocomotionFeature>().AsCached().WithArguments(2f);

            
        }
    }
}
