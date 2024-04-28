using Game.StateMachine;

namespace Game
{
    public class Player
    {
        private IState _currentState;

        public void ChangeState(IState newState)
        {
            _currentState?.Exit(this);
            _currentState = newState;
            _currentState.Enter(this);
        }

        public void Update()
        {
            _currentState.Update(this);
        }
    }
}