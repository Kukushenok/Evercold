using System.Collections;
using System.Collections.Generic;
using PlasticGui.WorkspaceWindow;
using UnityEngine;

namespace Feature.Player
{
    public class PlayerInput
    {
        public bool IsJump {get{return Input.GetButtonDown("Jump");}}
        public float MouseMovementX{get => Input.GetAxis("Mouse X");}
        public float MouseMovementY{get => Input.GetAxis("Mouse Y");}
        public float MovementX {get{return Input.GetAxis("Horizontal");}}
        public float MovementY{get{return Input.GetAxis("Vertical");}}
        public Vector2 Movement {get{
            return new Vector2(MovementX, MovementY);}}
        
    }
}
