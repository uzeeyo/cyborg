using Cyborg.StateMachine;
using UnityEngine.AI;

namespace Cyborg.Enemies
{
    public class AttackState : EnemyState
    {
        private EnemyAttack _enemyAttack;
        private NavMeshAgent _agent;

        public AttackState(EnemyStateMachine stateMachine, Enemy enemy, PlayerMovement player) : base(stateMachine, enemy, player)
        {
            _agent = _enemy.Agent;
            _enemyAttack = _enemy.GetComponentInChildren<EnemyAttack>();
        }

        public override void Enter()
        {
            base.Enter();
            _agent.isStopped = true;

            _enemyAttack.AttackComplete += OnAttackComplete;
            _enemyAttack.Attack(_player.transform);

        }

        private void OnAttackComplete()
        {
            _enemyAttack.AttackComplete -= OnAttackComplete;
            _fsm.ChangeState(EnemyStateType.Chase);
        }

        public override void Exit()
        {
            _agent.isStopped = false;
        }

        public override void Tick()
        {

        }
    }
}