using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Feature.Player
{
    public interface IPlayerLocomotion
    {
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
        public CharacterController Controller {get; private set;}

        public PlayerLocomotion(Transform playerTransform, Transform cameraTransform, CharacterController controller)
        {
            _cameraTransform = cameraTransform;
            _playerTransform = playerTransform;
            Controller = controller;
            Velocity = Vector3.zero;
            
        }
    }
}

