using UnityEngine;
using System.Collections.Generic;
using System.Text;
namespace Feature.Player
{
   
    public class CharacterControllerLocomotion : BasePlayerLocomotion
    {
        private CharacterController _controller;
        private Vector3 _velocity;
        protected internal bool _isGrounded;
        public float jumpHeight = 1.5f;
        public float gravity = -9.81f;

        private Vector3 cameraRotation;
        public override Vector3 DesiredDeltaPos { get; set; }


        public override Vector3 CameraRotation
        {
            get => cameraRotation;
            set => cameraRotation = value;
        }

        private void Start()
        {
            _controller = GetComponent<CharacterController>();




            if (playerCamera == null)
            {
                playerCamera = Camera.main;
                playerCamera.transform.SetParent(transform);
                playerCamera.transform.localPosition = new Vector3(0, 1.6f, 0); // Позиция камеры на уровне головы
                playerCamera.transform.localRotation = Quaternion.identity;
            }
        }

        public override void Jump()
        {
            if (_isGrounded)
            {
                _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }

        protected override void OnLocomotionUpdate()
        {
            _isGrounded = _controller.isGrounded;

            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }

            Vector3 move = DesiredDeltaPos;
            _controller.Move(move * Time.deltaTime);

            _velocity.y += gravity * Time.deltaTime;
            _controller.Move(_velocity * Time.deltaTime);


            transform.rotation = Quaternion.Euler(0f, CameraRotation.y, 0f);




            playerCamera.transform.localRotation = Quaternion.Euler(CameraRotation.x, 0f, 0f);



        }

        protected override void OnLocomotionFixedUpdate()
        {

        }
    }

}

