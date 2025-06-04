using System.ComponentModel;
using UnityEngine;

namespace Feature.Player
{
    [CreateAssetMenu(menuName = "Configs.PlayerConfig", fileName = "PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        //[field: SerializeField, Header("General Params")]
        //public bool _curvedJump = enum
        [field: SerializeField] public float Gravity { get; private set; } = -9.8f;
        [field: SerializeField, Header("Jump Params")] public float CoyoteTime { get; private set; }
        public enum JumpType
        {
            Normal,
            Curve
        }
        [field: SerializeField] public JumpType PlayerJumpType { get; private set; } = JumpType.Normal;
        [field: SerializeField] public int MaxJumps { get; private set; } = 2;
        [ field: SerializeField, Header("Normal Jump Params")] public float JumpForce { get; private set; } = 10f;
        [field: SerializeField, Header("Curve Jump Params")] public float JumpHeight { get; private set; } = 0f; 
        //[field: SerializeField] public CharacterControllerLocomotion.JumpSettings LocomotionSettings {get; private set;}
        
        [field: SerializeField, Tooltip("(sorry for bad Inglish) Shows jump trajectory, after t=1 player may be still falling so you need to expand curve further")]
        public AnimationCurve JumpCurve { get; private set; }
        [field: SerializeField, Tooltip("This value is used with jump curve")] public float JumpDuration { get; private set; }
        [field: SerializeField, Header("Wall Jump Params")] public float WallJumpSideForce = 10f;
        [field: SerializeField, Header("Jump Additional Params"), Tooltip("if vertical velocity is higher than this value, player is considered jumping")]
        public float LowestJumpingVelocity { get; private set; } = 0.1f;
        [field: SerializeField, Tooltip("if vertical velocity is lower than this value, player is considered falling")]
        public float HighestFallingVelocity { get; private set; } = 0.1f;
        [field: SerializeField, Header("SlideParams"), Tooltip("This value added to player velocity when sliding")]

        public float SlideLength { get; private set; } = 10f;
        public enum SlideType
        {
            Addition,
            Multiply
            //Curved // todo
        }
        [field: SerializeField] public SlideType PlayerSlideType { get; private set; } = SlideType.Addition;
        //[field: SerializeField] public AnimationCurve SlideCurve { get; private set; }
        [field: SerializeField] public Vector3 SlideDirection { get; private set; } = Vector3.forward; // todo readonly
        // [field: SerializeField] public float SlideDuration = 1f;
        [field: SerializeField] public Vector3 CameraSlideLocalPosition { get; private set; } = new Vector3(0f, -1f, 0f);
        [field: SerializeField] public Vector3 CameraDefaultLocalPosition { get; private set; } = new Vector3(0f, 0.658f, 0f);
        [field: SerializeField] public float DefaultColliderHeight { get; private set; } = 2f;
        [field: SerializeField] public float SlideColliderHeight { get; private set; } = 1f;

        [field: SerializeField, Header("Dash Params")]
        public float DashSpeed { get; private set; } = 1f;
        [field: SerializeField] public float DashDuration { get; private set; } = 10f;
        [field: SerializeField] public int MaxDashAmount { get; private set; } = 3;


        public enum SlamType
        {
            SpeedAndAcceleration // todo
            //Curve // todo
        }
        [field: SerializeField, Header("Slam Params")]
        public SlamType PlayerSlamType { get; private set; } = SlamType.SpeedAndAcceleration;
        // [field: SerializeField] public AnimationCurve SlamCurve;
        [field: SerializeField] public float SlamStartSpeed { get; private set; } = 1f;
        [field: SerializeField] public float SlamAcceleration { get; private set; } = 1f;
        [field: SerializeField] public float SlamRayCheckerHeight { get; private set; } = 3f;
        [field: SerializeField] public float SlamRayCheckerRadius { get; private set; } = 0.1f;
        [field: SerializeField, Space, Header("Move Params")] public float MovementSpeed { get; private set; }
        [field: SerializeField] public float MovementDrag { get; private set; } = 1f;
        [field: SerializeField] public float AirMovementDrag { get; private set; } = 0.1f;
        [field: SerializeField, Range(0, 1)] public float AirControlMultiplyer { get; private set; } = 1f;
        [field: SerializeField, Space, Header("Camera Params")] public float MouseSensitivity { get; private set; }
        [field: SerializeField, Space, Header("Wall Check Params"), Tooltip("example: colliding sphere radius")]
        public float WallCheckFigureSize { get; private set; } = 1f;
        [field: SerializeField, Tooltip("the direction in which the figure will be shifted")]
        public Vector3 LeftWallCheckDirection { get; private set; } = Vector3.left;
        [field: SerializeField, Tooltip("the direction in which the figure will be shifted")]
        public Vector3 RightWallCheckDirection { get; private set; } = Vector3.right;
        [field: SerializeField] public float WallCheckFigureShiftDistance { get; private set; } = 1f;
        [field: SerializeField] public LayerMask WallLayerMask { get; private set; }


        [field: SerializeField, Header("Ground Check Params"), Tooltip("example: colliding sphere radius")]
        public float GroundCheckFigureSize { get; private set; } = 1f;
        [field: SerializeField] public float GroundCheckFigureShiftDistance { get; private set; } = 1f;
        [field: SerializeField] public LayerMask GroundLayerMask { get; private set; }

        [field: SerializeField, Space, Header("Debug Params")]
        public bool ShowGroundCheckGizmos { get; private set; } = false;
        [field: SerializeField] public bool ShowWallCheckGizmos { get; private set; } = false;


        //[field: SerializeField, Space, Header("Attack Params")] public PlayerCombat.AttackSettings CombatSettings {get; private set;}
    }
}
