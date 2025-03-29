using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
namespace Feature.Player
{
    [System.Serializable]
    public class CoyoteTimeJumpLocomotionFeature : PlayerLocomotionFeature
    {
        private float _coyoteTime = 0.2f;
        private float _coyoteTimeCounter = 0f;
        private PlayerInput _playerInput;

        public CoyoteTimeJumpLocomotionFeature(float coyoteTime, PlayerInput playerInput) {
            _coyoteTime = coyoteTime; 
            _playerInput = playerInput;
        }
        public override void LocomotionFixedUpdate(BasePlayerLocomotion loc)
        {
            
        }

        public override void LocomotionUpdate(BasePlayerLocomotion loc)
        {
            CharacterControllerLocomotion locomotion = (CharacterControllerLocomotion)loc;
            if (locomotion._isGrounded)
            {
                _coyoteTimeCounter = _coyoteTime;
            }
            else
            {
                _coyoteTimeCounter -= Time.deltaTime;
            }

            if (_coyoteTimeCounter > 0 && Input.GetButtonDown("Jump"))
            {
                loc.Jump();
            }
        }
    }
}