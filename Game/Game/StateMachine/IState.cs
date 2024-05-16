// Пространство имен для машины состояний игры
namespace Game.StateMachine
{
    // Интерфейс для состояний игрока
    public interface IState
    {
        // Метод, вызываемый при входе в состояние
        void Enter(Player player);

        // Метод, вызываемый при выходе из состояния
        void Exit(Player player);

        // Метод, вызываемый при обновлении состояния
        void Update(Player player);
    }
}
