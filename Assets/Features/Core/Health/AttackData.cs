using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackData
{
    public float Damage {private set; get;}
    public GameObject? AssociatedOwner { private set; get; }
    public Vector3? AssociatedKnockback { private set; get; }



    // public AttackData(float damage) => Damage = damage;
    public AttackData(float damage, GameObject? associatedOwner, Vector3? associatedKnockback){
        Damage = damage;
        AssociatedOwner = associatedOwner;
        AssociatedKnockback = associatedKnockback;
    }

    public AttackData(float damage, GameObject? associatedOwner){
        Damage = damage;
        AssociatedOwner = associatedOwner;
        AssociatedKnockback = null;
    }
    public AttackData(float damage, Vector3? associatedKnockback){
        Damage = damage;
        AssociatedOwner = null;
        AssociatedKnockback = associatedKnockback;
    }
    public AttackData(float damage){
        Damage = damage;
        AssociatedOwner = null;
        AssociatedKnockback = null;
    }
}
