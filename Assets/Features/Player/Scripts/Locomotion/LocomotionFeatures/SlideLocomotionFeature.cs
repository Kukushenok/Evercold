using System.Collections;
using System.Collections.Generic;
using Feature.Player;
using UnityEngine;

namespace Feature.Player
{
    public class SlideLocomotionFeature : ILocomotionFeature
    {
        private IMovementInput _movementInput;
        PlayerConfig _config;
        IPlayerGroundChecker _checker;

        public SlideLocomotionFeature(PlayerConfig config, IMovementInput movementInput, IPlayerGroundChecker checker)
        {
            _config = config;
            _movementInput = movementInput;
            _checker = checker;
        }

        public void OnFixedUpdate(IPlayerLocomotion loc) { }

        public void OnUpdate(IPlayerLocomotion loc)
        {
            if (!loc.Status.IsSliding && _movementInput.IsSliding() && _checker.IsOnGround())
            { // && loc.Velocity.x>0
                Quaternion rotation = Quaternion.Euler(0, loc.PlayerRotation.y, 0);
                if (_config.PlayerSlideType == PlayerConfig.SlideType.Addition)
                {
                    loc.Velocity += rotation * (_config.SlideDirection.normalized * _config.SlideLength);
                }
                else if (_config.PlayerSlideType == PlayerConfig.SlideType.Multiply)
                {
                    loc.Velocity *= _config.SlideLength;
                }
                loc.CameraPosition = _config.CameraSlideLocalPosition;
                loc.Status.IsSliding = true;
            }
            else if (loc.Status.IsSliding && !_movementInput.IsSliding())
            {
                loc.CameraPosition = _config.CameraDefaultLocalPosition;
                loc.Status.IsSliding = false;
            }


            if (loc.Status.IsJumping)
            {
                loc.Status.IsSliding = false;
                loc.CameraPosition = _config.CameraDefaultLocalPosition;
            }

        }
    }
}
