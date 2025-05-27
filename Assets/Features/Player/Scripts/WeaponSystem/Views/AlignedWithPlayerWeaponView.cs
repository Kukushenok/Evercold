using Feature.Player;
using Feature.Player.WeaponManager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class AlignedWithPlayerWeaponView : MonoBehaviour, IEnableable, IDisposable
{
    private IPlayerLocomotion locomotion;
    private IWeaponShootCallback weaponShootCallback;
    public UnityEvent BasicEventBridge;
    public bool Enabled { get => gameObject.activeSelf; set => gameObject.SetActive(value); }

    public void Dispose()
    {
        weaponShootCallback.OnShootButtonPressed.RemoveListener(OnCallback);
    }

    [Inject]
    private void Construct(IWeaponShootCallback shootCallback, IPlayerLocomotion loc)
    {
        this.locomotion = loc;
        weaponShootCallback = shootCallback;
        weaponShootCallback.OnShootButtonPressed.AddListener(OnCallback);
    }
    void OnCallback()
    {
        BasicEventBridge?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Quaternion.Euler(locomotion.CameraRotation);
    }
}
