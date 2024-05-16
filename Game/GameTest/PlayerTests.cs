using Moq;
using Game;
using Xunit;
using OpenTK.Mathematics;
using Game.StateMachine;

namespace GameTest
{
    public class PlayerTests
    {
        [Fact]
        public void TestPlayerHealth()
        {
            // Arrange
            var gameWindow = new Mock<FightWindow>();
            var player = new Player(null, new Vector2(0, 0), "TestPlayer");

            // Act
            player.Health = 50;

            // Assert
            Assert.Equal(50, player.Health);
        }

        [Fact]
        public void TestPlayerDamage()
        {
            // Arrange
            var gameWindow = new Mock<FightWindow>();
            var player = new Player(null, new Vector2(0, 0), "TestPlayer");

            // Act
            player.Damage = 10;

            // Assert
            Assert.Equal(10, player.Damage);
        }

        [Fact]
        public void TestPlayerPosition()
        {
            // Arrange
            var gameWindow = new Mock<FightWindow>();
            var player = new Player(null, new Vector2(0, 0), "TestPlayer");

            // Act
            player.Position = new Vector2(10, 20);

            // Assert
            Assert.Equal(new Vector2(10, 20), player.Position);
        }

        [Fact]
        public void TestPlayerBlock()
        {
            // Arrange
            var gameWindow = new Mock<FightWindow>();
            var player = new Player(null, new Vector2(0, 0), "TestPlayer");

            // Act
            player.Block();

            // Assert
            Assert.True(player.IsBlocking);
        }

        [Fact]
        public void TestPlayerUnblock()
        {
            // Arrange
            var gameWindow = new Mock<FightWindow>();
            var player = new Player(null, new Vector2(0, 0), "TestPlayer");

            // Act
            player.Block();
            player.Unblock();

            // Assert
            Assert.False(player.IsBlocking);
        }
        [Fact]
        public void TestPlayerName()
        {
            // Arrange
            var gameWindow = new Mock<FightWindow>();
            var player = new Player(null, new Vector2(0, 0), "TestPlayer");

            // Act
            string name = player.Name;

            // Assert
            Assert.Equal("TestPlayer", name);
        }

        [Fact]
        public void TestPlayerSpeed()
        {
            // Arrange
            var gameWindow = new Mock<FightWindow>();
            var player = new Player(null, new Vector2(0, 0), "TestPlayer");

            // Act
            player.Speed = 5;

            // Assert
            Assert.Equal(5, player.Speed);
        }

        [Fact]
        public void TestPlayerAttackRange()
        {
            // Arrange
            var gameWindow = new Mock<FightWindow>();
            var player = new Player(null, new Vector2(0, 0), "TestPlayer");

            // Act
            player.AttackRange = 2.5f;

            // Assert
            Assert.Equal(2.5f, player.AttackRange);
        }

        [Fact]
        public void TestPlayerOpponent()
        {
            // Arrange
            var gameWindow = new Mock<FightWindow>();
            var player = new Player(null, new Vector2(0, 0), "TestPlayer");
            var opponent = new Player(null, new Vector2(0, 0), "TestOpponent");

            // Act
            player.Opponent = opponent;

            // Assert
            Assert.Equal(opponent, player.Opponent);
        }

        [Fact]
        public void TestPlayerState()
        {
            // Arrange
            var gameWindow = new Mock<FightWindow>();
            var player = new Player(null, new Vector2(0, 0), "TestPlayer");
            var state = new Mock<IState>();

            // Act
            player.ChangeState(state.Object);

            // Assert
            Assert.Equal(state.Object, player.CurrentState);
        }
    }
}
