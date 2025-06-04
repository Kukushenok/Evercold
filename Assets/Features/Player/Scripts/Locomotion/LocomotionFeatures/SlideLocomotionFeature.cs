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
        CharacterController _contorller;

        public SlideLocomotionFeature(PlayerConfig config, IMovementInput movementInput, IPlayerGroundChecker checker, CharacterController controller)
        {
            _config = config;
            _movementInput = movementInput;
            _checker = checker;
            _contorller = controller;
        }

        public void OnFixedUpdate(IPlayerLocomotion loc) { }

        public void OnUpdate(IPlayerLocomotion loc)
        {
            if (!loc.Status.IsSliding && _movementInput.IsSliding() && _checker.IsOnGround())
            { 
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
                _contorller.height = _config.SlideColliderHeight;
                _contorller.center = new Vector3(0f, -0.5f * (_config.DefaultColliderHeight-_config.SlideColliderHeight), 0f);
                loc.Status.IsSliding = true;
            }
            else if (loc.Status.IsSliding && !_movementInput.IsSliding())
            {
                loc.CameraPosition = _config.CameraDefaultLocalPosition;
                _contorller.height = _config.DefaultColliderHeight;
                _contorller.center = new Vector3(0f, 0f, 0f);
                loc.Status.IsSliding = false;
            }


            if (loc.Status.IsJumping)
            {
                loc.Status.IsSliding = false;
                _contorller.height = _config.DefaultColliderHeight;
                _contorller.center = new Vector3(0f, 0f, 0f);       
                loc.CameraPosition = _config.CameraDefaultLocalPosition;
            }

        }
    }
}
