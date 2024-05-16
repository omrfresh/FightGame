using Xunit;
using OpenTK.Mathematics;
using Game;

namespace GameTest
{
    public class BoundingBoxTests
    {
        [Fact]
        public void TestIsCollidingWith()
        {
            // Arrange
            var box1 = new BoundingBox(new Vector2(10, 20), new Vector2(50, 50));
            var box2 = new BoundingBox(new Vector2(40, 40), new Vector2(50, 50));
            var box3 = new BoundingBox(new Vector2(100, 100), new Vector2(50, 50));

            // Act & Assert
            Assert.True(box1.IsCollidingWith(box2));
            Assert.False(box1.IsCollidingWith(box3));
            Assert.False(box2.IsCollidingWith(box3));
        }
    }
}
