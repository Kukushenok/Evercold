using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Feature.Enemy
{
    /// <summary>
    /// ������������ ������ �� �����. ��, ��� ����� ����� ���������
    /// </summary>
    public class Destructible : MonoBehaviour
    {
        /// <summary>
        /// ������ ���������� �����������
        /// </summary>
        [SerializeField] private bool _Indestructible;
        public bool Indestructible => _Indestructible;
        /// <summary>
        /// ��������� ���-�� ����������
        /// </summary>
        [SerializeField] private int _HitPoints;
        public int MaxHitPoints => _HitPoints;
        /// <summary>
        /// ������� �������� ����������
        /// </summary>
        private int _CurrentHitpoints;
        public int CurrentHitpoints => _CurrentHitpoints;

        [SerializeField] private UnityEvent _EventOnDeath;
        public UnityEvent EventOnDeath => _EventOnDeath;

        protected virtual void Start()
        {
            _CurrentHitpoints = _HitPoints;
        }

        protected virtual void Update()
        {
        }

        /// <summary>
        /// ���������� ����� � �������
        /// </summary>
        /// <param name="damage"> ����, ��������� �������</param>
        public void ApplyDamage(int damage)
        {
            if (_Indestructible) return;

            _CurrentHitpoints -= damage;

            if (_CurrentHitpoints <= 0)
                OnDeath();

        }

        /// <summary>
        /// ���������������� ������� �������, ����� ���-�� ���������� <= 0
        /// </summary>
        protected virtual void OnDeath()
        {
            Destroy(gameObject);
            _EventOnDeath?.Invoke();
        }
    }
}