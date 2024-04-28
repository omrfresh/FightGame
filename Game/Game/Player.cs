using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.StateMachine;

namespace Game
{
    public class Player
    {
        public PlayerState CurrentState { get; private set; }

        public Player()
        {
            CurrentState = new IdleState(this);
        }

        public void ChangeState(PlayerState newState)
        {
            CurrentState = newState;
        }

        public void HandleInput()
        {
            CurrentState.HandleInput();
        }

        public void Update()
        {
            CurrentState.Update();
        }
    }
}
