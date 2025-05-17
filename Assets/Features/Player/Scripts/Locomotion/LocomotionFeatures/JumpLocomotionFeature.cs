using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Feature.Player;
using UnityEngine;
using Zenject;

public class JumpLocomotionFeature : ILocomotionFeature
{
    private IMovementInput _movementInput;
    PlayerConfig _config;
    private int _jumpsLeft;
    private IPlayerGroundChecker _groundChecker;

    public JumpLocomotionFeature(PlayerConfig config, IMovementInput movementInput, IPlayerGroundChecker checker) {
        _config = config;
        _movementInput = movementInput;
        _jumpsLeft = _config.MaxJumps;
        _groundChecker = checker;
    }
    
    public void OnFixedUpdate(IPlayerLocomotion loc) {
        if (loc.Velocity.y <= _config.LowestJumpingVelocity) { // don't use HighestFallingVelocity here we need to include grounded state
            _jumpsLeft = _config.MaxJumps;
        }
    }
    
    public void OnUpdate(IPlayerLocomotion loc) { 
        if (_movementInput.IsJumping()) {
            loc.Velocity = new Vector3(loc.Velocity.x, _config.JumpHeight * -_config.Gravity, loc.Velocity.z);
            _jumpsLeft -= 1;
        }
    }
}
