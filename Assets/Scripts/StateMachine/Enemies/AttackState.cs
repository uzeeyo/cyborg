using Cyborg.StateMachine;

namespace Cyborg.Enemies
{
    public class AttackState : EnemyState
    {
        public AttackState(EnemyStateMachine stateMachine, Enemy enemy, PlayerMovement player) : base(stateMachine, enemy, player)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {

        }

        public override void Tick()
        {

        }
    }
}