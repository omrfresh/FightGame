using Game;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.StateMachine
{
    public class AttackState : IState
    {
        private float _attackTimer;
        private float _attackCooldown = 1f; // Задержка между атаками в секундах
        private Player _player;
        public bool IsOpponentKilled { get; private set; }
        public AttackState(Player player)
        {
            _player = player;
        }

        public void Enter(Player player)
        {
            _attackTimer = 0;
        }

        public void Exit(Player player)
        {
            // Сброс таймера атаки при выходе из состояния атаки
            _attackTimer = 0;
            IsOpponentKilled = false;
        }

        public void Update(Player player)
        {
            _attackTimer += (float)_player.gameWindow.ElapsedTime;

            if (_attackTimer >= _attackCooldown)
            {
                var opponent = GameEngine.GetOpponent(_player);

                if (opponent != null && GameEngine.IsInRange(_player, opponent, _player.AttackRange))
                {
                    opponent.Health -= _player.Damage;

                    if (opponent.Health <= 0)
                    {
                        IsOpponentKilled = true;
                    }
                }

                _attackTimer = 0f;
            }

            // Если атака завершена, переходим в состояние "ожидание"
            if (_attackTimer == 0)
            {
                _player.ChangeState(new IdleState(_player));
            }
        }
    }


}
