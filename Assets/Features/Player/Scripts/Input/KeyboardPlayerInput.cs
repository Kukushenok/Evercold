using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using Zenject;


namespace Feature.Player
{
    public class KeyboardPlayerInput : IMovementInput
    {
        private PlayerInputActions _inputActions;

        // input actions
        private InputAction _move;
        private InputAction _look;
        private InputAction _fire;
        private InputAction _jump;
        private InputAction _slide;
        private InputAction _dash;

        [Inject]
        public void Construct(PlayerInputActions actions)
        {
            _inputActions = actions;
            
            _move = _inputActions.Player.Move;
            _move.Enable();

            _look = _inputActions.Player.Look;
            _look.Enable();

            _fire = _inputActions.Player.Fire;
            _fire.Enable();

            _jump = _inputActions.Player.Jump;
            _jump.Enable();

            _slide = _inputActions.Player.Slide;
            _slide.Enable();

            _dash = _inputActions.Player.Dash;
            _dash.Enable();

        }

        public bool IsSliding() { return _slide.IsPressed(); }
        public bool IsJumping() { return _jump.WasPressedThisFrame(); }
        public bool IsDashing() { return _dash.WasPressedThisFrame(); }
        public float GetMouseMovementX() { return GetMouseMovement().x; }
        public float GetMouseMovementY() { return GetMouseMovement().y; }
        public float GetHorizontalMovement() { return GetMovement().x; }
        public float GetVerticalMovement() { return GetMovement().y; }
        public Vector2 GetMovement() { return _move.ReadValue<Vector2>(); }
        public Vector2 GetMouseMovement() { return _look.ReadValue<Vector2>(); }
    }
}
