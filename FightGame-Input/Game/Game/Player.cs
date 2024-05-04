using Game.StateMachine;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;

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
        public bool IsBlocked { get; private set; }
        public Buffer PlayerBuffer { get; set; }
        public Texture PlayerTexture { get; set; }
        public Texture AttackTexture { get; set; }

        private IState _currentState;
        public FightWindow gameWindow { get; private set; }
        public IState CurrentState => _currentState;

        public Player(FightWindow gameWindow, Vector2 position, string name)
        {
            PlayerBuffer = null;
            PlayerTexture = null;
            Position = position;
            Name = name;
            Health = 100;
            Damage = 10;
            AttackRange = 10000;
            Speed = 1.15f;
            this.gameWindow = gameWindow;
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
            if (_currentState is AttackState)
            {
                PlayerTexture = AttackTexture;
            }
            else
            {
                PlayerTexture = PlayerTexture;
            }
            _currentState.Update(this);
            UpdateBuffer();
        }
        public void Attack()
        {
            ChangeState(new AttackState(this));
        }
        public IState GetCurrentState()
        {
            return _currentState;
        }
        public void Block()
        {
            IsBlocked = true;
        }

        public void Unblock()
        {
            IsBlocked = false;
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
        public void UpdateTextureCoordinates()
        {
            double[] textureCoordinates = new double[]
            {
              Position.X - 0.25, Position.Y + 0.25,
              Position.X + 0.25, Position.Y + 0.25,
              Position.X + 0.25, Position.Y - 0.25,
              Position.X - 0.25, Position.Y - 0.25,
            };

            PlayerBuffer.UpdateData(textureCoordinates, 2 * sizeof(double));
        }
        public void Update(bool moveLeft, bool moveRight, bool attack, bool block)
        {
            if (moveLeft && !moveRight)
            {
                ChangeState(new MoveState(this, new Vector2(-Speed, 0)));
            }
            else if (moveRight && !moveLeft)
            {
                ChangeState(new MoveState(this, new Vector2(Speed, 0)));
            }
            else if (attack)
            {
                ChangeState(new AttackState(this));
            }
            else if (block)
            {
                ChangeState(new BlockState(this));
            }
            else
            {
                ChangeState(new IdleState(this));
            }

            _currentState.Update(this);
        }
    }
}