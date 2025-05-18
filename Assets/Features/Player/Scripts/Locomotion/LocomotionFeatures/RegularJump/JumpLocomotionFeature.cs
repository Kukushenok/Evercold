using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Feature.Player;
using UnityEngine;
using Zenject;

namespace Feature.Player
{
    public class JumpLocomotionFeature : ILocomotionFeature
    {
        private IMovementInput _movementInput;
        PlayerConfig _config;
        private int _jumpsLeft = 1;
        private IPlayerGroundChecker _groundChecker;
        private IPlayerWallChecker _wallChecker;

        public JumpLocomotionFeature(PlayerConfig config, IMovementInput movementInput, IPlayerGroundChecker groundChecker, IPlayerWallChecker wallChecker)
        {
            _config = config;
            _movementInput = movementInput;
            _jumpsLeft = _config.MaxJumps;
            _groundChecker = groundChecker;
            _wallChecker = wallChecker;
        }

        public void OnFixedUpdate(IPlayerLocomotion loc)
        {
            //if (_wallChecker.IsCollidingWithWalls()){
            //    _jumpsLeft = 1;
            //}
            if (loc.Velocity.y <= _config.LowestJumpingVelocity && _groundChecker.IsOnGround())
            { // don't use HighestFallingVelocity here we need to include grounded state
                _jumpsLeft = _config.MaxJumps;
                loc.Status.IsJumping = false;
            }

        }

        public void OnUpdate(IPlayerLocomotion loc)
        {
            if (_movementInput.IsJumping() && _jumpsLeft > 0)
            {
                loc.Velocity = new Vector3(loc.Velocity.x, _config.JumpHeight * -_config.Gravity, loc.Velocity.z);
                _jumpsLeft -= 1;
                loc.Status.IsJumping = true;
            }
            if (_movementInput.IsJumping() && _wallChecker.IsCollidingWithWalls())
            {
                loc.Velocity = new Vector3(loc.Velocity.x, _config.JumpHeight * -_config.Gravity, loc.Velocity.z);
                loc.Status.IsJumping = true;
            }
        }
    }
}
