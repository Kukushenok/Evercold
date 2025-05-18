using UnityEngine;

namespace Feature.Player
{
    public class GravityLocomotionFeature : ILocomotionFeature
    {
        private PlayerConfig _config;
        IPlayerGroundChecker _checker;

        public GravityLocomotionFeature(PlayerConfig config, IPlayerGroundChecker checker)
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

            loc.Velocity = loc.Velocity + Vector3.up * _config.Gravity * Time.fixedDeltaTime;
        }

        public void OnUpdate(IPlayerLocomotion loc)
        {

        }
    }
}