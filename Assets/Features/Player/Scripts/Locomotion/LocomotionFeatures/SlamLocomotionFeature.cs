using UnityEngine;

namespace Feature.Player
{
    public class SlamLocomotionFeature : ILocomotionFeature
    {
        private IMovementInput _movementInput;
        PlayerConfig _config;
        IPlayerGroundChecker _checker;

        public SlamLocomotionFeature(PlayerConfig config, IMovementInput movementInput, IPlayerGroundChecker checker)
        {
            _config = config;
            _movementInput = movementInput;
            _checker = checker;
        }

        public void OnFixedUpdate(IPlayerLocomotion loc)
        {
            if (_checker.IsOnGround())
            {
                loc.Status.IsSlamming = false;
            }
            
            if (loc.Status.IsSlamming)
            {
                loc.Velocity += Vector3.down * _config.SlamAcceleration;
            }

        }

        public void OnUpdate(IPlayerLocomotion loc)
        {
            if (_movementInput.IsSlamming() && !_checker.IsOnGround() && !loc.Status.IsSlamming)
            {
                loc.Velocity = Vector3.down * _config.SlamStartSpeed;
                loc.Status.IsSlamming = true;
            }
        }
    }
}