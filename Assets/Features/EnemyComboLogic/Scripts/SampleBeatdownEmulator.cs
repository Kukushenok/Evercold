using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Feature.EnemyComboLogic
{
    public class SampleBeatdownEmulator : ITickable
    {
        private IDamageable _damageable;
        private Transform trackTransform;
        public SampleBeatdownEmulator(Transform tr, IDamageable damageable)
        {
            trackTransform = tr;
            _damageable = damageable;
        }
        public void Tick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 toCenter = -(trackTransform.position + Random.insideUnitSphere*2.0f).normalized;
                _damageable.TakeDamage(new AttackData(1, toCenter));
            }
        }
    }
}
