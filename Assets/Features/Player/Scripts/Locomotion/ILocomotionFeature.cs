using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feature.Player
{
    public interface ILocomotionFeature
    {
        public void OnFixedUpdate(ILocomotionData loc) { }
        public void OnUpdate(ILocomotionData loc) { }
    }
}
