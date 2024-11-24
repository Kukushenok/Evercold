using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public float MaxHealth { private set; get; }
    [SerializeField] public float CurrentHealth { private set; get; }

    public static event Action OnTakeDamage;
    public static event Action OnHeal;
    public static event Action OnDie;

    public void GetDamage(float dmg)
    {
        if (dmg < 0) Debug.LogError("Can't take damage, damage must be > 0");

        OnTakeDamage?.Invoke();
        CurrentHealth -= dmg;
        if (CurrentHealth <= 0) { CurrentHealth = 0f; OnDie?.Invoke(); }
    }

    public void Heal(float value)
    {
        if (value < 0) Debug.LogError("Can't heal, value must be > 0");

        OnHeal?.Invoke();
        CurrentHealth += value;
        if (CurrentHealth >= MaxHealth) CurrentHealth = MaxHealth;
    }
}
