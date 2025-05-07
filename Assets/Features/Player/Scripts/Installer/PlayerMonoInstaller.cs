using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Feature.Player
{
    public class PlayerInstaller : MonoInstaller
    {
        [field: SerializeField] Transform _cameraTransform;
        [SerializeField] PlayerConfig _playerConfig;

        public override void InstallBindings()
        {
            Container
                .Bind<PlayerConfig>()
                .FromInstance(_playerConfig)
                .AsSingle();

            Container
                .BindInterfacesTo<PlayerChecker>()
                .FromNewComponentOn(this.gameObject)
                .AsSingle()
                .WithArguments(_playerConfig);
                
            Container
                .BindInterfacesTo<PlayerFacade>()
                .FromNewComponentOn(this.gameObject)
                .AsSingle();
            
            Container
                .Bind<IPlayerLocomotionStatus>()
                .To<PlayerLocomotionStatus>()
                .AsSingle();

            Container
                .Bind<IPlayerLocomotion>()
                .To<PlayerLocomotion>()
                .AsSingle()
                .WithArguments(transform, _cameraTransform);

            Container
                .BindInterfacesAndSelfTo<LocomotionUpdateManager>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<PlayerInputActions>()
                .AsSingle();
            
            Container
                .Bind<IMovementInput>()
                .To<KeyboardPlayerInput>()
                .AsSingle()
                .WithArguments(new PlayerInputActions())
                .NonLazy();
            
            Container
                .Bind<Rigidbody>()
                .FromInstance(GetComponent<Rigidbody>())
                .AsSingle();
            
            Container
                .Bind<CharacterController>()
                .FromInstance(GetComponent<CharacterController>())
                .AsSingle();
            
            // locomotion features
            //Container
            //    .Bind<ILocomotionFeature>()
            //    .To<JumpLocomotionFeature>()
            //    .AsSingle();
            Container
                .Bind<ILocomotionFeature>()
                .To<CurveJumpLocomotionFeature>()
                .AsSingle();
            Container
                .Bind<ILocomotionFeature>()
                .To<GravityLocomotionFeature>()
                .AsSingle();
            Container
                .Bind<ILocomotionFeature>()
                .To<MoveLocomotionFeature>()
                .AsSingle();
            Container
                .Bind<ILocomotionFeature>()
                .To<CameraMoveLocomotionFeature>()
                .AsSingle();
            Container
                .Bind<ILocomotionFeature>()
                .To<SlideLocomotionFeature>()
                .AsSingle();
            
            
            
            
            //Container.Bind<IPlayerLocomotion>().AsSingle();
            //Container.Bind<IPlayerLocomotionFeature>().To<MoveLocomotionFeature>().AsCached().WithArguments(_mineConfig.MovementSpeed);
            //Container.Bind<IPlayerLocomotionFeature>().To<CoyoteTimeJumpLocomotionFeature>().AsCached().WithArguments(_mineConfig.CoyoteTime);
            //Container.Bind<IPlayerLocomotionFeature>().To<CameraLocomotionFeature>().AsCached().WithArguments(_mineConfig.MouseSensitivity);
            //Container.Bind<CharacterControllerLocomotion.JumpSettings>().FromInstance(_mineConfig.LocomotionSettings);
            //Container.Bind<PlayerCombat.AttackSettings>().FromInstance(_mineConfig.CombatSettings);

        }
    }
}
