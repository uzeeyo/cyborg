using Cyborg.Items;
using Cyborg.StateMachine;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace Cyborg.Enemies
{
    [RequireComponent(typeof(NavMeshAgent), typeof(EnemyStateMachine))]
    public class Enemy : MonoBehaviour
    {
        private EnemyStateMachine _stateMachine;
        private NavMeshAgent _agent;
        private PlayerMovement _player;

        [SerializeField] private float _health;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _stoppingDistance;
        [SerializeField] private float _detectionRange;
        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private EnemyStateType _defaultState;

        public NavMeshAgent Agent => _agent;
        public float MovementSpeed => _movementSpeed;
        public float DetectionRange => _detectionRange;
        public StatusIcon StatusIcon { get; private set; }
        public event Action<EnemyType> EnemyDied;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _player = FindObjectOfType<PlayerMovement>();
            _stateMachine = GetComponent<EnemyStateMachine>();

            _stateMachine.Init(this, _player, _defaultState);

            //Prevents weird rotations
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;

            _agent.speed = _movementSpeed;
            _agent.stoppingDistance = _stoppingDistance;

            StatusIcon = GetComponentInChildren<StatusIcon>();
        }

        private void TakeDamage(float damage)
        {
            if (_stateMachine.CurrentState == EnemyStateType.Die) return;

            _health -= damage;
            if (_health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            _stateMachine.ChangeState(EnemyStateType.Die);
            EnemyDied?.Invoke(_enemyType);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Projectile projectile))
            {
                TakeDamage(projectile.Damage);
            }
        }
    }
}