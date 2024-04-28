using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.StateMachine
{
    public class AttackState : PlayerState
    {
        public AttackState(Player player) : base(player) { }

        public override void HandleInput()
        {
            // Обработать ввод для перехода в другие состояния
        }

        public override void Update()
        {
            // Обновить логику состояния атаки
        }
    }
}
