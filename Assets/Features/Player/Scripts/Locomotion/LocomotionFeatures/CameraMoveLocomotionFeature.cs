using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Threading;
using Feature.Player;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;
using Zenject.SpaceFighter;

public class CameraMoveLocomotionFeature : ILocomotionFeature
{
    [Inject] Feature.Player.IMovementInput input;
    [Inject] Feature.Player.PlayerConfig config;
    public void OnFixedUpdate(IPlayerLocomotion loc) {
        
    }
    
    public void OnUpdate(IPlayerLocomotion loc) { 
        Debug.Log(input.GetMouseMovementX());
        float mouseInstantRotationX = input.GetMouseMovementX() * config.MouseSensitivity * Time.deltaTime;
        float mouseInstantRotationY = input.GetMouseMovementY() * config.MouseSensitivity * Time.deltaTime;
        loc.PlayerRotation += new Vector3(0f, 1f, 0f) * mouseInstantRotationX;
        loc.CameraRotation += new Vector3(Mathf.Clamp(-mouseInstantRotationY, -80f, 80f),0f,0f);
    }
}
