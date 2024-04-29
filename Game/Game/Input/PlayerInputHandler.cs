using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game;

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
            if (keyboardState.IsKeyDown(Keys.F))
            {
                _player.Attack();
            }
        }
    }

}
