using System.Collections;
using System.Collections.Generic;
using Feature.Player;
using UnityEngine;
using Zenject;

public class JumpLocomotionFeature : ILocomotionFeature
{
    [Inject]
    private IMovementInput _movementInput;
    [Inject] PlayerConfig config;

    
    
    public void OnFixedUpdate(IPlayerLocomotion loc) {
        
    }
    
    public void OnUpdate(IPlayerLocomotion loc) { 
        if (_movementInput.IsJumping()) {loc.Velocity = Vector3.up * config.JumpHeight;}
    }
}
