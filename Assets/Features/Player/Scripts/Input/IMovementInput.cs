using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feature.Player
{
    public interface IMovementInput
    {
        float GetVerticalMovement();
        float GetHorizontalMovement();
        Vector2 GetMovement();
        float GetMouseMovementX();
        float GetMouseMovementY();
        Vector2 GetMouseMovement();
        bool IsSliding();
        bool IsJumping();
        bool IsDashing();
    }
}
