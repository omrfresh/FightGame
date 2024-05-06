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
            // Код для перехода в состояние "ожидание"
        }

        public void Exit(Player player)
        {
            // Код для выхода из состояния "ожидание"
        }

        public void Update(Player player)
        {
            // Обновление логики для состояния "ожидание"
        }
    }
}
