using Game;
using OpenTK.Mathematics;
using Xunit;

namespace GameTest
{
    public class GameEngineTests
    {
        [Fact]
        public void TestAddPlayer()
        {
            // Arrange
            var player = new Player(null, new Vector2(0, 0), "TestPlayer");

            // Act
            GameEngine.AddPlayer(player);

            // Assert
            Assert.Equal(1, GameEngine.GetPlayerCount());
        }

        [Fact]
        public void TestIsInRange()
        {
            // Arrange
            var player1 = new Player(null, new Vector2(0, 0), "TestPlayer1");
            var player2 = new Player(null, new Vector2(10, 0), "TestPlayer2");

            // Act & Assert
            Assert.True(GameEngine.IsInRange(player1, player2, 11));
            Assert.False(GameEngine.IsInRange(player1, player2, 9));
        }
    }
}
