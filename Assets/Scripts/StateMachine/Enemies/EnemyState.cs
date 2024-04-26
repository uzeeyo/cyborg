using Cyborg.Enemies;
using UnityEngine;

namespace Cyborg.StateMachine
{
    public abstract class EnemyState : IState
    {
        public EnemyState(EnemyStateMachine stateMachine, Enemy enemy, PlayerMovement player)
        {
            _fsm = stateMachine;
            _enemy = enemy;
            _player = player;
            _animator = enemy.GetComponent<Animator>();
        }

        protected EnemyStateMachine _fsm;
        protected Enemy _enemy;
        protected PlayerMovement _player;
        protected Animator _animator;
        protected float _startTime;

        public virtual void Enter()
        {
            _startTime = Time.time;
        }

        public abstract void Tick();
        public abstract void Exit();
    }
}