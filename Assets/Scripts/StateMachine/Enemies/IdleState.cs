using Cyborg.Enemies;
using UnityEngine;

namespace Cyborg.StateMachine
{
    public class IdleState : EnemyState
    {
        private float _lookDirectionZ;
        private float _timeToRotate;
        private float _waitTime;
        private bool _waiting;

        public IdleState(EnemyStateMachine stateMachine, Enemy enemy, PlayerMovement player) : base(stateMachine, enemy, player)
        {
        }

        public override void Enter()
        {
            base.Enter();
            ResetRotation();
        }

        public override void Exit()
        {

        }

        public override void Tick()
        {
            if (Vector3.Distance(_enemy.transform.position, _player.transform.position) < _enemy.DetectionRange)
            {
                _fsm.ChangeState(EnemyStateType.Follow);
            }

            float timeSinceLastRotate = Time.time - _startTime;

            if (_waiting && timeSinceLastRotate > _waitTime)
            {
                _waiting = false;
            }

            if (_waiting) return;

            if (timeSinceLastRotate - _waitTime < _timeToRotate)
            {
                _enemy.transform.rotation = Quaternion.Slerp(_enemy.transform.rotation, Quaternion.Euler(0, 0, _lookDirectionZ), Time.deltaTime);
                return;
            }
            ResetRotation();
        }

        private void ResetRotation()
        {
            _waiting = true;
            _startTime = Time.time;
            _waitTime = Random.Range(0.3f, .8f);
            _lookDirectionZ = Random.Range(0, 360);
            _timeToRotate = Random.Range(0.5f, 1f);
        }
    }
}