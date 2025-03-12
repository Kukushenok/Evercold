using UnityEngine;

namespace Feature.Player
{
    [CreateAssetMenu(menuName = "Configs.PlayerConfig", fileName = "PlayerConfig")]
    public class PlayerConfig: ScriptableObject
    {
        [field: SerializeField, Header("Jump Params")] public float CoyoteTime {get; private set;}
        [field: SerializeField] public CharacterControllerLocomotion.JumpSettings LocomotionSettings {get; private set;}
        
        [field: SerializeField, Space, Header("Move Params")] public float MovementSpeed {get; private set;}
        [field: SerializeField, Space, Header("Camera Params")] public float MouseSensitivity {get; private set;}

        [field: SerializeField, Space, Header("Attack Params")] public PlayerCombat.AttackSettings CombatSettings {get; private set;}
    }
}
