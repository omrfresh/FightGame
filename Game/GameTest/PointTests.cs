using Xunit;
using Game;

namespace GameTest
{
    public class PointTests
    {
        [Fact]
        public void TestPointProperties()
        {
            // Arrange
            var point = new Point(1.0, 2.0, 3.0);

            // Act & Assert
            Assert.Equal(1.0, point.X);
            Assert.Equal(2.0, point.Y);
            Assert.Equal(3.0, point.Z);
        }

        [Fact]
        public void TestNewPoint()
        {
            // Arrange
            var point = new Point();

            // Act
            point.NewPoint(1.0, 2.0, 3.0);

            // Assert
            Assert.Equal(1.0, point.X);
            Assert.Equal(2.0, point.Y);
            Assert.Equal(3.0, point.Z);
        }

        [Fact]
        public void TestToArray()
        {
            // Arrange
            var point = new Point(1.0, 2.0, 3.0);

            // Act
            var array = point.ToArray();

            // Assert
            Assert.Equal(new double[] { 1.0, 2.0, 3.0 }, array);
        }

        [Fact]
        public void TestClone()
        {
            // Arrange
            var point = new Point(1.0, 2.0, 3.0);

            // Act
            var clone = (Point)point.Clone();

            // Assert
            Assert.Equal(point.X, clone.X);
            Assert.Equal(point.Y, clone.Y);
            Assert.Equal(point.Z, clone.Z);
        }
    }
}