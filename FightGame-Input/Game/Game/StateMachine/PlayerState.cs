using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Game;

namespace Game.StateMachine
{
    public abstract class PlayerState
    {
        protected Player _player;

        public PlayerState(Player player)
        {
            _player = player;
        }

        public abstract void HandleInput();
        public abstract void Update();
    }

}
