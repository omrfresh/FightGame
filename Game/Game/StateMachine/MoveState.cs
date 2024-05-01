using OpenTK.Mathematics;

namespace Game.StateMachine
{
    public class MoveState : IState
    {
        private Player _player;
        private Vector2 _direction;

        public MoveState(Player player, Vector2 direction)
        {
            _player = player;
            _direction = direction;
        }

        public void Enter(Player player)
        {
            // Пусто, потому что нет никаких действий при входе в состояние MoveState
        }

        public void Exit(Player player)
        {
            // Пусто, потому что нет никаких действий при выходе из состояния MoveState
        }

        public void Update(Player player)
        {
            Vector2 newPosition = _player.Position + _direction * _player.Speed * (float)_player.gameWindow.ElapsedTime;

            // Проверка на выход за пределы экрана
            if (newPosition.X < -0.9f)
            {
                newPosition.X = -0.9f;
            }
            else if (newPosition.X > 0.9f)
            {
                newPosition.X = 0.9f;
            }

            _player.Position = newPosition;
        }
    }
}
