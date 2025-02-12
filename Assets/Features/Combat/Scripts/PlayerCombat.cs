using Feature.Health;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private GameObject _leftHandObject;
    [SerializeField] private GameObject _rightHandObject;
    [Header("attack params")]
    [SerializeField] private float _attackRange = 1.5f;
    [SerializeField] private int _attackDamage = 10;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private Transform _attackPoint;

    [Header("pick up params")]
    [SerializeField] private float _pickUpRange = 1.5f;
    [SerializeField] private LayerMask _pickUpLayer;
    [SerializeField] private Transform _pickUpPoint;
    [SerializeField] private Transform _leftHandTransform;
    [SerializeField] private Transform _rightHandTransform;


    [Header("animation logic")]
    [SerializeField] private Animator _animator;
    [SerializeField] private string _leftPunchAnimationName;
    [SerializeField] private string _rightPunchAnimationName; // animation names are also trigger names
    private bool _isRightPunch = true;
    private bool _canDoAction = true;

    
    

    private void Update()
    {
        if (_canDoAction && Input.GetMouseButtonDown(0))
        {
            PlayAttackAnimation();

        }

        if (_canDoAction && Input.GetKeyDown(KeyCode.E))
        {   
            TryPickUp();
        }
    }

    private void TryPickUp() {
        Collider[] items = Physics.OverlapSphere(_pickUpPoint.position, _pickUpRange, _pickUpLayer);
        foreach (Collider item in items)
        {
            if (item.TryGetComponent(out PickUpItem pickUpItem))
            {
                if (pickUpItem._handType == HandType.Left)
                {   
                    pickUpItem.PickUp(_leftHandTransform);
                    _leftHandObject.SetActive(false);
                    break;
                }
                if (pickUpItem._handType == HandType.Right)
                {   
                    
                    pickUpItem.PickUp(_rightHandTransform);
                    _rightHandObject.SetActive(false);
                    break;
                }

            }
        }
    }

    private void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(_attackPoint.position, _attackRange, _enemyLayer);
        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.TryGetComponent(out IDamageable enemyHealth))
            {
                enemyHealth.TakeDamage(_attackDamage);
            }
        }
        _canDoAction = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }

    private void PlayAttackAnimation()
    { 
        if (_isRightPunch=!_isRightPunch) { _animator.SetTrigger(_rightPunchAnimationName); }
        else {_animator.SetTrigger(_leftPunchAnimationName); }
        _canDoAction = false;
    }

    private void PlayLeftHandAnimation() 
    {
        
    }
}
