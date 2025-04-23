using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        bool collisionWithWalls = false;
        bool onGround = false;
        private PlayerConfig _config;
        public PlayerChecker(PlayerConfig config) {
            _config = config;
        }
        public bool IsOnGround() { 
            return Physics.SphereCast(
                transform.position,
                _config.GroundCheckFigureSize,
                Vector3.down,
                out _, 
                _config.CheckFigureShiftDistance,
                _config.GroundLayerMask);
         }
        
        public bool IsCollidingWithWalls() { 
            if (Physics.SphereCast(
                transform.position,
                _config.WallCheckFigureSize,
                _config.LeftWallCheckDirection,
                out _,
                _config.CheckFigureShiftDistance,
                _config.WallLayerMask)) return true;
            if (Physics.SphereCast(
                transform.position,
                _config.WallCheckFigureSize,
                _config.RightWallCheckDirection,
                out _,
                _config.CheckFigureShiftDistance,
                _config.WallLayerMask)) return true;
            return false;
        }

    }
}

