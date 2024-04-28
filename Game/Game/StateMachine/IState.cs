namespace Game.StateMachine
{
    public interface IState
    {
        void Enter(Player player);
        void Exit(Player player);
        void Update(Player player);
    }
}