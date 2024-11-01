using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feature.Enemy
{
    public class AIBaseAttack : MonoBehaviour
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _fireRate;
        [SerializeField] private float _attackDistance;
        [SerializeField] private Destructible target; //TODO �������� Destructible �� ����� �� ���������

        private Timer _FireRateTimer;

        private void Start()
        {
            StartCoroutine(AttackCoroutine());
        }

        public IEnumerator AttackCoroutine()
        {
            while (true)
            {
                if (target != null)
                {
                    if (Vector3.Distance(target.GetComponent<Transform>().position, transform.position) <= _attackDistance)
                    {
                        target.ApplyDamage(_damage); //TODO �������� �� ������� ��������� ����� ���������.
                        yield return new WaitForSeconds(_fireRate);
                    }
                    else
                    {
                        yield return new WaitForEndOfFrame();
                    }
                }
                else
                {
                    yield return new WaitForEndOfFrame();
                }
            }
        }
    }
}