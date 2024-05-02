using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using OpenTK.Mathematics;
using Game;
using Game.StateMachine;

namespace Game.Input
{
    public class PlayerInputHandler : IInputHandler
    {
        private Player _player;

        public PlayerInputHandler(Player player)
        {
            _player = player;
        }

        public void HandleInput(KeyboardState keyboardState)
        {
            if (_player.Name == "Player1")
            {
                if (keyboardState.IsKeyDown(Keys.F))
                {
                    _player.Attack();
                }
                if (keyboardState.IsKeyDown(Keys.A))
                {
                    _player.ChangeState(new MoveState(_player, new Vector2(-1, 0)));
                }
                if (keyboardState.IsKeyDown(Keys.D))
                {
                    _player.ChangeState(new MoveState(_player, new Vector2(1, 0)));
                }
                if (keyboardState.IsKeyDown(Keys.S)) // Клавиша блокировки для первого игрока
                {
                    _player.ChangeState(new BlockState(_player));
                }
            }
            else if (_player.Name == "Player2")
            {
                if (keyboardState.IsKeyDown(Keys.LeftShift))
                {
                    _player.Attack();
                }
                if (keyboardState.IsKeyDown(Keys.Left))
                {
                    _player.ChangeState(new MoveState(_player, new Vector2(-1, 0)));
                }
                if (keyboardState.IsKeyDown(Keys.Right))
                {
                    _player.ChangeState(new MoveState(_player, new Vector2(1, 0)));
                }
                if (keyboardState.IsKeyDown(Keys.Down)) // Клавиша блокировки для второго игрока
                {
                    _player.ChangeState(new BlockState(_player));
                }
            }
        }
    }
}
