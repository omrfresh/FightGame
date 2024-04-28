namespace Game
{
    public static class GameEngine
    {
        private static List<Player> _players = new List<Player>();
        private static List<Enemy> _enemies = new List<Enemy>();

        public static void AddPlayer(Player player)
        {
            _players.Add(player);
        }

        public static void AddEnemy(Enemy enemy)
        {
            _enemies.Add(enemy);
        }

        public static List<Enemy> GetEnemiesInRange(Vector2 position, float range)
        {
            return _enemies.Where(enemy => Vector2.Distance(position, enemy.Position) <= range).ToList();
        }
    }
}
