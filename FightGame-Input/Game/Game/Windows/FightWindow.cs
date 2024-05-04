using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using Game;
using Game.Input;
using Game.StateMachine;
using System.Windows.Input;


namespace Game
{
    public class FightWindow : GameWindow
    {
        public double ElapsedTime { get; private set; }
        private Buffer _backgroundBuffer;
        private Texture _backgroundTexture;
        private Buffer _player1Buffer;
        private Texture _player1Texture;
        private Buffer _player2Buffer;
        private Texture _player2Texture;
        private Buffer _textBuffer;
        private Texture _textTexture;
        private Player _player1;
        private Player _player2;
        private PlayerInputHandler _inputHandler1;
        private PlayerInputHandler _inputHandler2;
        //ПРиколы убрать
        private Vector2 _player1Position;
        private Vector2 _player2Position;
        private bool _player1MoveLeft;
        private bool _player1MoveRight;
        private bool _player1Attack;
        private bool _player1Block;

        private bool _player2MoveLeft;
        private bool _player2MoveRight;
        private bool _player2Attack;
        private bool _player2Block;

        public FightWindow() : base(new GameWindowSettings()
        {

        },
        new NativeWindowSettings()
        {
            API = ContextAPI.OpenGL,
            Profile = ContextProfile.Core,
        })
        {
            _player1Position = new Vector2(-0.5f, 0);
            _player2Position = new Vector2(0.5f, 0);
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            _textBuffer = new Buffer(new double[]
            {
            -1.0, -1.0, 0.0, 0.0, 0.0,
             1.0, -1.0, 0.0, 1.0, 0.0,
             1.0,  1.0, 0.0, 1.0, 1.0,
            -1.0,  1.0, 0.0, 0.0, 1.0,
            });

            

            _textTexture = Texture.LoadFromFile(@"Textures\Text.png");

            _player1 = new Player(this, new Vector2(-0.5f, 0), "Player 1");
            _player2 = new Player(this, new Vector2(0.5f, 0), "Player 2");

            _inputHandler1 = new PlayerInputHandler(_player1);
            _inputHandler2 = new PlayerInputHandler(_player2);

            _backgroundBuffer = new Buffer(new double[]
            {
            -1.0, -1.0, 0.0, 0.0, 0.0,
             1.0, -1.0, 0.0, 1.0, 0.0,
             1.0,  1.0, 0.0, 1.0, 1.0,
            -1.0,  1.0, 0.0, 0.0, 1.0,
            -1.0, -1.0, 0.0, 0.0, 0.0,
             1.0,  1.0, 0.0, 1.0, 1.0,
            });
            _backgroundTexture = Texture.LoadFromFile(@"Textures\Background.png");

            double[] player1Vertices = new double[]
            {
            -0.25, -0.25, 0.0, 0.0, 0.0,
             0.25, -0.25, 0.0, 1.0, 0.0,
             0.25,  0.25, 0.0, 1.0, 1.0,
            -0.25,  0.25, 0.0, 0.0, 1.0,
            -0.25, -0.25, 0.0, 0.0, 0.0,
             0.25,  0.25, 0.0, 1.0, 1.0,
            };

            double[] player2Vertices = new double[]
            {
             0.75, -0.25, 0.0, 0.0, 0.0,
             1.25, -0.25, 0.0, 1.0, 0.0,
             1.25,  0.25, 0.0, 1.0, 1.0,
             0.75,  0.25, 0.0, 0.0, 1.0,
             0.75, -0.25, 0.0, 0.0, 0.0,
             1.25,  0.25, 0.0, 1.0, 1.0,
            };

            _player1Buffer = new Buffer(player1Vertices);
            _player2Buffer = new Buffer(player2Vertices);

            _player1Texture = Texture.LoadFromFile(@"Textures\Player1.png");
            _player2Texture = Texture.LoadFromFile(@"Textures\Player2.png");
            _player1.AttackTexture = Texture.LoadFromFile(@"Textures\attack1.png");

            _player1.PlayerBuffer = _player1Buffer;
            _player1.PlayerTexture = _player1Texture;
            _player2.PlayerBuffer = _player2Buffer;
            _player2.PlayerTexture = _player2Texture;
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            
            base.OnUpdateFrame(e);

            var keyboardState = KeyboardState;

            // Игрок 1
            _player1MoveLeft = keyboardState.IsKeyDown(Keys.A);
            _player1MoveRight = keyboardState.IsKeyDown(Keys.D);
            _player1Attack = keyboardState.IsKeyDown(Keys.Space);
            _player1Block = keyboardState.IsKeyDown(Keys.LeftShift);

            // Игрок 2
            _player2MoveLeft = keyboardState.IsKeyDown(Keys.Left);
            _player2MoveRight = keyboardState.IsKeyDown(Keys.Right);
            _player2Attack = keyboardState.IsKeyDown(Keys.Up);
            _player2Block = keyboardState.IsKeyDown(Keys.Down);

            _player1.Update(_player1MoveLeft, _player1MoveRight, _player1Attack, _player1Block);
            _player2.Update(_player2MoveLeft, _player2MoveRight, _player2Attack, _player2Block);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {

            base.OnRenderFrame(e);

            ElapsedTime = e.Time;

            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            // Alpha-channel support
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            _backgroundTexture.Use(TextureUnit.Texture0);
            _backgroundBuffer.Render(_backgroundTexture);

            // Рендеринг состояний игроков
            var player1State = _player1.CurrentState;
            var player2State = _player2.CurrentState;

            // Обновление координат игрока 1
            if (_player1MoveLeft)
            {
                _player1Position.X -= _player1.Speed * (float)ElapsedTime;
            }
            if (_player1MoveRight)
            {
                _player1Position.X += _player1.Speed * (float)ElapsedTime;
            }

            // Обновление координат игрока 2
            if (_player2MoveLeft)
            {
                _player2Position.X -= _player2.Speed * (float)ElapsedTime;
            }
            if (_player2MoveRight)
            {
                _player2Position.X += _player2.Speed * (float)ElapsedTime;
            }

            if (player1State is AttackState)
            {
                _player1.AttackTexture.Use(TextureUnit.Texture0);
                _player1Buffer.Render(_player1.AttackTexture);
            }
            else if (player1State is BlockState)
            {
                // Рендеринг блока игрока 1
            }
            else
            {
                _player1Texture.Use(TextureUnit.Texture0);
                _player1Buffer.Render(_player1Texture);
            }

            if (player2State is AttackState)
            {
                // Рендеринг удара игрока 2
            }
            else if (player2State is BlockState)
            {
                // Рендеринг блока игрока 2
            }
            else
            {
                _player2Texture.Use(TextureUnit.Texture0);
                _player2Buffer.Render(_player2Texture);
            }

            SwapBuffers();
        }

        protected override void OnUnload()
        {

            _backgroundBuffer.Dispose();
            _backgroundTexture.Dispose();
            _player1Buffer.Dispose();
            _player1Texture.Dispose();
            _player2Buffer.Dispose();
            _player2Texture.Dispose();
            _textBuffer.Dispose();
            _textTexture.Dispose();

            base.OnUnload();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Size.X, Size.Y);
        }
    }
}
