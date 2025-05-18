using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feature.Player
{
    [System.Serializable]
    public class PlayerHealth : IHealth, IDamageable
    {
        [field: SerializeField] public float MaxHealth { get; private set; } = 0f;
        [field: SerializeField] public float Health { get; private set; } = 0f;
        public bool IsDead() { return MaxHealth <= 0; }
        public void TakeDamage(AttackData attackData)
        {
            if ((Health -= attackData.Damage) <= 0) Health = 0;
        }

    }
}
