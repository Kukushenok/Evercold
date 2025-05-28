using UnityEngine;

namespace Feature.EnemyComboLogic
{
    public class EnemyDamageReactorCallbackReciever : MonoBehaviour, IHealth, IEnemyDamageReactorCallbackReciever
    {
        [SerializeField] private Animator anim;

        [field:SerializeField] public float Health { get; private set; }

        [field: SerializeField] public float MaxHealth { get; private set; }
        [SerializeField] private GameObject Core;

        public void Damaged(AttackData dt)
        {
            Health -= dt.Damage;
            anim.Play("damaged");
            if(Health <= 0)
            {
                Destroy(Core);
            }
        }

        public void GroundedStateChanged(bool state)
        {
            anim.SetBool("grounded", state);
        }
    }
}
