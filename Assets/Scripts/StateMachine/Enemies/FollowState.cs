using Cyborg.Enemies;
using UnityEngine;

namespace Cyborg.StateMachine
{
    public class FollowState : EnemyState
    {
        public FollowState(EnemyStateMachine stateMachine, Enemy enemy, PlayerMovement player) : base(stateMachine, enemy, player)
        {

        }

        public override void Enter()
        {
            _enemy.StatusIcon.Show(StatusIcon.IconType.Detect);
        }

        public override void Tick()
        {
            _enemy.Agent.SetDestination(_player.transform.position);
            var direction = (_player.transform.position - _enemy.transform.position).normalized;
            direction.z = 0;
            _enemy.transform.up = direction;

            if (Vector3.Distance(_enemy.transform.position, _player.transform.position) > _enemy.DetectionRange)
            {
                _fsm.ChangeState(EnemyStateType.Idle);
            }
        }

        public override void Exit()
        {
            UnityEngine.Debug.Log("Stopped following player");
        }
    }
}