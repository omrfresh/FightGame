﻿using Game.StateMachine;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.IO;
using System.Windows.Controls;


namespace Game
{
    public class FightWindow : GameWindow
    {
        public double ElapsedTime { get; private set; }
        private MainWindow _mainWindow;
        public int TexHelper;
        private System.Timers.Timer closeTimer;
        //BackGround
        private Buffer _backgroundBuffer;
        private Texture _backgroundTexture;
        //Player1
        private Player _player1;
        private Buffer _player1Buffer;
        private Texture _player1Texture;
        private Vector2 _player1Position;
        private PlayerController _player1Controller;
        //Player2
        private Player _player2;
        private Buffer _player2Buffer;
        private Texture _player2Texture;
        private Vector2 _player2Position;
        private PlayerController _player2Controller;
        //HealthBar
        private HealthBar _player1HealthBar;
        private HealthBar _player2HealthBar;
        private Shader _shader;
        public FightWindow() : base(new GameWindowSettings()
        {

        },
        new NativeWindowSettings()
        {
            API = ContextAPI.OpenGL,
            Profile = ContextProfile.Core,
            WindowBorder = WindowBorder.Hidden,
            WindowState = WindowState.Maximized,
        })
        {
            _player1Position = new Vector2(-0.5f, -0.5f);
            _player2Position = new Vector2(0.5f, -0.5f);
            _player1 = new Player(this, new Vector2(-0.5f, -0.5f), "Player 1");
            _player2 = new Player(this, new Vector2(0.5f, -0.5f), "Player 2");
            _player1.Opponent = _player2;
            _player2.Opponent = _player1;
            _player1Controller = new PlayerController(_player1);
            _player2Controller = new PlayerController(_player2);
        }

        protected override void OnLoad()
        {
            base.OnLoad();

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
           
            _player1Texture = Texture.LoadFromFile(@"Textures\RedPlayer\Idle.png");
            _player2Texture = Texture.LoadFromFile(@"Textures\BluePlayer\Idle.png");

            _player1.PlayerBuffer = _player1Buffer;
            _player1.PlayerTexture = _player1Texture;
            _player2.PlayerBuffer = _player2Buffer;
            _player2.PlayerTexture = _player2Texture;

            _shader = new Shader(@"Shaders\shader.vert", @"Shaders\shader.frag");
            _player1HealthBar = new HealthBar(_shader, new Vector2(-1.0f, 0.9f), new Vector2(0.5f, 0.1f), _player1.Health);
            _player2HealthBar = new HealthBar(_shader, new Vector2(0.5f, 0.9f), new Vector2(0.5f, 0.1f), _player2.Health);



        }
        /// <summary>
        /// Метод для обработки действий персонажа и обновления его состояния(Для обоих игроков)
        /// </summary>
        /// <param name="e"></param>
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            var keyboardState = KeyboardState;
            // Игрок 1
            _player1Controller.Update(keyboardState.IsKeyDown(Keys.A), keyboardState.IsKeyDown(Keys.D), keyboardState.IsKeyDown(Keys.Space), keyboardState.IsKeyDown(Keys.Q), keyboardState.IsKeyDown(Keys.E), keyboardState.IsKeyDown(Keys.LeftShift));
            // Игрок 2
            _player2Controller.Update(keyboardState.IsKeyDown(Keys.Left), keyboardState.IsKeyDown(Keys.Right), keyboardState.IsKeyDown(Keys.Up), keyboardState.IsKeyDown(Keys.K), keyboardState.IsKeyDown(Keys.L), keyboardState.IsKeyDown(Keys.Down));
            //Обновлдение буфера 
            _player1.UpdateBuffer();
            _player2.UpdateBuffer();

            _player1HealthBar.Update(_player1.Health);
            _player2HealthBar.UpdateLeftToRight(_player2.Health);

            if (_player1.BoundingBox.IsCollidingWith(_player2.BoundingBox))
            {
                // Обработка столкновений
                HandleCollision(_player1, _player2);
            }

