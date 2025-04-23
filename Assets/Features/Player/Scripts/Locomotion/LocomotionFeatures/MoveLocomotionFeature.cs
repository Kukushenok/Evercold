using System.Collections;
using System.Collections.Generic;
using Feature.Player;
using UnityEngine;
using Zenject;

public class MoveLocomotionFeature : ILocomotionFeature
{
    [Inject] Feature.Player.IMovementInput _movementInput;
    [Inject] PlayerConfig config;
    public void OnFixedUpdate(IPlayerLocomotion loc) {
        Vector3 move = Vector3.right * _movementInput.GetHorizontalMovement()
            + Vector3.forward * _movementInput.GetVerticalMovement();
        Quaternion rotation = Quaternion.Euler(0, loc.PlayerRotation.y, 0);
        loc.Velocity += rotation * move;
        
        

    }
    
    public void OnUpdate(IPlayerLocomotion loc) { 
        
    }
}
