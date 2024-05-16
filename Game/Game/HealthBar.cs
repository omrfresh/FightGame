using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;

namespace Game
{
    public class HealthBar
    {
        private Buffer _borderBuffer;
        private Buffer _healthBuffer;
        private Texture _texture;
        private Texture _textureBack;
        private Vector2 _position;
        private Vector2 _size;
        private float _maxHealth;
        private Shader _shader;
        private Vector2 _initialSize;

        public HealthBar(Shader shader, Vector2 position, Vector2 size, float maxHealth)
        {
            _shader = shader;
            _position = position;
            _size = size;
            _initialSize = size;
            _maxHealth = maxHealth;

            // Инициализируем буфер вершин для геометрии границы полосы здоровья (прямоугольник)
            double[] borderVertices = new double[]
            {
            position.X, position.Y, 0.0, 0.0, 0.0,
            position.X + size.X, position.Y, 0.0, 1.0, 0.0,
            position.X + size.X, position.Y + size.Y, 0.0, 1.0, 1.0,
            position.X, position.Y + size.Y, 0.0, 0.0, 1.0,
            position.X, position.Y, 0.0, 0.0, 0.0,
            position.X + size.X, position.Y + size.Y, 0.0, 1.0, 1.0,
            };

            _borderBuffer = new Buffer(borderVertices);

            // Инициализируем буфер вершин для геометрии полосы здоровья (прямоугольник)
            double[] healthVertices = new double[]
            {
            position.X, position.Y, 0.0, 0.0, 0.0,
            position.X + size.X, position.Y, 0.0, 1.0, 0.0,
            position.X + size.X, position.Y + size.Y, 0.0, 1.0, 1.0,
            position.X, position.Y + size.Y, 0.0, 0.0, 1.0,
            position.X, position.Y, 0.0, 0.0, 0.0,
            position.X + size.X, position.Y + size.Y, 0.0, 1.0, 1.0,
            };

            _healthBuffer = new Buffer(healthVertices);

            // Инициализируем текстуру для заливки полосы здоровья (можно использовать любую текстуру, например, зеленый прямоугольник)
            _texture = Texture.LoadFromFile(@"Textures\HealthBar.png");
            _textureBack = Texture.LoadFromFile(@"Textures\Buttonground.png");
        }

        public void Update(float currentHealth)
        {
            // Обновляем размеры закрашенной части полосы здоровья в зависимости от текущего здоровья игрока
            float healthPercentage = currentHealth / _maxHealth;
            _size.X = healthPercentage * _initialSize.X;

            // Обновляем буфер вершин закрашенной части полосы здоровья
            double[] healthVertices = new double[]
            {
                _position.X, _position.Y, 0.0, 0.0, 0.0,
                _position.X + _size.X, _position.Y, 0.0, 1.0, 0.0,
                _position.X + _size.X, _position.Y + _size.Y, 0.0, 1.0, 1.0,
                _position.X, _position.Y + _size.Y, 0.0, 0.0, 1.0,
                _position.X, _position.Y, 0.0, 0.0, 0.0,
                _position.X + _size.X, _position.Y + _size.Y, 0.0, 1.0, 1.0,
            };

            _healthBuffer.UpdateData(healthVertices);
        }
        public void UpdateLeftToRight(float currentHealth)
        {
            // Обновляем размеры закрашенной части полосы здоровья в зависимости от текущего здоровья игрока
            float healthPercentage = currentHealth / _maxHealth;
            _size.X = healthPercentage * _initialSize.X;

            // Обновляем буфер вершин закрашенной части полосы здоровья
            double[] healthVertices = new double[]
            {
                _position.X, _position.Y, 0.0, 0.0, 0.0,
                _position.X + _size.X, _position.Y, 0.0, 1.0, 0.0,
                _position.X + _size.X, _position.Y + _size.Y, 0.0, 1.0, 1.0,
                _position.X, _position.Y + _size.Y, 0.0, 0.0, 1.0,
                _position.X, _position.Y, 0.0, 0.0, 0.0,
                _position.X + _size.X, _position.Y + _size.Y, 0.0, 1.0, 1.0,
            };

            _healthBuffer.UpdateData(healthVertices);
        }
        public void Render()
        {
            // Отрисовываем задний фон полосы здоровья
            _shader.Use();
            _textureBack.Use(TextureUnit.Texture0);
            _borderBuffer.Render(_textureBack);

            // Отрисовываем закрашенную часть полосы здоровья
            _shader.Use();
            _texture.Use(TextureUnit.Texture0);
            _healthBuffer.Render(_texture);
        }

        public void Dispose()
        {
            // Освобождаем ресурсы
            _borderBuffer.Dispose();
            _healthBuffer.Dispose();
            _texture.Dispose();
        }
    }
}