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
        public bool IsOnGround() { return false; }
        public bool IsCollidingWithWalls() { return false; }
    }
}

