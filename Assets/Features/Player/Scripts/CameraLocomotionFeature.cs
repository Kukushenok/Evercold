using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
namespace Feature.Player
{
    
    //Camera rotation
    [System.Serializable]
    public class CameraLocomotionFeature : PlayerLocomotionFeature
    {
        public float mouseSensitivity = 2f;
        private float verticalRotation = 0f;
        private float horizontalRotation = 0f;


        public override void LocomotionFixedUpdate(BasePlayerLocomotion loc) { }

        public override void LocomotionUpdate(BasePlayerLocomotion loc)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            // Updating horizontal and vertical rotation
            horizontalRotation += mouseX;
            verticalRotation -= mouseY;
            verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
            // Updating CameraRotation in base class
            loc.CameraRotation = new Vector3(verticalRotation, horizontalRotation);
        }
    }
}