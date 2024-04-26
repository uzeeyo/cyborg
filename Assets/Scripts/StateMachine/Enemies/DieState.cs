using Cyborg.Enemies;
using UnityEngine;

namespace Cyborg.StateMachine
{
    public class DieState : EnemyState
    {
        public DieState(EnemyStateMachine stateMachine, Enemy enemy, PlayerMovement player) : base(stateMachine, enemy, player) { }

        public override void Enter()
        {
            base.Enter();
            _enemy.Agent.isStopped = true;
            _enemy.GetComponent<Collider2D>().enabled = false;
            _animator.Play("Die");
        }

        public override void Exit()
        {

        }

        public override void Tick()
        {
            var aniState = _animator.GetCurrentAnimatorStateInfo(0);
            if (aniState.IsName("Die") && Time.time - _startTime > aniState.length)
            {
                _enemy.GetComponent<BatteryDropper>().DropBattery();
                GameObject.Destroy(_enemy.gameObject);
            }
        }
    }
}