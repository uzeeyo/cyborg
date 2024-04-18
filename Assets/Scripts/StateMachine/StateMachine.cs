using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Cyborg.StateMachine
{
    public abstract class StateMachine : MonoBehaviour
    {
        protected Dictionary<EnemyStateType, EnemyState> _stateMap;
        protected IState _currentState;

        public EnemyStateType CurrentState => _stateMap.Keys.First(x => _stateMap[x] == _currentState);

        public void ChangeState(EnemyStateType stateType)
        {
            if (_currentState != null)
            {
                _currentState.Exit();
            }

            _currentState = _stateMap[stateType];
            _currentState.Enter();
        }


        private void Update()
        {
            _currentState?.Tick();
        }
    }
}