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
        [field: SerializeField] public int MaxJumps = 2;
        [field: SerializeField, Header("Jump Additional Params"), Tooltip("if vertical velocity is higher than this value, player is considered jumping")]
        public float LowestJumpingVelocity = 0.1f; 
        [field: SerializeField, Tooltip("if vertical velocity is higher than this value, player is considered falling")]
        public float HighestFallingVelocity = 0.1f;

        [field: SerializeField, Space, Header("Move Params")] public float MovementSpeed { get; private set; }
        [field: SerializeField, Space, Header("Camera Params")] public float MouseSensitivity { get; private set; }
        [field: SerializeField, Space, Header("Wall Check Params"), Tooltip("example: colliding sphere radius")]
        public float WallCheckFigureSize = 1f;
        [field: SerializeField, Tooltip("the direction in which the figure will be shifted")] 
        public Vector3 LeftWallCheckDirection = Vector3.left;
        [field: SerializeField, Tooltip("the direction in which the figure will be shifted")] 
        public Vector3 RightWallCheckDirection = Vector3.right;
        [field: SerializeField] public float WallCheckFigureShiftDistance = 1f;
        [field: SerializeField] public LayerMask WallLayerMask;

        [Space]
        [field: SerializeField, Header("Ground Check Params"), Tooltip("example: colliding sphere radius")]
        public float GroundCheckFigureSize = 1f; 
        [field: SerializeField] public float GroundCheckFigureShiftDistance = 1f;
        public LayerMask GroundLayerMask;

        [field: SerializeField, Space, Header("Debug Params")] 
        public bool ShowGroundCheckGizmos = false;
        [field: SerializeField] public bool ShowWallCheckGizmos = false;

        //[field: SerializeField, Space, Header("Attack Params")] public PlayerCombat.AttackSettings CombatSettings {get; private set;}
    }
}
