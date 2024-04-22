using Cyborg.Enemies;
using UnityEngine;

namespace Cyborg.StateMachine
{
    public class ScanState : EnemyState
    {
        private const float SCAN_INTERVAL = 2f;
        private const int MAX_ATTEMPTS = 3;

        private PlayerFinder _playerFinder;
        private float _timeSinceScan = 0;
        private int _scanAttempts = 0;
        private Scanner _scanner;

        public ScanState(EnemyStateMachine stateMachine, Enemy enemy, PlayerMovement player) : base(stateMachine, enemy, player)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _playerFinder = _enemy.GetComponent<PlayerFinder>();
            _timeSinceScan = Time.time - _startTime;
            _scanner = _enemy.GetComponentInChildren<Scanner>();
        }

        public override void Exit()
        {
            _scanAttempts = 0;
            _timeSinceScan = 0;
        }

        public override void Tick()
        {
            var timeElapsed = Time.time - _startTime;
            if (_scanAttempts > MAX_ATTEMPTS)
            {
                _fsm.ChangeState(EnemyStateType.Idle);
            }

            if (_playerFinder.PlayerIsVisible())
            {
                _fsm.ChangeState(EnemyStateType.Chase);
            }

            if (_timeSinceScan >= SCAN_INTERVAL && _scanAttempts <= MAX_ATTEMPTS)
            {
                Debug.Log("Starting scan");
                _scanner.Scan();
                _timeSinceScan = 0;
                _scanAttempts++;
                return;
            }

            if (Vector2.Distance(_enemy.transform.position, _player.transform.position) <= _scanner.CurrentRange)
            {
                Debug.Log("Detected");
                _enemy.PlayerLastSeenPosition = _player.transform.position;
                _fsm.ChangeState(EnemyStateType.TravelToDetected);
            }

            _timeSinceScan += Time.deltaTime;
        }

    }
}