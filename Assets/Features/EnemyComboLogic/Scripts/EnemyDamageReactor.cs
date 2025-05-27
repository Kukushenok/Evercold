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
    public class EnemyDamageReactor : MonoBehaviour, IDamageable
    {
        private Rigidbody rigidbody;
        private IGroundChecker groundChecker;
        private float verticalVelocity;
        [Inject]
        private void Construct(Rigidbody rigidbody, IGroundChecker groundChecker, float velocity)
        {
            this.rigidbody = rigidbody;
            this.groundChecker = groundChecker;
            verticalVelocity = velocity;
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
                    direction.y += verticalVelocity;
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
