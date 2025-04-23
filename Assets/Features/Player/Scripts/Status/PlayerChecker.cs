using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Feature.Player
{
    public interface IPlayerWallChecker
    {
        bool IsCollidingWithWalls();
    }


    public interface IPlayerGroundChecker
    {
        bool IsOnGround();
    }

    public class PlayerChecker : MonoBehaviour, IPlayerGroundChecker, IPlayerWallChecker
    {
        private PlayerConfig _config;
        [Inject]
        //public PlayerChecker(PlayerConfig config) {
        //    Debug.Log("asdfasdfasdf"); Debug.Log(config);
        //    _config = config;
        //}
        public void Construct(PlayerConfig config)
        {
            _config = config;
        }
        public bool IsOnGround() { 
            return Physics.SphereCast(
                transform.position,
                _config.GroundCheckFigureSize,
                Vector3.down,
                out _, 
                _config.WallCheckFigureShiftDistance,
                _config.GroundLayerMask);
         }
        
        public bool IsCollidingWithWalls() { 
            if (Physics.SphereCast(
                transform.position,
                _config.WallCheckFigureSize,
                _config.LeftWallCheckDirection,
                out _,
                _config.WallCheckFigureShiftDistance,
                _config.WallLayerMask)) return true;
            if (Physics.SphereCast(
                transform.position,
                _config.WallCheckFigureSize,
                _config.RightWallCheckDirection,
                out _,
                _config.WallCheckFigureShiftDistance,
                _config.WallLayerMask)) return true;
            return false;
        }

        void OnDrawGizmos()
        {
            if (_config.ShowGroundCheckGizmos) Gizmos.DrawSphere(transform.position+Vector3.down*_config.GroundCheckFigureShiftDistance, _config.GroundCheckFigureSize);
            if (_config.ShowWallCheckGizmos) {
                Gizmos.DrawSphere(transform.position+_config.LeftWallCheckDirection*_config.WallCheckFigureShiftDistance, _config.WallCheckFigureSize);
                Gizmos.DrawSphere(transform.position+_config.RightWallCheckDirection*_config.WallCheckFigureShiftDistance, _config.WallCheckFigureSize);
                }
        }

    }
}

