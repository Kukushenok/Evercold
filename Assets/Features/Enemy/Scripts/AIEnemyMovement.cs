using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Feature.Enemy
{
    public enum AIState
    {
        Patrol,
        Triggered
    }

    [RequireComponent(typeof(NavMeshAgent))]
    public class AIEnemyMovement : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        public Transform player => _player;

        [Header("Distance Properties")]
        [SerializeField] private float _distanceToDetectPlayer;
        [SerializeField] private float _patrolDistance;
        [SerializeField] private float _maxDistanceToStartPoint;

        private Vector3 _startPointPatrol;
        private NavMeshAgent _navMeshAgent;
        private AIState _state;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_patrolDistance > _maxDistanceToStartPoint)
            {
                _maxDistanceToStartPoint = _patrolDistance;
            }
        }
#endif

        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _startPointPatrol = transform.position;
            _state = AIState.Patrol;
        }

        private void Update()
        {
            UpdateBehaviourPatrol();
        }

        public void UpdateBehaviourPatrol()
        {
            CheckPlayerInVisibleArea();
            CheckAIOutRangeToStartPoint();
            SetTargetPosition();
        }

        private void CheckPlayerInVisibleArea()
        {
            if (_player != null)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, _player.position);
                if (distanceToPlayer < _distanceToDetectPlayer && Vector3.Distance(transform.position, _startPointPatrol) < _maxDistanceToStartPoint)
                {
                    _state = AIState.Triggered;
                }
            }
        }

        private void CheckAIOutRangeToStartPoint()
        {
            float distanceToStartPoint = Vector3.Distance(transform.position, _startPointPatrol);
            if (distanceToStartPoint > _maxDistanceToStartPoint)
            {
                _state = AIState.Patrol;
            }

            if (_player == null)
            {
                _state = AIState.Patrol;
            }
        }

        private void SetTargetPosition()
        {
            if (_state == AIState.Triggered)
            {
                if (_player != null)
                {
                    _navMeshAgent.destination = _player.position;
                }
            }

            if (_state == AIState.Patrol)
            {
                if (Vector3.Distance(transform.position, _navMeshAgent.destination) <= _navMeshAgent.stoppingDistance)
                {
                    Vector3 newPoint = UnityEngine.Random.onUnitSphere * _patrolDistance + _startPointPatrol;
                    newPoint.y = transform.position.y;

                    _navMeshAgent.destination = newPoint;
                }
            }
        }
    }
}
