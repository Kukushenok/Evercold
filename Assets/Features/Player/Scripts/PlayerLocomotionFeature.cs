using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
namespace Feature.Player
{
    public interface IPlayerLocomotionFeature
    {
        public void LocomotionFixedUpdate(BasePlayerLocomotion loc);
        public void LocomotionUpdate(BasePlayerLocomotion loc);
    }
    public abstract class PlayerLocomotionFeature : IPlayerLocomotionFeature
    {
        public abstract void LocomotionFixedUpdate(BasePlayerLocomotion loc);
        public abstract void LocomotionUpdate(BasePlayerLocomotion loc);
    }
}