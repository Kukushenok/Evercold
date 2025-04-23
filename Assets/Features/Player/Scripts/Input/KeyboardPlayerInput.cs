using System.Collections;
using System.Collections.Generic;
using PlasticGui.WorkspaceWindow;
using UnityEngine;

namespace Feature.Player
{
    public class KeyboardPlayerInput: IMovementInput
    {

        public bool IsSliding() {return Input.GetButtonDown("Slide");}
        public bool IsJumping() {return Input.GetAxis("Jump")>0;}
        public bool IsDashing() {return Input.GetButtonDown("Dash");}
        public float GetMouseMovementX() {return Input.GetAxis("Mouse X");}
        public float GetMouseMovementY() {return Input.GetAxis("Mouse Y");}
        public float GetHorizontalMovement() {return Input.GetAxis("Horizontal");}
        public float GetVerticalMovement() {return Input.GetAxis("Vertical");}
        public Vector2 GetMovement() {return new Vector2(GetHorizontalMovement(), GetVerticalMovement());}
        public Vector2 GetMouseMovement() {return new Vector2(GetMouseMovementX(), GetMouseMovementY());}
        
    }
}
