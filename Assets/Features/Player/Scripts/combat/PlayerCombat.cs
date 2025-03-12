using Feature.Health;
using Feature.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Feature.Player
{

    public class PlayerCombat : MonoBehaviour
    {
        [System.Serializable]
        public class AttackSettings
        {
            [field: SerializeField] public float AttackRange { get; private set; }
            [field: SerializeField] public float AttackDamage { get; private set; }
            [field: SerializeField] public LayerMask EnemyLayer { get; private set; }
            [field: SerializeField] public float PickUpRange { get; private set; }
            [field: SerializeField] public LayerMask PickUpLayer { get; private set; }
        }
        private PlayerInput _playerInput;
        [SerializeField] private GameObject _leftHandObject;
        [SerializeField] private GameObject _rightHandObject;
        [Header("attack params")]
        [SerializeField] private float _attackRange = 1.5f;
        [SerializeField] private float _attackDamage = 10;
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private Transform _attackPoint;

        [Header("pick up params")]
        [SerializeField] private float _pickUpRange = 1.5f;
        [SerializeField] private LayerMask _pickUpLayer;
        [SerializeField] private Transform _pickUpPoint;
        [SerializeField] private Transform _leftHandTransform;
        [SerializeField] private Transform _rightHandTransform;
        private PickUpItem _weapon = null;
        private PickUpItem _throwable = null;


        [Header("animation logic")]
        [SerializeField] private Animator _animator;
        [SerializeField] private string _leftPunchAnimationName;
        [SerializeField] private string _rightPunchAnimationName; // animation names are also trigger names
        [SerializeField] private string _forwardHitAnimationName;
        [SerializeField] private string _leftHitAnimationName;
        [SerializeField] private string _rightHitAnimationName;
        private bool _isRightPunch = true;
        private bool _canAttack = true;



        [Inject]
        public void Construct(AttackSettings settings, PlayerInput playerInput)
        {
            _attackRange = settings.AttackRange;
            _attackDamage = settings.AttackDamage;
            _enemyLayer = settings.EnemyLayer;
            _pickUpRange = settings.PickUpRange;
            _pickUpLayer = settings.PickUpLayer;
            _playerInput = playerInput;
        }


        private void Update()
        {
            // todo rewrite Input
            if (_canAttack && Input.GetMouseButtonDown(0))
            {
                PlayAttackAnimation();
            }

            if (_canAttack && Input.GetKeyDown(KeyCode.E))
            {
                TryPickUp();
            }

            if (_weapon != null && Input.GetKeyDown(KeyCode.Q))
            {
                DropWeapon();
            }
        }

        private void TryPickUp()
        {
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
                    if (pickUpItem._handType == HandType.Right && _weapon == null)
                    {
                        pickUpItem.PickUp(_rightHandTransform);
                        _rightHandObject.SetActive(false);
                        _weapon = pickUpItem;
                        break;
                    }

                }
            }
        }

        private void DropWeapon()
        {
            _weapon.Drop();
            _weapon = null;
            _rightHandObject.SetActive(true);
        }

        protected void Attack()
        {
            Collider[] hitEnemies = Physics.OverlapSphere(_attackPoint.position, _attackRange, _enemyLayer);
            foreach (Collider enemy in hitEnemies)
            {
                if (enemy.TryGetComponent(out IDamageable enemyHealth))
                {
                    enemyHealth.TakeDamage(_attackDamage);
                }
            }

        }

        protected void ResetAction() => _canAttack = true;

        private void OnDrawGizmosSelected()
        {
            if (_attackPoint == null)
                return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
        }

        private void PlayAttackAnimation()
        {
            if (_weapon != null)
            {
                string animationName = _forwardHitAnimationName;
                if (_playerInput.MovementX > 0) animationName = _rightHitAnimationName;
                else if (_playerInput.MovementX < 0) animationName = _leftHitAnimationName;
                _animator.SetTrigger(animationName);
                _canAttack = false;
                return;
            }
            if (_isRightPunch = !_isRightPunch) { _animator.SetTrigger(_rightPunchAnimationName); }
            else { _animator.SetTrigger(_leftPunchAnimationName); }
            _canAttack = false;
        }
    }

}
