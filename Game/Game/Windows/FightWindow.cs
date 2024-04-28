using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using Game;

namespace Game
{
    public class FightWindow : GameWindow
    {
        private double _elapsedTime;
        public static double ElapsedTime => _elapsedTime;
        private Buffer _buffer;
        private Texture _texture;
        private TextureMap _textureMap;
        private Buffer _playerBuffer;
        private Texture _backgroundTexture;
        private Buffer _backgroundBuffer;

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

            double[] backgroundVertices = new double[]
            {
                -1.0, -1.0, 0.0, 0.0, 0.0,
                 1.0, -1.0, 0.0, 1.0, 0.0,
                 1.0,  1.0, 0.0, 1.0, 1.0,
                -1.0,  1.0, 0.0, 0.0, 1.0,
                -1.0, -1.0, 0.0, 0.0, 0.0,
                 1.0,  1.0, 0.0, 1.0, 1.0,
            };

            _backgroundBuffer = new Buffer(backgroundVertices);

            _backgroundTexture = Texture.LoadFromFile(@"Textures\Background.png");

            _texture = Texture.LoadFromFile(@"Textures\texMap.png");

            _textureMap = new TextureMap(8, 8, 4, _texture);

            TexturePoint[] texturePoints = _textureMap.GetTexturePoints((int)Resources.PlayerDefault);

            double[] playerVertices = new double[]
            {
                -0.25, -0.25, 0.0, texturePoints[0].S, texturePoints[0].T,
                 0.25, -0.25, 0.0, texturePoints[1].S, texturePoints[1].T,
                 0.25,  0.25, 0.0, texturePoints[2].S, texturePoints[2].T,
                -0.25,  0.25, 0.0, texturePoints[3].S, texturePoints[3].T,
                -0.25, -0.25, 0.0, texturePoints[0].S, texturePoints[0].T,
                 0.25,  0.25, 0.0, texturePoints[2].S, texturePoints[2].T,
            };
            _playerBuffer = new Buffer(playerVertices);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            _elapsedTime = e.Time;

            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            // Alpha-chanal support
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            _backgroundTexture.Use(TextureUnit.Texture0);
            _backgroundBuffer.Render(_backgroundTexture);

            _texture.Use(TextureUnit.Texture0);
            _playerBuffer.Render(_texture);

            SwapBuffers();
        }


        protected override void OnUnload()
        {
            _buffer.Dispose();
            _texture.Dispose();
            _textureMap = null;
            _playerBuffer.Dispose();
            _backgroundTexture.Dispose();
            _backgroundBuffer.Dispose();

            base.OnUnload();
        }
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Size.X, Size.Y);
        }

    }
}
