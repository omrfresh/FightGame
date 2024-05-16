using Xunit;
using Game;
using Game.StateMachine;
using OpenTK.Mathematics;
using Moq;

namespace GameTest
{
    public class AttackStateTests
    {
        [Fact]
        public void TestEnter()
        {
            // Arrange
            var mockPlayer = new Player(null, new Vector2(0, 0), "TestPlayer");
            ;
            var attackState = new AttackState(mockPlayer, AttackType.Hand);

            // Act
            attackState.Enter(mockPlayer);

            // Assert
            Assert.Equal(0.0f, attackState.GetAttackTimer());
        }
    }
}
