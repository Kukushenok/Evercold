using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feature.Enemy
{
    public class AIBaseAttack : MonoBehaviour
    {
        [SerializeField] private int _Damage;
        [SerializeField] private float _FireRate;
        [SerializeField] private float _AttackDistance;
        [SerializeField] private Destructible Target; //TODO Заменить Destructible на класс хп персонажа

        private Timer _FireRateTimer;

        private void Start()
        {
            InitTimers();
        }

        private void Update()
        {
            Attack();
            UpdateTimers();
        }

        private void Attack()
        {
            if (Target != null)
            {
                if (Vector3.Distance(Target.GetComponent<Transform>().position, transform.position) <= _AttackDistance)
                {
                    if (_FireRateTimer.IsFinished == true)
                    {
                        Target.ApplyDamage(_Damage); //TODO Заменить на функцию нанесения урона персонажу.

                        _FireRateTimer.Start(_FireRate);
                    }
                }
            }
        }

        private void InitTimers()
        {
            _FireRateTimer = new Timer(_FireRate);
        }

        private void UpdateTimers()
        {
            _FireRateTimer.RemoveTime(Time.deltaTime);
        }
    }
}