using Cyborg.StateMachine;
using UnityEngine;

namespace Cyborg.Enemies
{
    public class AttackState : EnemyState
    {
        private EnemyAttack _enemyAttack;
        private Vector2 _playerPosition;

        public AttackState(EnemyStateMachine stateMachine, Enemy enemy, PlayerMovement player) : base(stateMachine, enemy, player)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("Attack State");
            _enemyAttack = _enemy.GetComponentInChildren<EnemyAttack>();
            _playerPosition = _player.transform.position;
            _enemyAttack.Attack(_playerPosition);
            _fsm.ChangeState(EnemyStateType.Chase);
        }

        public override void Exit()
        {

        }

        public override void Tick()
        {

        }
    }
}