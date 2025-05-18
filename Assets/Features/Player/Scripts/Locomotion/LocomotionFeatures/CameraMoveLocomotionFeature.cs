using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Threading;
using Feature.Player;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;
using Zenject.SpaceFighter;

namespace Feature.Player
{
    
    
    public class CameraMoveLocomotionFeature : ILocomotionFeature
    {
        private float _pitch = 0f;
        [Inject] Feature.Player.IMovementInput input;
        [Inject] Feature.Player.PlayerConfig config;
        public void OnFixedUpdate(IPlayerLocomotion loc)
        {

        }

        public void OnUpdate(IPlayerLocomotion loc)
        {
            float mouseInstantRotationX = input.GetMouseMovementX() * config.MouseSensitivity * Time.deltaTime;
            float mouseInstantRotationY = input.GetMouseMovementY() * config.MouseSensitivity * Time.deltaTime;
            loc.PlayerRotation += new Vector3(0f, 1f, 0f) * mouseInstantRotationX;
            UnityEngine.Debug.Log(loc.CameraRotation.x - mouseInstantRotationY);
            _pitch = Mathf.Clamp(_pitch - mouseInstantRotationY, -80f, 80f);
            //loc.CameraRotation = new Vector3(Mathf.Clamp(loc.CameraRotation.x - mouseInstantRotationY, -80f, 80f), 0f, 0f);
            loc.CameraRotation = new Vector3(_pitch, 0f, 0f);

        }
    }
}
