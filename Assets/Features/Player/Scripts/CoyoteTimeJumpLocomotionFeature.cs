using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
namespace Feature.Player
{
    [System.Serializable]
    public class CoyoteTimeJumpLocomotionFeature : PlayerLocomotionFeature
    {
        private float coyoteTime = 0.2f;
        private float coyoteTimeCounter;
        public override void LocomotionFixedUpdate(BasePlayerLocomotion loc)
        {
            
        }

        public override void LocomotionUpdate(BasePlayerLocomotion loc)
        {
            CharacterControllerLocomotion locomotion = (CharacterControllerLocomotion)loc;
            if (locomotion._isGrounded)
            {
                coyoteTimeCounter = coyoteTime;
            }
            else
            {
                coyoteTimeCounter -= Time.deltaTime;
            }

            if (coyoteTimeCounter > 0 && Input.GetButtonDown("Jump"))
            {
                loc.Jump();
            }
        }
    }
}