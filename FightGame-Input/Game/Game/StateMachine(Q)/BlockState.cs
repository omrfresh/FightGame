using Game;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.StateMachine
{
    public class BlockState : IState
    {
        private float _blockTimer;
        private float _blockCooldown = 1f; // Задержка между блоками в секундах
        private Player _player;
        public bool IsBlocked { get; private set; }

        public BlockState(Player player)
        {
            _player = player;
        }

        public void Enter(Player player)
        {
            _blockTimer = 0;
        }

        public void Exit(Player player)
        {
            // Сброс таймера блокировки при выходе из состояния блокировки
            _blockTimer = 0;
            IsBlocked = false;
        }

        public void Update(Player player)
        {
            _blockTimer += (float)_player.gameWindow.ElapsedTime;

            if (_blockTimer >= _blockCooldown)
            {
                var opponent = GameEngine.GetOpponent(_player);

                if (opponent != null && GameEngine.IsInRange(_player, opponent, _player.AttackRange))
                {
                    IsBlocked = true;
                }

                _blockTimer = 0f;
            }

            // Если блок завершен, переходим в состояние "ожидание"
            if (_blockTimer == 0)
            {
                _player.ChangeState(new IdleState(_player));
            }
        }
    }
}
