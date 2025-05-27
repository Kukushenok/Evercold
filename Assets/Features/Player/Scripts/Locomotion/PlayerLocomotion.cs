using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Feature.Player
{
    public interface IPlayerLocomotion
    {
        public Vector3 CameraForwardVec { get; }
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
        public Vector3 CameraForwardVec => _cameraTransform.forward;
        public Vector3 CameraPosition {
            get => _cameraTransform.position;
            set => _cameraTransform.position = value;
        }
        public Vector3 CameraRotation {
            get => _cameraTransform.localRotation.eulerAngles;
            set => _cameraTransform.localRotation = Quaternion.Euler(value);
            }
        public Vector3 PlayerPosition {
            get => _playerTransform.position;
            set => _playerTransform.position = value;
        }
        public Vector3 PlayerRotation {
            get => _playerTransform.localRotation.eulerAngles;
            set => _playerTransform.localRotation = Quaternion.Euler(value);
        }
        private Transform _playerTransform;
        private Transform _cameraTransform;
        public Vector3 Velocity { get; set; }
        private CharacterController _controller;
        private PlayerConfig _config;

        public PlayerLocomotion(Transform playerTransform, Transform cameraTransform, CharacterController controller, PlayerConfig config)
        {
            _cameraTransform = cameraTransform;
            _playerTransform = playerTransform;
            _controller = controller;
            Velocity = Vector3.zero;
            _config = config; 
            
        }

        public void FixedUpdate() {
            _controller.Move(Velocity * Time.fixedDeltaTime);
            Velocity = new Vector3(Mathf.Lerp(Velocity.x, 0, _config.MovementDrag * Time.fixedDeltaTime), Velocity.y, Mathf.Lerp(Velocity.z, 0, _config.MovementDrag * Time.fixedDeltaTime));
        }
    }
}

