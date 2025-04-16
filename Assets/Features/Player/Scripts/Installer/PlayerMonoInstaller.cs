using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Feature.Player
{
    public class PlayerInstaller : MonoInstaller
    {
        //[SerializeField] private PlayerConfig _mineConfig;
        [field: SerializeField] Transform _cameraTransform;
        private List<ILocomotionFeature> _locomotionFeatures;
        //ILocomotionData _locomotion;

        public override void InstallBindings()
        {
            Container
                .Bind<ILocomotionData>()
                .To<LocomotionData>()
                .AsSingle()
                .WithArguments(_cameraTransform);

            Container
                .Bind<ILocomotionFeature>()
                .To<JumpLocomotionFeature>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<LocomotionUpdateManager>()
                .AsSingle()
                .NonLazy();
            
            
            
            //Container.Bind<IPlayerLocomotion>().AsSingle();
            //Container.Bind<IPlayerLocomotionFeature>().To<MoveLocomotionFeature>().AsCached().WithArguments(_mineConfig.MovementSpeed);
            //Container.Bind<IPlayerLocomotionFeature>().To<CoyoteTimeJumpLocomotionFeature>().AsCached().WithArguments(_mineConfig.CoyoteTime);
            //Container.Bind<IPlayerLocomotionFeature>().To<CameraLocomotionFeature>().AsCached().WithArguments(_mineConfig.MouseSensitivity);
            //Container.Bind<CharacterControllerLocomotion.JumpSettings>().FromInstance(_mineConfig.LocomotionSettings);
            //Container.Bind<PlayerCombat.AttackSettings>().FromInstance(_mineConfig.CombatSettings);

        }
    }

    [CreateAssetMenu(menuName = "Configs.PlayerConfig", fileName = "PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField, Header("Jump Params")] public float CoyoteTime { get; private set; }
        //[field: SerializeField] public CharacterControllerLocomotion.JumpSettings LocomotionSettings {get; private set;}

        [field: SerializeField, Space, Header("Move Params")] public float MovementSpeed { get; private set; }
        [field: SerializeField, Space, Header("Camera Params")] public float MouseSensitivity { get; private set; }

        //[field: SerializeField, Space, Header("Attack Params")] public PlayerCombat.AttackSettings CombatSettings {get; private set;}
    }
}
