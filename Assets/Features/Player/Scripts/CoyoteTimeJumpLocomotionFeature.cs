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
        private float _coyoteTimeCounter;
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