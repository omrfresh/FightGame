using Game.StateMachine;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using Game;
using System.ComponentModel;

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
        public bool IsBlocking { get; private set; }
        public float LastAttackTime { get; private set; } = 0f;
        public Buffer PlayerBuffer { get; set; }
        public Texture PlayerTexture { get; set; }
        public FightWindow gameWindow { get; private set; }
        public Player Opponent { get; set; }
        private IState _currentState;
        public IState CurrentState => _currentState;
        public BoundingBox BoundingBox { get; private set; }

        //Конструктор
        public Player(FightWindow gameWindow, Vector2 position, string name)
        {
            PlayerBuffer = null;
            PlayerTexture = null;
            Position = position;
            Name = name;
            Health = 100;
            Damage = 0.5f;
            AttackRange = 0.55f;
            Speed = 1.15f;
            this.gameWindow = gameWindow;
            _currentState = new IdleState(this);
            BoundingBox = new BoundingBox(position, new Vector2(0.5f, 0.5f));

        }

        //Методы
        public void ChangeState(IState newState)
        {
            _currentState?.Exit(this);
            _currentState = newState;
            _currentState.Enter(this);
        }
        public void Update()
        {
            BoundingBox.Position = Position;
            PlayerTexture = PlayerTexture;
            _currentState.Update(this);
            UpdateBuffer();
        }
        public void Block()
        {
            IsBlocking = true;
        }

        public void Unblock()
        {
            IsBlocking = false;
        }

        public void UpdateBuffer()
        {
            double[] playerVertices = new double[]
            {
            Position.X - 0.25, Position.Y - 0.25, 0.0, 0.0, 0.0,
            Position.X + 0.25, Position.Y - 0.25, 0.0, 1.0, 0.0,
            Position.X + 0.25, Position.Y + 0.25, 0.0, 1.0, 1.0,
            Position.X - 0.25, Position.Y + 0.25, 0.0, 0.0, 1.0,
            Position.X - 0.25, Position.Y - 0.25, 0.0, 0.0, 0.0,
            Position.X + 0.25, Position.Y + 0.25, 0.0, 1.0, 1.0,
            };

            PlayerBuffer.UpdateData(playerVertices);
        }
        public void Reset()
        {
            Health = 100;
            _currentState = new IdleState(this);
        }

    }
}