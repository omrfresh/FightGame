using OpenTK.Mathematics;

namespace Game
{
    // Статический класс, отвечающий за управление игроками
    public static class GameEngine
    {
        // Список всех игроков в игре
        private static List<Player> _players = new List<Player>();


        // Добавляет нового игрока в список игроков
        public static void AddPlayer(Player player)
        {
         _players.Add(player);
        }

        // Возвращает противника для указанного игрока (первого в списке, отличного от него)
        public static Player GetOpponent(Player player)
        {
            return _players.FirstOrDefault(p => p != player);
        }

        // Проверяет, находится ли атакующий игрок в заданном диапазоне от защищающегося
        public static bool IsInRange(Player attacker, Player defender, float range)
        {
            // Расстояние между двумя точками в двумерном пространстве
            return Vector2.Distance(attacker.Position, defender.Position) <= range;
        }

        // Возвращает количество игроков в игре
        public static int GetPlayerCount()
        {
            return _players.Count;
        }
    }
}