using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feature.Player
{
    public interface ILocomotionFeature
    {
        public void OnFixedUpdate(IPlayerLocomotion loc) { }
        public void OnUpdate(IPlayerLocomotion loc) { }
    }
}
