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
        [SerializeField] private bool _Indestructible;
        public bool Indestructible => _Indestructible;
        /// <summary>
        /// Стартовое кол-во хитпоинтов
        /// </summary>
        [SerializeField] private int _HitPoints;
        public int MaxHitPoints => _HitPoints;
        /// <summary>
        /// Текущее значение хитпоинтов
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
        /// Применение урона к объекту
        /// </summary>
        /// <param name="damage"> Урон, наносимый объекту</param>
        public void ApplyDamage(int damage)
        {
            if (_Indestructible) return;

            _CurrentHitpoints -= damage;

            if (_CurrentHitpoints <= 0)
                OnDeath();

        }

        /// <summary>
        /// Переопределяемое событие объекта, когда кол-во хитпоинтов <= 0
        /// </summary>
        protected virtual void OnDeath()
        {
            Destroy(gameObject);
            _EventOnDeath?.Invoke();
        }
    }
}