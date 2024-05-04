using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;

namespace Game
{
    public class HealthBar
    {
        private Texture _texture;
        private double[] _vertices;
        private double _width;
        private double _height;
        private Vector2 _position;
        private double _currentHealth;
        private double _maxHealth;

        public HealthBar(Texture texture, double width, double height, Vector2 position, double maxHealth)
        {
            _texture = texture;
            _width = width;
            _height = height;
            _position = position;
            _maxHealth = maxHealth;
            _currentHealth = maxHealth;

            _vertices = new double[]
            {
            position.X, position.Y, 0.0, 0.0, 0.0,
            position.X + width, position.Y, 0.0, 1.0, 0.0,
            position.X + width, position.Y + height, 0.0, 1.0, 1.0,
            position.X, position.Y + height, 0.0, 0.0, 1.0,
            position.X, position.Y, 0.0, 0.0, 0.0,
            position.X + width, position.Y + height, 0.0, 1.0, 1.0,
            };
        }

        public void Update(double currentHealth)
        {
            _currentHealth = currentHealth;
            double ratio = _currentHealth / _maxHealth;
            double newWidth = _width * ratio;
            _vertices[1] = _position.X + newWidth;
            _vertices[6] = _position.X + newWidth;
            _vertices[11] = _position.X + newWidth;
        }

        public void Render()
        {
            Buffer buffer = new Buffer(_vertices);
            _texture.Use((TextureUnit)TextureUnit.Texture0);
            buffer.Render(_texture);
        }
    }

}
