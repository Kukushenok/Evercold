using UnityEngine;

namespace Feature.Player
{
    [CreateAssetMenu(menuName = "Configs.PlayerConfig", fileName = "PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public float Gravity = -9.8f;
        [field: SerializeField, Header("Jump Params")] public float CoyoteTime { get; private set; }
        [field: SerializeField] public float JumpHeight = 0f; // TODO: Make it really jump height, not jump force
        //[field: SerializeField] public CharacterControllerLocomotion.JumpSettings LocomotionSettings {get; private set;}

        [field: SerializeField, Space, Header("Move Params")] public float MovementSpeed { get; private set; }
        [field: SerializeField, Space, Header("Camera Params")] public float MouseSensitivity { get; private set; }
        [field: SerializeField, Space, Header("Wall Check Params"), Tooltip("example: colliding sphere radius")]
        public float WallCheckFigureSize = 1f;
        [field: SerializeField, Tooltip("the direction in which the figure will be shifted")] 
        public Vector3 LeftWallCheckDirection = Vector3.left;
        [field: SerializeField, Tooltip("the direction in which the figure will be shifted")] 
        public Vector3 RightWallCheckDirection = Vector3.right;
        [field: SerializeField] public float CheckFigureShiftDistance = 1f;
        [field: SerializeField] public LayerMask WallLayerMask;

        [field: SerializeField, Space, Header("Ground Check Params"), Tooltip("example: colliding sphere radius")]
        public float GroundCheckFigureSize = 1f; 
        [field: SerializeField] public float GroundCheckFigureShiftDistance = 1f;
        public LayerMask GroundLayerMask;

        //[field: SerializeField, Space, Header("Attack Params")] public PlayerCombat.AttackSettings CombatSettings {get; private set;}
    }
}
