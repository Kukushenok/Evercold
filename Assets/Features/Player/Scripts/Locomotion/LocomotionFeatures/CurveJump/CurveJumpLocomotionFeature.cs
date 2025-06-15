using System.Collections;
using System.Collections.Generic;
using Feature.Player;
using UnityEngine;

namespace Feature.Player
{
    public class CurveJumpLocomotionFeature : ILocomotionFeature
    {
        private IMovementInput _movementInput;
        PlayerConfig _config;

        private IPlayerGroundChecker _groundChecker;
        private IPlayerWallChecker _wallChecker;
        private IPlayerCeilingChecker _ceilingChecker;
        private int _jumpsLeft = 2;
        private float _jumpTimer = 0f;
        private float _maxTime = 0f;

        public CurveJumpLocomotionFeature(PlayerConfig config, IMovementInput movementInput, IPlayerGroundChecker groundChecker, IPlayerWallChecker wallChecker, IPlayerCeilingChecker ceilingChecker)
        {
            _config = config;
            _movementInput = movementInput;
            _jumpsLeft = _config.MaxJumps;
            _groundChecker = groundChecker;
            _wallChecker = wallChecker;
            _ceilingChecker = ceilingChecker;

            _maxTime = _config.JumpCurve.keys[_config.JumpCurve.length - 1].time - 0.05f; // do not include last values to avoid errors
        }

        public void OnFixedUpdate(IPlayerLocomotion loc)
        {
            if (loc.Status.IsDashing) { loc.Status.IsJumping = false; }
            if (loc.Status.IsSlamming) { loc.Status.IsJumping = false; }
            if (_ceilingChecker.IsCollidingWithCeiling())
            {
                loc.Status.IsJumping = false;
            loc.Velocity = new Vector3(loc.Velocity.x, -loc.Velocity.y * _config.CeilingBounciness, loc.Velocity.z);  }
            
            if (loc.Status.IsJumping)
            {
                _jumpTimer += Time.fixedDeltaTime / _config.JumpDuration;
                if (_maxTime <= _jumpTimer) _jumpTimer = _maxTime;

            }

            if (_wallChecker.IsCollidingWithWalls())
            {
                _jumpsLeft = 1;
            }
            // changing velocity using curve
            if (loc.Status.IsJumping)
            {
                loc.Velocity = new Vector3(loc.Velocity.x, GetVelocity(_jumpTimer) * _config.JumpHeight, loc.Velocity.z);
            }
            if (loc.Velocity.y <= _config.LowestJumpingVelocity && _groundChecker.IsOnGround())
            { // don't use HighestFallingVelocity here we need to include grounded state
                _jumpsLeft = _config.MaxJumps;
                loc.Status.IsJumping = false;
            }

            // for testing purposes
            #if UNITY_EDITOR
            _maxTime = _config.JumpCurve.keys[_config.JumpCurve.length - 1].time - 0.05f;
            #endif

        }

        public void OnUpdate(IPlayerLocomotion loc)
        {
            if (_movementInput.IsJumping() && _jumpsLeft > 0)
            {
                //loc.Velocity = new Vector3(loc.Velocity.x, _config.JumpHeight * -_config.Gravity, loc.Velocity.z);
                loc.Status.IsJumping = true;
                _jumpsLeft -= 1;
                _jumpTimer = 0f;
            }
            if (_movementInput.IsJumping() && _wallChecker.IsCollidingWithWalls())
            {
                //loc.Velocity = new Vector3(loc.Velocity.x, _config.JumpHeight * -_config.Gravity, loc.Velocity.z);

                loc.Status.IsJumping = true;
                _jumpTimer = 0f;
                Quaternion rotation = Quaternion.Euler(0, loc.PlayerRotation.y, 0);
                if (_wallChecker.IsCollidingWithLeftWall()) { loc.Velocity = loc.Velocity + rotation * Vector3.right * _config.WallJumpSideForce; }
                else if (_wallChecker.IsCollidingWithRightWall()) { loc.Velocity = loc.Velocity + rotation * Vector3.left * _config.WallJumpSideForce; }
            }
        }

        float GetVelocity(float time)
        {
            float dt = Time.fixedDeltaTime;
            float t1 = time - dt;
            float t2 = time + dt;
            float pos1 = _config.JumpCurve.Evaluate(t1);
            float pos2 = _config.JumpCurve.Evaluate(t2);

            return (pos2 - pos1) / (t2 - t1);
        }

        float GetAcceleration(float time)
        {
            float dt = Time.fixedDeltaTime;
            float v1 = GetVelocity(time - dt);
            float v2 = GetVelocity(time + dt);
            return (v2 - v1) / (2 * dt);
        }
    }
}
