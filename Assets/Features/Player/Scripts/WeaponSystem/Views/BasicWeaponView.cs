using Feature.Player.WeaponManager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
namespace Feature.Player.WeaponManager
{
    public class BasicWeaponView : MonoBehaviour, IWeaponView
    {
        public bool Enabled { get => gameObject.activeSelf; set => gameObject.SetActive(value); }
        public UnityEvent BasicEventBridge;
        private IWeaponShootCallback logic;
        [Inject]
        private void Construct(IWeaponShootCallback logic)
        {
            this.logic = logic;
            logic.OnShootButtonPressed.AddListener(ShootButtonPressed);
        }
        private void ShootButtonPressed()
        {
            BasicEventBridge?.Invoke();
        }
        public void Dispose()
        {
            logic.OnShootButtonPressed.RemoveListener(ShootButtonPressed);
            Destroy(gameObject);
        }
    }
}
