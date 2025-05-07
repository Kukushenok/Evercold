using System.ComponentModel;
using UnityEngine;

namespace Feature.Player
{
    [CreateAssetMenu(menuName = "Configs.PlayerConfig", fileName = "PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        //[field: SerializeField, Header("General Params")]
        //public bool _curvedJump = enum
        [field: SerializeField] public float Gravity {get; private set;} = -9.8f;
        [field: SerializeField, Header("Jump Params")] public float CoyoteTime { get; private set; }
        [field: SerializeField] public float JumpHeight = 0f; // TODO: Make it really jump height, not jump force
        //[field: SerializeField] public CharacterControllerLocomotion.JumpSettings LocomotionSettings {get; private set;}
        [field: SerializeField] public int MaxJumps = 2;
        [field: SerializeField, Tooltip("(sorry for bad Inglish) Shows jump trajectory, after t=1 player may be still falling so you need to expand curve further")]
        public AnimationCurve JumpCurve;
        [field: SerializeField, Tooltip("This value is used with jump curve")] public float JumpDuration;
        [field: SerializeField, Header("Jump Additional Params"), Tooltip("if vertical velocity is higher than this value, player is considered jumping")]
        public float LowestJumpingVelocity = 0.1f; 
        [field: SerializeField, Tooltip("if vertical velocity is lower than this value, player is considered falling")]
        public float HighestFallingVelocity = 0.1f;
        [field: SerializeField, Header("SlideParams"), Tooltip("This value added to player velocity when sliding")]
        public float SlideStrength = 10f;
        [field: SerializeField] public Vector3 SlideDirection = Vector3.forward; // todo readonly
        [field: SerializeField] public Vector3 CameraSlideLocalPosition = new Vector3(0f, -1f, 0f);
        [field: SerializeField] public Vector3 CameraDefaultLocalPosition = new Vector3(0f, 0.658f, 0f);

        [field: SerializeField, Space, Header("Move Params")] public float MovementSpeed { get; private set; }
        [field: SerializeField] public float MovementDrag = 1f;
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
