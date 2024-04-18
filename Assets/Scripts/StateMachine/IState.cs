namespace Cyborg.StateMachine
{
    public interface IState
    {
        void Enter();
        void Tick();
        void Exit();
    }
}