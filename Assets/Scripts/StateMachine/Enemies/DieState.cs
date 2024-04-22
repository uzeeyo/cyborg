using Cyborg.Enemies;
using UnityEngine;

namespace Cyborg.StateMachine
{
    public class DieState : EnemyState
    {
        public DieState(EnemyStateMachine stateMachine, Enemy enemy, PlayerMovement player) : base(stateMachine, enemy, player)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _enemy.Agent.isStopped = true;
            _enemy.GetComponent<Collider2D>().enabled = false;
        }

        public override void Exit()
        {
            GameObject.Destroy(_enemy.gameObject);
        }

        public override void Tick()
        {
            if (Time.time - _startTime > 1)
            {
                Exit();
            }
        }
    }
}