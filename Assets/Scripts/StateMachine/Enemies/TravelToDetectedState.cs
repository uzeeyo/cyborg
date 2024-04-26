using Cyborg.Enemies;
using UnityEngine;
using UnityEngine.AI;

namespace Cyborg.StateMachine
{
    public class TravelToDetectedState : EnemyState
    {
        public TravelToDetectedState(EnemyStateMachine stateMachine, Enemy enemy, PlayerMovement player) : base(stateMachine, enemy, player)
        {
        }

        private const float TIME_TO_SEARCH = 5f;

        private PlayerFinder _playerFinder;
        private NavMeshAgent _agent;
        private float _originalStoppingDistance;
        private float? _timeDestinationReached;

        public override void Enter()
        {
            base.Enter();

            _agent = _enemy.GetComponent<NavMeshAgent>();
            _originalStoppingDistance = _agent.stoppingDistance;
            _agent.stoppingDistance = 0;
            _agent.updatePosition = false;
            _agent.SetDestination(_player.transform.position);

            _playerFinder = _enemy.GetComponent<PlayerFinder>();
        }

        public override void Exit()
        {
            _agent.stoppingDistance = _originalStoppingDistance;
            _timeDestinationReached = null;
            _agent.updatePosition = true;
        }

        public override void Tick()
        {
            if (_playerFinder.PlayerIsVisible())
            {
                _fsm.ChangeState(EnemyStateType.Chase);
                return;
            }

            if (_agent.remainingDistance == 0)
            {
                _timeDestinationReached ??= Time.time;
                _enemy.transform.Rotate(0, 0, 2);
                var timeSinceDestinationReached = Time.time - _timeDestinationReached;
                if (timeSinceDestinationReached >= TIME_TO_SEARCH)
                {
                    _fsm.ChangeState(EnemyStateType.Idle);
                }
            }
            else
            {
                var direction = (_agent.nextPosition - _enemy.transform.position).normalized;
                _enemy.transform.up = direction;
                _enemy.transform.position = _agent.nextPosition;
            }


        }
    }
}