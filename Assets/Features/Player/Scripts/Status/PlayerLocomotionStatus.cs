using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feature.Player
{
    public interface IPlayerLocomotionStatus
    {
        bool IsSliding();
        bool IsJumping();
        bool IsDashing();
    }

    public class PlayerLocomotionStatus
    {

    }
}
