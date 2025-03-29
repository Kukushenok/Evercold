using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    float Health {get;}
    public float MaxHealth { get;}
    public bool IsDead => MaxHealth <= 0;
    
}
