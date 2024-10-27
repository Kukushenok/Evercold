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
        [SerializeField] private Transform _Player;
        public Transform Player => _Player;

        [Header("Distance Properties")]
        [SerializeField] private float _DistanceToDetectPlayer;
        [SerializeField] private float _PatrolDistance;
        [SerializeField] private float _MaxDistanceToStartPoint;

        private Vector3 _StartPointPatrol;
        private NavMeshAgent _NavMeshAgent;
        private AIState _State;
        private Timer _FindNewPositionTimer;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_PatrolDistance > _MaxDistanceToStartPoint)
            {
                _MaxDistanceToStartPoint = _PatrolDistance;
            }
        }
#endif

        private void Start()
        {
            _NavMeshAgent = GetComponent<NavMeshAgent>();
            _StartPointPatrol = transform.position;
            _State = AIState.Patrol;
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
            if (_Player != null)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, _Player.position);
                if (distanceToPlayer < _DistanceToDetectPlayer && Vector3.Distance(transform.position, _StartPointPatrol) < _MaxDistanceToStartPoint)
                {
                    _State = AIState.Triggered;
                }
            }
        }

        private void CheckAIOutRangeToStartPoint()
        {
            float distanceToStartPoint = Vector3.Distance(transform.position, _StartPointPatrol);
            if (distanceToStartPoint > _MaxDistanceToStartPoint)
            {
                _State = AIState.Patrol;
            }

            if (_Player == null)
            {
                _State = AIState.Patrol;
            }
        }

        private void SetTargetPosition()
        {
            if (_State == AIState.Triggered)
            {
                if (_Player != null)
                {
                    _NavMeshAgent.destination = _Player.position;
                }
            }

            if (_State == AIState.Patrol)
            {
                if (Vector3.Distance(transform.position, _NavMeshAgent.destination) <= _NavMeshAgent.stoppingDistance)
                {
                    Vector3 newPoint = UnityEngine.Random.onUnitSphere * _PatrolDistance + _StartPointPatrol;
                    newPoint.y = transform.position.y;

                    _NavMeshAgent.destination = newPoint;
                }
            }
        }
    }
}
