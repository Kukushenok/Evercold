using System.Runtime.CompilerServices;
using Feature.Player;
using UnityEngine;

namespace Feature.Player
{
    public class DashLocomotionFeature : ILocomotionFeature
    {
        private PlayerConfig _config;
        private IMovementInput _movementInput;
        private IPlayerGroundChecker _checker;
        private float _dashTimer = 0f;
        private Quaternion _currentDashRotation;
        private int _currentDashAmount;

        public DashLocomotionFeature(PlayerConfig config, IMovementInput movementInput, IPlayerGroundChecker checker)
        {
            _config = config;
            _movementInput = movementInput;
            _checker = checker;
            _currentDashAmount = _config.MaxDashAmount;
        }
        public void OnFixedUpdate(IPlayerLocomotion loc)
        {
            if (loc.Status.IsDashing)
            {
                loc.Velocity = _currentDashRotation * (_config.SlideDirection.normalized * _config.DashSpeed);
            }
            if (loc.Status.IsDashing) {
                _dashTimer -= Time.fixedDeltaTime;
                if (_dashTimer <= 0) { loc.Status.IsDashing = false; }
            }
            
        }

        public void OnUpdate(IPlayerLocomotion loc)
        {
            if (!loc.Status.IsDashing && _movementInput.IsDashing()) // dash start
            {
                Quaternion rotation = Quaternion.Euler(loc.CameraRotation.x, loc.PlayerRotation.y, 0);
                _currentDashRotation = rotation;
                loc.Velocity = rotation * (_config.SlideDirection.normalized * _config.DashSpeed); // todo remove slide direction
                loc.Status.IsDashing = true;
                _dashTimer = _config.DashDuration;
            }
            if(_checker.IsOnGround()){ _currentDashAmount = _config.MaxDashAmount; }
        }
    }
}