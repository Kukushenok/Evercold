using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
namespace Feature.Player
{
   
    [System.Serializable]
    public class MoveLocomotionFeature : PlayerLocomotionFeature
    {
        public float speed = 5f;

        public override void LocomotionFixedUpdate(BasePlayerLocomotion loc) { }

        public override void LocomotionUpdate(BasePlayerLocomotion loc)
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");
            Vector3 move = new Vector3(moveX, 0, moveZ);
            loc.DesiredDeltaPos = loc.transform.TransformDirection(move) * speed;
        }
    }
}