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

        public void Enter(Player player)
        {
            _attackTimer = 0;
        }

        public void Exit(Player player)
        {
            // Сброс таймера атаки при выходе из состояния атаки
            _attackTimer = 0;
        }

        public void Update(Player player)
        {
            _attackTimer += (float)GameWindow.ElapsedTime;

            if (_attackTimer >= _attackCooldown)
            {
                // Нанесение урона противнику в радиусе атаки
                var enemies = GameEngine.GetEnemiesInRange(player.Position, player.AttackRange);
                foreach (var enemy in enemies)
                {
                    enemy.Health -= player.Damage;
                }

                // Сброс таймера атаки
                _attackTimer = 0;
            }

            // Если атака завершена, переходим в состояние "ожидание"
            if (_attackTimer == 0)
            {
                player.ChangeState(new IdleState());
            }
        }
    }

}
