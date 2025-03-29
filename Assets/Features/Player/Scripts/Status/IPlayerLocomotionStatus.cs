using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerLocomotionStatus
{
    bool IsSliding();
    bool IsJumping();
    bool IsDashing();
}
