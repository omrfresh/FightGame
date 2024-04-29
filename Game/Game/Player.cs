using Game.StateMachine;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;

namespace Game
{
    public class Player
    {
        public Vector2 Position { get; set; }
        public float Health { get; set; }
        public float Damage { get; set; }
        public float AttackRange { get; set; }
        public float Speed { get; set; }

        private IState _currentState;
        public FightWindow gameWindow;


        public Player(FightWindow gameWindow)
        {
            Position = new Vector2(0, 0);
            Health = 100;
            Damage = 10;
            AttackRange = 1;
            Speed = 2;

            _currentState = new IdleState(this);
            
        }

        public void ChangeState(IState newState)
        {
            _currentState?.Exit(this);
            _currentState = newState;
            _currentState.Enter(this);
        }

        public void Update()
        {
            _currentState.Update(this);
        }
    }

}