using System.Collections;
using System.Collections.Generic;
using Feature.Player;
using UnityEngine;
using Zenject;

public class GravityLocomotionFeature : ILocomotionFeature
{
    [Inject] PlayerConfig config;

    public void OnFixedUpdate(IPlayerLocomotion loc) {
        //if (loc.Controller.isGrounded){loc.Velocity = Vector3.down*0.5f;}
        loc.Velocity = loc.Velocity + Vector3.up * config.Gravity * Time.fixedDeltaTime;
        //loc.Controller.Move(loc.Velocity * Time.fixedDeltaTime);
    }
    
    public void OnUpdate(IPlayerLocomotion loc) { 
        
    }
}
