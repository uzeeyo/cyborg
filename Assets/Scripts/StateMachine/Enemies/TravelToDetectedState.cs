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
            _agent.SetDestination(_enemy.PlayerLastSeenPosition);

            _playerFinder = _enemy.GetComponent<PlayerFinder>();
        }

        public override void Exit()
        {
            _agent.stoppingDistance = _originalStoppingDistance;
            _timeDestinationReached = null;
        }

        public override void Tick()
        {
            if (_playerFinder.PlayerIsVisible())
            {
                _fsm.ChangeState(EnemyStateType.Chase);
                return;
            }

            if (_agent.remainingDistance < 1f)
            {
                _timeDestinationReached ??= Time.time;
                var timeSinceDestinationReached = Time.time - _timeDestinationReached;
                if (timeSinceDestinationReached >= TIME_TO_SEARCH)
                {
                    _fsm.ChangeState(EnemyStateType.Idle);
                }
            }


        }
    }
}