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
        private PlayerFinder _playerFinder;

        public IdleState(EnemyStateMachine stateMachine, Enemy enemy, PlayerMovement player) : base(stateMachine, enemy, player)
        {
        }

        public override void Enter()
        {
            base.Enter();
            ResetRotation();
            _playerFinder = _enemy.GetComponent<PlayerFinder>();
            _animator.Play("Idle");
        }

        public override void Exit()
        {

        }

        public override void Tick()
        {
            if (_playerFinder.PlayerIsVisible())
            {
                _enemy.StatusIcon.Show(StatusIcon.IconType.Detect);
                _fsm.ChangeState(EnemyStateType.Chase);
                return;
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