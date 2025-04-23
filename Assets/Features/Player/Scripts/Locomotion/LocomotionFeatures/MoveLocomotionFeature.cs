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

        //Vector3 move = loc.PlayerTransform.right * _movementInput.GetHorizontalMovement()
        //    + loc.PlayerTransform.forward * _movementInput.GetVerticalMovement();
        //loc.Controller.Move(move.normalized * config.MovementSpeed * Time.deltaTime);

    }
    
    public void OnUpdate(IPlayerLocomotion loc) { 
        
    }
}
