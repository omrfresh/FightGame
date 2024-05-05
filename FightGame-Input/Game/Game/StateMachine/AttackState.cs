using Game;
using Game.StateMachine;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;

namespace Game.StateMachine
{
    public class AttackState : IState
    {
        private float _attackTimer;
        private float _attackCooldown = 0.25f;
        private Player _player;
        private Player _opponent;
        public bool IsOpponentKilled { get; private set; }
        public AttackState(Player player)
        {
            _player = player;
            _opponent = player.Opponent;
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
        private bool IsInAttackRange()
        {
            float distance = Vector2.Distance(_player.Position, _opponent.Position);
            return distance <= _player.AttackRange;
        }
        public void Update(Player player)
        {
            _attackTimer += (float)0.95;

            if (_attackTimer >= _attackCooldown)
            {
                if (IsInAttackRange())
                {
                    if (_opponent.IsBlocking) // проверяем, блокирует ли противник атаки
                    {
                        _opponent.Health -= 0;
                    }
                    else
                    {
                        _opponent.Health -= player.Damage;
                    }
                    _attackTimer = 1f;

                        using (StreamWriter sw = new StreamWriter("debug.log", false))
                    {
                        sw.WriteLine($"{DateTime.Now}: AttackState.Update() called, _opponent.Health = {_opponent.Health}, damage ={player.Damage}");
                    }

                    if (_opponent.Health <= 0)
                    {
                        IsOpponentKilled = true;
                        _attackTimer = 0;
                    }
                }
            }

            if (_attackTimer == 0)
            {
                player.ChangeState(new IdleState(player));
            }
        }
    }
}


////public void Update(Player player)
////{
////    _attackTimer += (float)_player.gameWindow.ElapsedTime;

////    if (_attackTimer >= _attackCooldown)
////    {
////        var opponent = GameEngine.GetOpponent(_player);

////        if (opponent != null && GameEngine.IsInRange(_player, opponent, _player.AttackRange))
////        {
////            if (opponent.GetCurrentState() is BlockState && opponent.IsBlocked)
////            {
////                // Удар блокирован
////                opponent.Health -= _player.Damage * 0.5f;
////            }
////            else
////            {
////                opponent.Health -= _player.Damage;

////                if (opponent.Health <= 0)
////                {
////                    IsOpponentKilled = true;
////                }
////            }
////        }

////        _attackTimer = 0f;
////    }

////    // Если атака завершена, переходим в состояние "ожидание"
////    if (_attackTimer == 0)
////    {
////        _player.ChangeState(new IdleState(_player));
////    }
////}