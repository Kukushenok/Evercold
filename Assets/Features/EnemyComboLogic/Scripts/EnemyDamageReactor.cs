using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Feature.EnemyComboLogic
{
    public interface IGroundChecker
    {
        public bool IsOnGround();
    }
    public class SampleGroundChecker : IGroundChecker
    {
        private float yLevel;
        private Transform transform;
        public SampleGroundChecker(float yLevel, Transform transform)
        {
            this.yLevel = yLevel;
            this.transform = transform;
        }

        public bool IsOnGround()
        {
            return transform.position.y <= yLevel;
        }
    }
    public interface IEnemyDamageReactorCallbackReciever
    {
        public void GroundedStateChanged(bool state);
        public void Damaged(AttackData dt);
    }
    public class EnemyDamageReactor : MonoBehaviour, IDamageable
    {
        private Rigidbody rigidbody;
        private IGroundChecker groundChecker;
        private Settings settings;
        private IEnemyDamageReactorCallbackReciever reciever;
        private bool wasOnGround = false;
        [Inject]
        private void Construct(Rigidbody rigidbody, IGroundChecker groundChecker, IEnemyDamageReactorCallbackReciever reciever, Settings settings)
        {
            this.rigidbody = rigidbody;
            this.groundChecker = groundChecker;
            this.settings = settings;
            this.reciever = reciever;
        }

        public void TakeDamage(AttackData attackData)
        {
            if (attackData.AssociatedKnockback != null)
            {
                Vector3 direction = attackData.AssociatedKnockback.Value;
                direction.y = 0;
                Vector3 associatedVelocity = Vector3.zero;
                bool yElevationSaver = false;
                if (groundChecker.IsOnGround())
                {
                    direction.y += settings.VerticalVelocity;
                }
                else
                {
                    yElevationSaver = true;
                }
                rigidbody.AddForce(direction, ForceMode.Impulse);
                if (yElevationSaver)
                {
                    rigidbody.velocity += new Vector3(0, Physics.gravity.magnitude * settings.ElevationSafeTime / 2 - rigidbody.velocity.y * settings.ElevationVelocityRatio, 0);
                }
            }
            reciever.Damaged(attackData);
        }
        public void Update()
        {
            bool gdr = groundChecker.IsOnGround();
            if (gdr != wasOnGround)
            {
                reciever.GroundedStateChanged(gdr);
                wasOnGround = gdr;
            }
        }
        [Serializable]
        public class Settings
        {
            [field: SerializeField] public float VerticalVelocity { get; private set; }
            [field: SerializeField] public float ElevationSafeTime { get; private set; }
            [field: SerializeField, Range(0, 1)] public float ElevationVelocityRatio { get; private set; }
        }
    }
}
