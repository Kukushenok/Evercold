using UnityEngine;
using System.Collections.Generic;
using System.Text;
namespace Feature.Player
{
   [RequireComponent(typeof(CharacterController))]
    public class CharacterControllerLocomotion : BasePlayerLocomotion
    {
        private CharacterController _controller;
        private Vector3 _velocity;
        protected internal bool _isGrounded;
        [SerializeField] private float jumpHeight = 1.5f;
        [SerializeField] private float gravity = -9.81f;

        private Vector3 _cameraRotation;
        [SerializeField] private Camera playerCamera;
        public override Vector3 DesiredDeltaPos { get; set; }

        public override Vector3 CameraRotation
        {
            get => _cameraRotation;
            set => _cameraRotation = value;
        }
        private void Start()
        {
            _controller = GetComponent<CharacterController>();

            if (playerCamera == null)
            {
                Debug.LogError("playerCamera is not setted in inspector");
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

