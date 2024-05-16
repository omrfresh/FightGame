namespace Game.StateMachine
{
    public class IdleState : IState
    {
        private Player _player;
        public IdleState(Player player)
        {
            _player = player;
        }
        public void Enter(Player player)
        {
            // Нет действий в состояние "ожидание"
        }

        public void Exit(Player player)
        {
            // Нет действий в состояния "ожидание"
        }

        public void Update(Player player)
        {
            // Нет действий в состояния "ожидание"
        }
    }
}
