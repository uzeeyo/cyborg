using Cyborg.StateMachine;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace Cyborg.Enemies
{
    [RequireComponent(typeof(NavMeshAgent), typeof(EnemyStateMachine))]
    public class Enemy : MonoBehaviour, I_TakeDamage
    {
        private EnemyStateMachine _stateMachine;
        private NavMeshAgent _agent;
        private PlayerMovement _player;
        public static short DeathCount = 0;
        [SerializeField] private float _health;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _stoppingDistance;
        [SerializeField] private float _detectionRange;
        [SerializeField] private float _attackSpeed = 1;
        [SerializeField, Range(0, 360)] private float _fov;
        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private EnemyStateType _defaultState;
        [SerializeField] private SpriteRenderer _scannerSprite;

        public NavMeshAgent Agent => _agent;
        public float DetectionRange => _detectionRange;
        public float AttackSpeed => _attackSpeed;
        public float Fov => _fov;
        public StatusIcon StatusIcon { get; private set; }
        public event Action<EnemyType> EnemyDied;
        public SpriteRenderer ScannerSprite => _scannerSprite;

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

        private void Update()
        {
            if (Vector3.Distance(transform.position, _player.transform.position) > 30)
            {
                Destroy(gameObject);
            }
        }

        public void TakeDamage(float damage)
        {
            if (_stateMachine.CurrentState == EnemyStateType.Die) return;

            _stateMachine.ChangeState(EnemyStateType.TravelToDetected);

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
            DeathCount++;
            if (DeathCount > 8)
            {
                LevelManager.OpenBossScene();
            }
        }
    }
}