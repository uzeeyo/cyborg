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
        private bool _facingPlayer;

        public override void Enter()
        {
            _playerFinder = _enemy.GetComponent<PlayerFinder>();
            _animator.Play("Move");
            _enemy.transform.up = (_player.transform.position - _enemy.transform.position).normalized;
            _timeSinceAttack = 0;
        }

        public override void Tick()
        {
            _timeSinceAttack += Time.deltaTime;
            if (_timeSinceAttack > _enemy.AttackSpeed)
            {
                _timeSinceAttack = 0;
                _fsm.ChangeState(EnemyStateType.Attack);
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