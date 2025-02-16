using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Feature.Player
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private MineConfig _mineConfig;
        public override void InstallBindings()
        {
            //base.InstallBindings(); // todo
            Container.Bind<PlayerInput>().AsSingle();
            Container.Bind<IPlayerLocomotionFeature>().To<MoveLocomotionFeature>().AsCached().WithArguments(_mineConfig.MovementSpeed);
            Container.Bind<IPlayerLocomotionFeature>().To<CoyoteTimeJumpLocomotionFeature>().AsCached().WithArguments(_mineConfig.CoyoteTime);
            Container.Bind<IPlayerLocomotionFeature>().To<CameraLocomotionFeature>().AsCached().WithArguments(_mineConfig.MouseSensitivity);
            Container.Bind<CharacterControllerLocomotion.JumpSettings>().FromInstance(_mineConfig.LocomotionSettings);
            Container.Bind<PlayerCombat.AttackSettings>().FromInstance(_mineConfig.CombatSettings);
            //Container.Bind<MonoBehaviour>()
            
        }
    }

    [CreateAssetMenu(menuName = "Configs.PlayerConfig", fileName = "PlayerConfig")]
    public class MineConfig: ScriptableObject
    {
        [field: SerializeField, Header("Jump Params")] public float CoyoteTime {get; private set;}
        [field: SerializeField] public CharacterControllerLocomotion.JumpSettings LocomotionSettings {get; private set;}
        
        [field: SerializeField, Space, Header("Move Params")] public float MovementSpeed {get; private set;}
        [field: SerializeField, Space, Header("Camera Params")] public float MouseSensitivity {get; private set;}

        [field: SerializeField, Space, Header("Attack Params")] public PlayerCombat.AttackSettings CombatSettings {get; private set;}
    }
}
