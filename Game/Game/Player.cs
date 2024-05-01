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
        public string Name { get; set; }
        public Buffer PlayerBuffer { get; set; }
        public Texture PlayerTexture { get; set; }

        private IState _currentState;
        public FightWindow gameWindow;


        public Player(FightWindow gameWindow, Vector2 position, string name)
        {
            PlayerBuffer = null;
            PlayerTexture = null;
            Position = position;
            Name = name;
            Health = 100;
            Damage = 10;
            AttackRange = 1000;
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
        public void Attack()
        {
            ChangeState(new AttackState(this));
        }
        public IState GetCurrentState()
        {
            return _currentState;
        }
    }

}