            if (_player1.CurrentState is DeadState || _player2.CurrentState is DeadState)
            {
                //Close();
                return;
            }

        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {

            // Выводим в файл текущее время, сообщение и значение _player2.Health
            using (StreamWriter sw = new StreamWriter("debug1.log", true))
            {
                sw.WriteLine($"{DateTime.Now}: FightWindow.OnRenderFrame() called, _player2.Health = {_player2.Health}");
            }
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
            var player1State = _player1Controller.CurrentState;
            var player2State = _player2Controller.CurrentState;

            _player1HealthBar.Render();
            _player2HealthBar.Render();

            if (player1State is AttackState attackState1)
            {
                //Это тема с созданием нескольких текстур и их использованием(Память нормально используется, но есть приколы в Player, нужно определить в нём дохуя(5+-) texture, в целом годно) 
                //_player1.AttackTexture.Use(TextureUnit.Texture0);
                //_player1Buffer.Render(_player1.AttackTexture);

                //Это тема с загрузкой в одну claaassss Texture разных png, посмотреть что с памятью, жрёт только так, попробовать очищать(фиксится, прикольно :d)
                if (attackState1.Type == AttackType.Hand)
                {
                    _player1Texture = Texture.LoadFromFile(@"Textures\RedPlayer\Punch.png");
                }
                if (attackState1.Type == AttackType.Leg)
                {
                    _player2Texture = Texture.LoadFromFile(@"Textures\RedPlayer\KomboPunch.png");
                }
                if (attackState1.Type == AttackType.Combo)
                {
                    _player2Texture = Texture.LoadFromFile(@"Textures\RedPlayer\LegPunch.png");
                }
                _player1Texture.Use(TextureUnit.Texture0);
                _player1Buffer.Render(_player1Texture);
                _player1Texture.Dispose();
            }
            else if (player1State is BlockState)
            {
                _player1Texture = Texture.LoadFromFile(@"Textures\RedPlayer\Block.png");
                _player1Texture.Use(TextureUnit.Texture0);
                _player1Buffer.Render(_player1Texture);
                _player1Texture.Dispose();
            }
            else if (player1State is MoveState)
            {
                _player1Texture = Texture.LoadFromFile(@"Textures\RedPlayer\Run.png");
                _player1Texture.Use(TextureUnit.Texture0);
                _player1Buffer.Render(_player1Texture);
                _player1Texture.Dispose();
            }
            else if (player1State is DeadState)
            {
                _player1Texture = Texture.LoadFromFile(@"Textures\RedPlayer\Death.png");
                _player1Texture.Use(TextureUnit.Texture0);
                _player1Buffer.Render(_player1Texture);
                _player1Texture.Dispose();
            }
            else
            {
                _player1Texture = Texture.LoadFromFile(@"Textures\RedPlayer\Idle.png");
                _player1Texture.Use(TextureUnit.Texture0);
                _player1Buffer.Render(_player1Texture);
                _player1Texture.Dispose();
            }

            
            if (player2State is AttackState attackState2)
            {
                if (attackState2.Type == AttackType.Hand)
                {
                    _player2Texture = Texture.LoadFromFile(@"Textures\BluePlayer\Punch.png");
                }
                if (attackState2.Type == AttackType.Leg)
                {
                    _player2Texture = Texture.LoadFromFile(@"Textures\BluePlayer\LegPunch.png");
                }
                if (attackState2.Type == AttackType.Combo)
                {
                    _player2Texture = Texture.LoadFromFile(@"Textures\BluePlayer\KomboPunch.png");
                }
                _player2Texture.Use(TextureUnit.Texture0);
                _player2Buffer.Render(_player2Texture);
                _player2Texture.Dispose();
               
            }
            else if (player2State is BlockState)
            {
                _player2Texture = Texture.LoadFromFile(@"Textures\BluePlayer\Block.png");
                _player2Texture.Use(TextureUnit.Texture0);
                _player2Buffer.Render(_player2Texture);
                _player2Texture.Dispose();
            }
            else if (player2State is MoveState)
            {
                _player2Texture = Texture.LoadFromFile(@"Textures\BluePlayer\Run.png");
                _player2Texture.Use(TextureUnit.Texture0);
                _player2Buffer.Render(_player1Texture);
                _player2Texture.Dispose();
            }
            else if (player2State is DeadState)
            {
                _player2Texture = Texture.LoadFromFile(@"Textures\BluePlayer\Death.png");
                _player2Texture.Use(TextureUnit.Texture0);
                _player2Buffer.Render(_player2Texture);
                _player2Texture.Dispose();
            }
            else
            {
                _player2Texture = Texture.LoadFromFile(@"Textures\BluePlayer\Idle.png");
                _player2Texture.Use(TextureUnit.Texture0);
                _player2Buffer.Render(_player2Texture);
                _player2Texture.Dispose();
            }
            SwapBuffers();
            // Проверка на смерть игроков
            if (_player1.CurrentState is DeadState && _player2.CurrentState is not DeadState)
            {
                //Dispose();
                // Вызов FinishWindow с текстом "Игрок 2 победил!"
                var finishWindow = new FinishWindow("Игрок 2 победил!", this, _mainWindow);
                finishWindow.ShowDialog();
                // Завершение игры
            }
            else if (_player2.CurrentState is DeadState && _player1.CurrentState is not DeadState)
            {
                //Dispose();
                // Вызов FinishWindow с текстом "Игрок 1 победил!"
                var finishWindow = new FinishWindow("Игрок 1 победил!", this, _mainWindow);
                finishWindow.ShowDialog();
                // Завершение игры
            }
        }

