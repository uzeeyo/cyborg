using Cyborg.Enemies;

namespace Cyborg.StateMachine
{
    public class EnemyStateMachine : StateMachine
    {
        public void Init(Enemy enemy, PlayerMovement player, EnemyStateType defaultState)
        {
            _stateMap = new()
            {
                { EnemyStateType.Chase, new ChaseState(this, enemy, player) },
                { EnemyStateType.Idle, new IdleState(this, enemy, player) },
                { EnemyStateType.Attack, new AttackState(this, enemy, player) },
                { EnemyStateType.TravelToDetected, new TravelToDetectedState(this, enemy, player) },
                { EnemyStateType.Scan, new ScanState(this, enemy, player) },
                { EnemyStateType.Die, new DieState(this, enemy, player) }
            };

            ChangeState(defaultState);
        }
    }
}