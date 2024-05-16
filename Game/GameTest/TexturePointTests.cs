using Xunit;
using Game;

namespace GameTest
{
    public class TexturePointTests
    {
        [Fact]
        public void TestTexturePointProperties()
        {
            // Arrange
            var texturePoint = new TexturePoint(0.5, 0.5);

            // Act & Assert
            Assert.Equal(0.5, texturePoint.S);
            Assert.Equal(0.5, texturePoint.T);
        }

        [Fact]
        public void TestNewPoint()
        {
            // Arrange
            var texturePoint = new TexturePoint();

            // Act
            texturePoint.NewPoint(0.5, 0.5);

            // Assert
            Assert.Equal(0.5, texturePoint.S);
            Assert.Equal(0.5, texturePoint.T);
        }
        [Fact]
        public void TestToArray()
        {
            // Arrange
            var texturePoint = new TexturePoint(0.5, 0.5);

            // Act
            var array = texturePoint.ToArray();

            // Assert
            Assert.Equal(new double[] { 0.5, 0.5 }, array);
        }
    }
}
