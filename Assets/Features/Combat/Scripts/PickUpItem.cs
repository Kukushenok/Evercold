using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public readonly HandType _handType = HandType.Right; // this means what hand will character use to hold this Item 

    [SerializeField] private Vector3 _positionOffset;
    [SerializeField] private Vector3 rotationOffset;
    

    private Rigidbody _rigidbody;
    private bool _isPickedUp = false;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void PickUp(Transform hand)
    {
        transform.localPosition = _positionOffset;
        transform.localRotation = Quaternion.Euler(rotationOffset);

        transform.SetParent(hand);
        _isPickedUp = true;

        if (_rigidbody != null)
        {
            _rigidbody.isKinematic = true;
        }
        transform.localPosition = _positionOffset;
        transform.localRotation = Quaternion.Euler(rotationOffset);
    }

    public void Drop()
    {
        transform.SetParent(null);
        _isPickedUp = false;

        // Включаем физику, чтобы предмет снова мог падать
        if (_rigidbody != null)
        {
            _rigidbody.isKinematic = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Проверяем, что игрок находится рядом с предметом и нажимает клавишу для поднятия
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            // Ищем объект руки у игрока
            Transform playerHand = other.transform.Find("Hand"); // Убедитесь, что объект "Hand" существует
            if (playerHand != null)
            {
                PickUp(playerHand);
            }
        }
    }
}
public enum HandType
{
    Left,
    Right
}
