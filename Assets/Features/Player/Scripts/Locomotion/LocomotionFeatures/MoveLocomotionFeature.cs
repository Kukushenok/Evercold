using System.Collections;
using System.Collections.Generic;
using Feature.Player;
using UnityEngine;
using Zenject;

namespace Feature.Player
{
    public class MoveLocomotionFeature : ILocomotionFeature
    {
        Feature.Player.IMovementInput _movementInput;
        PlayerConfig _config;

        public MoveLocomotionFeature(Feature.Player.IMovementInput movementInput, PlayerConfig config) {
            _movementInput = movementInput;
            _config = config;
        }
        public void OnFixedUpdate(IPlayerLocomotion loc)
        {
            if (loc.Status.IsSliding) { return; }

            Vector3 move = Vector3.right * _movementInput.GetHorizontalMovement()
                + Vector3.forward * _movementInput.GetVerticalMovement();

            Quaternion rotation = Quaternion.Euler(0, loc.PlayerRotation.y, 0);

            if (loc.Status.IsJumping) { move *= _config.AirControlMultiplyer; }
            loc.Velocity += rotation * move * _config.MovementSpeed * Time.fixedDeltaTime;



        }

        public void OnUpdate(IPlayerLocomotion loc)
        {

        }
    }
}
