using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackData
{
    public float Damage {private set; get;}
    GameObject? AssociatedOwner;
    Vector3? AssociatedKnockback;
}
