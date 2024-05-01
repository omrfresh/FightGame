using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using Game;
using Game.Input;
using Game.StateMachine;

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
        private IInputHandler _inputHandler1;
        private IInputHandler _inputHandler2;

        public FightWindow() : base(new GameWindowSettings()
        {

        },
        new NativeWindowSettings()
        {
            API = ContextAPI.OpenGL,
            Profile = ContextProfile.Core,
        })
        {
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

            _player1.PlayerBuffer = _player1Buffer;
            _player1.PlayerTexture = _player1Texture;
            _player2.PlayerBuffer = _player2Buffer;
            _player2.PlayerTexture = _player2Texture;
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            var keyboardState = KeyboardState;

            _inputHandler1.HandleInput(keyboardState);
            _inputHandler2.HandleInput(keyboardState);

            _player1.Update();
            _player2.Update();
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

            _player1Texture.Use(TextureUnit.Texture0);
            _player1Buffer.Render(_player1Texture, _player1.Position);

            _player2Texture.Use(TextureUnit.Texture0);
            _player2Buffer.Render(_player2Texture, _player2.Position);

            var player1State = _player1.GetCurrentState() as AttackState;
            var player2State = _player2.GetCurrentState() as AttackState;

            if (player1State != null && player1State.IsOpponentKilled)
            {
                _textTexture.Use(TextureUnit.Texture0);
                _textBuffer.Render(_textTexture);
            }
            else if (player2State != null && player2State.IsOpponentKilled)
            {
                _textTexture.Use(TextureUnit.Texture0);
                _textBuffer.Render(_textTexture);
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
