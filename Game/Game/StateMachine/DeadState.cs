using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.StateMachine
{
    public class DeadState : IState
    {
        private Player _player;

        public DeadState(Player player)
        {
            _player = player;
        }

        public void Enter(Player player)
        {
            // Нет действий в состояние "мертв"
        }

        public void Exit(Player player)
        {
            // Нет действий в состояние "мертв"
        }

        public void Update(Player player)
        {
            // Нет действий в состояние "мертв"
        }
    }
}
