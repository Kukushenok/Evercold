using Feature.Health;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feature.EnemyComboLogic
{
    public interface IGroundChecker
    {
        public bool IsOnGround();
    }
    public class SampleGroundChecker: IGroundChecker
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
    public class EnemyDamageReactor : IDamageable
    {
        private Rigidbody rigidbody;
        private IGroundChecker groundChecker;
        public EnemyDamageReactor(Rigidbody rigidbody, IGroundChecker groundChecker)
        {
            this.rigidbody = rigidbody;
            this.groundChecker = groundChecker;
        }

        public void TakeDamage(AttackData attackData)
        {
            if (attackData.AssociatedKnockback != null) {
                Vector3 direction = attackData.AssociatedKnockback.Value;
                direction.y = 0;
                direction = direction.normalized;
                Vector3 associatedVelocity = Vector3.zero;
                bool yElevationSaver = false;
                if (groundChecker.IsOnGround())
                {
                    direction.y += 2;
                }
                else
                {
                    yElevationSaver = true;
                }
                direction *= 4;
                rigidbody.AddForce(direction, ForceMode.Impulse);
                if (yElevationSaver)
                {
                    rigidbody.velocity += new Vector3(0, 1.0f - rigidbody.velocity.y, 0);
                }
            }
        }
    }
}
