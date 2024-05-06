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
            // Здесь можно добавить любую логику, которая должна выполняться при входе в состояние "мертв"
        }

        public void Exit(Player player)
        {
            // Здесь можно добавить любую логику, которая должна выполняться при выходе из состояния "мертв"
        }

        public void Update(Player player)
        {
            // Здесь можно добавить любую логику, которая должна выполняться каждый кадр, пока игрок мертв
        }
    }
}
