using Xunit;
using Game;

namespace GameTest
{
    public class RectangleTests
    {
        [Fact]
        public void TestRectangleProperties()
        {
            // Arrange
            var topLeft = new Point(1.0, 2.0, 3.0);
            var topRight = new Point(4.0, 5.0, 6.0);
            var bottomRight = new Point(7.0, 8.0, 9.0);
            var bottomLeft = new Point(10.0, 11.0, 12.0);
            var rectangle = new Rectangle(topLeft, topRight, bottomRight, bottomLeft);

            // Act & Assert
            Assert.Equal(topLeft, rectangle.TopLeft);
            Assert.Equal(topRight, rectangle.TopRight);
            Assert.Equal(bottomRight, rectangle.BottomRight);
            Assert.Equal(bottomLeft, rectangle.BottomLeft);
            Assert.Equal(new Point[] { topLeft, topRight, bottomRight, bottomLeft }, rectangle.Points);
        }

        [Fact]
        public void TestGetWidth()
        {
            // Arrange
            var topLeft = new Point(1.0, 2.0, 3.0);
            var topRight = new Point(4.0, 5.0, 6.0);
            var bottomRight = new Point(7.0, 8.0, 9.0);
            var bottomLeft = new Point(10.0, 11.0, 12.0);
            var rectangle = new Rectangle(topLeft, topRight, bottomRight, bottomLeft);

            // Act & Assert
            Assert.Equal(3.0, rectangle.GetWidth());
        }

        [Fact]
        public void TestGetHeight()
        {
            // Arrange
            var topLeft = new Point(1.0, 2.0, 3.0);
            var topRight = new Point(4.0, 5.0, 6.0);
            var bottomRight = new Point(7.0, 8.0, 9.0);
            var bottomLeft = new Point(10.0, 11.0, 12.0);
            var rectangle = new Rectangle(topLeft, topRight, bottomRight, bottomLeft);

            // Act & Assert
            Assert.Equal(9.0, rectangle.GetHeight());
        }

        [Fact]
        public void TestToArray()
        {
            // Arrange
            var topLeft = new Point(1.0, 2.0, 3.0);
            var topRight = new Point(4.0, 5.0, 6.0);
            var bottomRight = new Point(7.0, 8.0, 9.0);
            var bottomLeft = new Point(10.0, 11.0, 12.0);
            var rectangle = new Rectangle(topLeft, topRight, bottomRight, bottomLeft);

            // Act
            var array = rectangle.ToArray();

            // Assert
            Assert.Equal(new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, 8.0, 9.0, 10.0, 11.0, 12.0 }, array);
        }

        [Fact]
        public void TestIntersects()
        {
            // Arrange
            var rectangle1 = new Rectangle(new Point(1.0, 2.0, 3.0), 4.0, 5.0);
            var rectangle2 = new Rectangle(new Point(3.0, 4.0, 3.0), 4.0, 5.0);
            var rectangle3 = new Rectangle(new Point(6.0, 7.0, 3.0), 4.0, 5.0);

            // Act & Assert
            Assert.True(rectangle1.Intersects(rectangle2));
            Assert.False(rectangle1.Intersects(rectangle3));
            Assert.True(rectangle2.Intersects(rectangle3));
        }
    }
}