        protected override void OnUnload()
        {
            base.OnUnload();

            //Goood
            _backgroundBuffer.Dispose();
            _backgroundTexture.Dispose();

            //Interesting
            _player1Buffer.Dispose();
            _player1Texture.Dispose();
            _player2Buffer.Dispose();
            _player2Texture.Dispose();

            _player1HealthBar.Dispose();
            _player2HealthBar.Dispose();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Size.X, Size.Y);
        }
        private void HandleCollision(Player player1, Player player2)
        {
            // Предотвращение прохождения друг через друга
            // Вычисление направления столкновения
            Vector2 direction = Vector2.Zero;
            if (player1.Position.X < player2.Position.X)
            {
                direction.X = 1;
            }
            else if (player1.Position.X > player2.Position.X)
            {
                direction.X = -1;
            }

            if (player1.Position.Y < player2.Position.Y)
            {
                direction.Y = 1;
            }
            else if (player1.Position.Y > player2.Position.Y)
            {
                direction.Y = -1;
            }

            // Разделение пересекающихся игроков
            float overlapX = Math.Abs((player1.Position.X + player1.BoundingBox.Size.X / 2) - (player2.Position.X + player2.BoundingBox.Size.X / 2)) - (player1.BoundingBox.Size.X + player2.BoundingBox.Size.X) / 2;
            float overlapY = Math.Abs((player1.Position.Y + player1.BoundingBox.Size.Y / 2) - (player2.Position.Y + player2.BoundingBox.Size.Y / 2)) - (player1.BoundingBox.Size.Y + player2.BoundingBox.Size.Y) / 2;

            // Отладка
            string logMessage = $"HandleCollision called. Player1: ({player1.Position.X}, {player1.Position.Y}), Player2: ({player2.Position.X}, {player2.Position.Y}), OverlapX: {overlapX}, OverlapY: {overlapY}";
            System.IO.File.AppendAllText("collision_log.txt", logMessage + Environment.NewLine);

            if (overlapX > 0 || overlapY > 0)
            {
                // Смещение позиции игроков назад в зависимости от скорости и направления движения
                float speedFactor = Math.Min(player1.Speed, player2.Speed);
                Vector2 offset = direction * new Vector2(overlapX + speedFactor, overlapY + speedFactor);
                player1.Position -= offset;
                player2.Position += offset;
            }
        }
        public void Reset()
        {
            _player1.Reset();
            _player2.Reset();

            _player1Position = new Vector2(-0.5f, -0.5f);
            _player2Position = new Vector2(0.5f, -0.5f);

            _player1.Position = _player1Position;
            _player2.Position = _player2Position;

            _player1HealthBar.Update(_player1.Health);
            _player2HealthBar.UpdateLeftToRight(_player2.Health);
        }
    }
}
