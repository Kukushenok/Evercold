using Feature.Health;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private float _attackRange = 1.5f;
    [SerializeField] private int _attackDamage = 10;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private Transform _attackPoint;

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
            PlayAnimation();

        }

        if (_canDoAction && Input.GetKeyDown(KeyCode.E))
        {   
            
        }
    }

    

    private void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(_attackPoint.position, _attackRange, _enemyLayer); // finding all enemies
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

    private void PlayAnimation()
    { 
        if (_isRightPunch=!_isRightPunch) { _animator.SetTrigger(_rightPunchAnimationName); }
        else {_animator.SetTrigger(_leftPunchAnimationName); }
        _canDoAction = false;
    }
}
