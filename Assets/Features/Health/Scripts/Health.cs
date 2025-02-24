using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Feature.Health
{
    public class Health : MonoBehaviour, IDamageable
    {
        [field: SerializeField] public float MaxHealth { private set; get; }
        [field: SerializeField] public float CurrentHealth { private set; get; }

        public UnityEvent OnTakeDamage;
        public UnityEvent OnHeal;
        public UnityEvent OnDie;

        public void TakeDamage(float dmg)
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

    public interface IDamageable
    {
        void TakeDamage(float dmg);
    }
}
