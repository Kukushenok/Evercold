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
        [SerializeField] private bool _indestructible;
        public bool indestructible => _indestructible;
        /// <summary>
        /// ��������� ���-�� ����������
        /// </summary>
        [SerializeField] private int _hitPoints;
        public int maxHitPoints => _hitPoints;
        /// <summary>
        /// ������� �������� ����������
        /// </summary>
        public int _currentHitpoints { get; private set; }

        [SerializeField] private UnityEvent _eventOnDeath;
        public UnityEvent eventOnDeath => _eventOnDeath;

        protected virtual void Start()
        {
            _currentHitpoints = _hitPoints;
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
            if (_indestructible) return;

            _currentHitpoints -= damage;

            if (_currentHitpoints <= 0)
                OnDeath();

        }

        /// <summary>
        /// ���������������� ������� �������, ����� ���-�� ���������� <= 0
        /// </summary>
        protected virtual void OnDeath()
        {
            Destroy(gameObject);
            _eventOnDeath?.Invoke();
        }
    }
}