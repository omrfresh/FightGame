using OpenTK.Mathematics;

namespace Game
{
    public static class GameEngine
    {
        private static List<Player> _players = new List<Player>();

        public static void AddPlayer(Player player)
        {
            _players.Add(player);
        }

        public static Player GetOpponent(Player player)
        {
            return _players.FirstOrDefault(p => p != player);
        }

        public static bool IsInRange(Player attacker, Player defender, float range)
        {
            return Vector2.Distance(attacker.Position, defender.Position) <= range;
        }
    }
}