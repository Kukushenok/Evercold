using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Feature.Player
{
    public interface ILocomotionData
    {
        public Transform CameraTransform { get; set; }
        public UnityEngine.Vector3 Velocity { get; set; }
    }

    public class LocomotionData : ILocomotionData
    {
        public Transform CameraTransform { get; set; }
        public UnityEngine.Vector3 Velocity { get; set; }

        public LocomotionData(Transform cameraTransform)
    {
        Velocity = Vector3.zero;
        CameraTransform = cameraTransform;
    }
    }
}

