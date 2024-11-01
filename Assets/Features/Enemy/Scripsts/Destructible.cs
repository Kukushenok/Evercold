using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Feature.Enemy
{
    /// <summary>
    /// Уничтожаемый объект на сцене. То, что может иметь хитпоинты
    /// </summary>
    public class Destructible : MonoBehaviour
    {
        /// <summary>
        /// Объект игнорирует повреждения
        /// </summary>
        [SerializeField] private bool _indestructible;
        public bool indestructible => _indestructible;
        /// <summary>
        /// Стартовое кол-во хитпоинтов
        /// </summary>
        [SerializeField] private int _hitPoints;
        public int maxHitPoints => _hitPoints;
        /// <summary>
        /// Текущее значение хитпоинтов
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
        /// Применение урона к объекту
        /// </summary>
        /// <param name="damage"> Урон, наносимый объекту</param>
        public void ApplyDamage(int damage)
        {
            if (_indestructible) return;

            _currentHitpoints -= damage;

            if (_currentHitpoints <= 0)
                OnDeath();

        }

        /// <summary>
        /// Переопределяемое событие объекта, когда кол-во хитпоинтов <= 0
        /// </summary>
        protected virtual void OnDeath()
        {
            Destroy(gameObject);
            _eventOnDeath?.Invoke();
        }
    }
}