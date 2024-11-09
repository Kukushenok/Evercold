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
        private float _verticalRotation = 0f;
        private float _horizontalRotation = 0f;


        public override void LocomotionFixedUpdate(BasePlayerLocomotion loc) { }

        public override void LocomotionUpdate(BasePlayerLocomotion loc)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            // Updating horizontal and vertical rotation
            _horizontalRotation += mouseX;
            _verticalRotation -= mouseY;
            _verticalRotation = Mathf.Clamp(_verticalRotation, -90f, 90f);
            // Updating CameraRotation in base class
            loc.CameraRotation = new Vector3(_verticalRotation, _horizontalRotation);
        }
    }
}