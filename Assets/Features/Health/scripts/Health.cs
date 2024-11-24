using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] public float MaxHealth { private set; get; }
    [SerializeField] public float CurrentHealth { private set; get; }

    public UnityEvent OnTakeDamage;
    public UnityEvent OnHeal;
    public UnityEvent OnDie;

    public void GetDamage(float dmg)
    {
        if (CurrentHealth == 0) { return; }
        if (dmg < 0) Debug.LogError("Can't take damage, damage must be > 0");

        OnTakeDamage?.Invoke();
        CurrentHealth -= dmg;
        if (CurrentHealth <= 0) { CurrentHealth = 0f; OnDie?.Invoke(); }
    }

    public void Heal(float value)
    {
        if (CurrentHealth == MaxHealth) { return; }
        if (value < 0) Debug.LogError("Can't heal, value must be > 0");

        OnHeal?.Invoke();
        CurrentHealth += value;
        if (CurrentHealth >= MaxHealth) CurrentHealth = MaxHealth;
    }
}
