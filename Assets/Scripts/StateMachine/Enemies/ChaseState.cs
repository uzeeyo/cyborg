using Cyborg.Enemies;
using UnityEngine;

namespace Cyborg.StateMachine
{
    public class ChaseState : EnemyState
    {
        public ChaseState(EnemyStateMachine stateMachine, Enemy enemy, PlayerMovement player) : base(stateMachine, enemy, player)
        { }

        private float _timeSinceAttack = 0;
        private PlayerFinder _playerFinder;

        public override void Enter()
        {
            _enemy.StatusIcon.Show(StatusIcon.IconType.Detect);
            _playerFinder = _enemy.GetComponent<PlayerFinder>();
        }

        public override void Tick()
        {
            _timeSinceAttack += Time.deltaTime;
            if (_timeSinceAttack > _enemy.AttackSpeed)
            {
                _fsm.ChangeState(EnemyStateType.Attack);
                _timeSinceAttack = 0;
                return;
            }

            if (_playerFinder.PlayerIsVisible())
            {
                _enemy.Agent.SetDestination(_player.transform.position);
                var direction = (_player.transform.position - _enemy.transform.position).normalized;
                direction.z = 0;
                _enemy.transform.up = direction;
                return;
            }

            _fsm.ChangeState(EnemyStateType.Scan);
        }

        public override void Exit()
        {
            _timeSinceAttack = 0;
        }
    }
}