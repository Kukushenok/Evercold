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
    [SerializeField] private Animator _animator;
    [SerializeField] private string _attackAnimationName;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    private void Attack()
    {
        _animator.Play(_attackAnimationName);
        Collider[] hitEnemies = Physics.OverlapSphere(_attackPoint.position, _attackRange, _enemyLayer); // finding all enemies
        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.TryGetComponent(out Health enemyHealth))
            {
                enemyHealth.GetDamage(_attackDamage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
}
