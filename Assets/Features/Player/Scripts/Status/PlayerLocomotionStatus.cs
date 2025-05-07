using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Feature.Player
{
    public interface IPlayerLocomotionStatus
    {
        
        public bool IsSliding {get; set;}
        public bool IsJumping {get; set;}
        // bool IsJumping();
        // bool IsDashing();
    }

    public class PlayerLocomotionStatus: IPlayerLocomotionStatus
    {
        public bool IsSliding {get; set;} = false;
        public bool IsJumping {get; set;} = false;
    }
}
