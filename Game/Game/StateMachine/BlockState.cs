﻿using Game;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Game.StateMachine
{
    public class BlockState : IState
    {
        private float _blockTimer;
        private float _blockCooldown = 1f; // Задержка между блоками в секундах
        private Player _player;
        public bool IsBlocked { get; private set; }
        private float _lastBlockTime; // Время последнего блока

        public BlockState(Player player)
        {
            _player = player;
            _lastBlockTime = (float)_player.gameWindow.ElapsedTime;
        }

        public void Enter(Player player)
        {
            _blockTimer = 0;
            _player.Block(); // устанавливаем флаг блокировки
        }

        public void Exit(Player player)
        {
            // Сброс таймера блокировки при выходе из состояния блокировки
            _blockTimer = 0;
            IsBlocked = false;
            _player.Unblock(); // сбрасываем флаг блокировки
        }

        public void Update(Player player)
        {
            using (StreamWriter sw = new StreamWriter("debugBlock.log", true))
            {
                sw.WriteLine($"{DateTime.Now}: BlockState.Update() called, _player.Name = {_player.Name}, IsBlocked = {IsBlocked}");
            }
            _blockTimer += (float)_player.gameWindow.ElapsedTime;

            if (_blockTimer >= _blockCooldown)
            {
                var opponent = GameEngine.GetOpponent(_player);

                if (opponent != null && GameEngine.IsInRange(_player, opponent, _player.AttackRange))
                {
                    if (_player.IsBlocking) // проверяем, блокирует ли игрок атаки противника
                    {
                        IsBlocked = true;
                        _blockTimer = 0f;
                    }
                }
            }

            // Если блок завершен, переходим в состояние "ожидание"
            if (_blockTimer == 0)
            {
                _player.Unblock(); // сбрасываем флаг блокировки
                _player.ChangeState(new IdleState(_player));
            }
        }

    }
}
