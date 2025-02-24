using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using Zenject;
namespace Feature.Player
{
   
    [System.Serializable]
    public class MoveLocomotionFeature : PlayerLocomotionFeature
    {
        private PlayerInput _playerInput;
        [SerializeField] protected float _speed = 5f;

        public MoveLocomotionFeature(float speed, PlayerInput playerInput) {
            _speed = speed;
            _playerInput = playerInput;
        }
        public override void LocomotionFixedUpdate(BasePlayerLocomotion loc) { }

        public override void LocomotionUpdate(BasePlayerLocomotion loc)
        {
            Vector3 move = new Vector3(_playerInput.MovementX, 0, _playerInput.MovementY);
            loc.DesiredDeltaPos = loc.transform.TransformDirection(move) * _speed;
        }
    }
}