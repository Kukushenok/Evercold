using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Feature.Player
{
    public class CurveGravityLocomotionFeature : ILocomotionFeature
    {
        private PlayerConfig _config;
        IPlayerGroundChecker _checker;

        public CurveGravityLocomotionFeature(PlayerConfig config, IPlayerGroundChecker checker)
        {
            _config = config;
            _checker = checker;
        }

        public void OnFixedUpdate(IPlayerLocomotion loc)
        {
            if (_checker.IsOnGround() && loc.Velocity.y <= _config.LowestJumpingVelocity)
            { // don't use HighestFallingVelocity here we need to include grounded state
                loc.Velocity = new Vector3(loc.Velocity.x, -0.5f, loc.Velocity.z);
            } //TODO: remove majic number
            if (!loc.Status.IsJumping)
            {
                loc.Velocity = loc.Velocity + Vector3.up * _config.Gravity * Time.fixedDeltaTime;
            }
        }

        public void OnUpdate(IPlayerLocomotion loc)
        {

        }
    }
}
