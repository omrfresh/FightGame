using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.StateMachine
{
    public class AttackState : IState
    {
        public void Enter(Player player)
        {
            // Код для перехода в состояние "ожидание"
        }

        public void Exit(Player player)
        {
            // Код для выхода из состояния "ожидание"
        }

        public void Update(Player player)
        {
            // Обновление логики для состояния "ожидание"
        }
    }
}
