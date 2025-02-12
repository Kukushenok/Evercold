using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [field: SerializeField] public HandType _handType {private set; get;} = HandType.Right; // this means what hand will character use to hold this Item 

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
        
        transform.SetParent(hand, worldPositionStays: true);
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

        if (_rigidbody != null)
        {
            _rigidbody.isKinematic = false;
        }
    }
}
public enum HandType
{
    Left,
    Right
}
