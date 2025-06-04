using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Feature.Player
{
    public interface IPlayerLocomotion
    {
        public IPlayerLocomotionStatus Status {get;}
        public Vector3 CameraPosition {get; set;}
        public Vector3 CameraRotation { get; set; } // eulerAngles
        public Vector3 PlayerPosition {get; set;}
        public Vector3 PlayerRotation {get; set;} // eulerAngles
        public Vector3 Velocity { get; set; }
        public void FixedUpdate() {}
        public void Update() {}
    }

    public class PlayerLocomotion : IPlayerLocomotion
    {
        public IPlayerLocomotionStatus Status { get; private set; }
        public Vector3 CameraPosition
        {
            get => _cameraTransform.localPosition;
            set => _cameraTransform.localPosition = value;
        }
        public Vector3 CameraRotation
        {
            get => _cameraTransform.localRotation.eulerAngles;
            set => _cameraTransform.localRotation = Quaternion.Euler(value);
        }
        public Vector3 PlayerPosition
        {
            get => _playerTransform.position;
            set => _playerTransform.position = value;
        }
        public Vector3 PlayerRotation
        {
            get => _playerTransform.localRotation.eulerAngles;
            set => _playerTransform.localRotation = Quaternion.Euler(value);
        }
        private Transform _playerTransform;
        private Transform _cameraTransform;
        public Vector3 Velocity { get; set; }
        private CharacterController _controller;
        private PlayerConfig _config;

        public PlayerLocomotion(
            Transform playerTransform,
            Transform cameraTransform,
            CharacterController controller,
            PlayerConfig config,
            IPlayerLocomotionStatus status)
        {
            _cameraTransform = cameraTransform;
            _playerTransform = playerTransform;
            _controller = controller;
            Velocity = Vector3.zero;
            _config = config;
            Status = status;
        }

        public void FixedUpdate()
        {
            _controller.Move(Velocity * Time.fixedDeltaTime);
            float drag = _config.MovementDrag;
            if (Status.IsJumping) drag = _config.AirMovementDrag;
            Velocity = new Vector3(Mathf.Lerp(Velocity.x, 0, drag * Time.fixedDeltaTime), Velocity.y, Mathf.Lerp(Velocity.z, 0, drag * Time.fixedDeltaTime));
        }   
    }
}

