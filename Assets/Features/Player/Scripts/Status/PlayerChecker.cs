using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChecker : MonoBehaviour, IPlayerGroundChecker, IPlayerWallChecker
{
    bool collisionWithWalls = false;
    bool onGround = false;
    public bool IsOnGround() {return false;}
    public bool IsCollidingWithWalls() {return false;}
}
