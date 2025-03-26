using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFacade : MonoBehaviour, IEnableAble
{

    public bool Enabled { get; private set; }

    [SerializeField] private PlayerHealth playerHealth;
    public void TakeDamage(AttackData attackData) => playerHealth.TakeDamage(attackData);
    public bool IsDead() => playerHealth.IsDead();


}
