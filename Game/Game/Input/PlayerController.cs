using Game.StateMachine;
using OpenTK.Mathematics;

namespace Game
{
    public class PlayerController
    {
        private Player _player;

        public PlayerController(Player player)
        {
            _player = player;
        }
        public IState CurrentState => _player.CurrentState;

        public void Update(bool moveLeft, bool moveRight, bool attack, bool legAttack, bool comboAttack, bool block)
        {
            if (_player.CurrentState is DeadState)
            {
                return;
            }
            if (moveLeft && !moveRight)
            {
                _player.ChangeState(new MoveState(_player, new Vector2(-_player.Speed, 0)));
            }
            else if (moveRight && !moveLeft)
            {
                _player.ChangeState(new MoveState(_player, new Vector2(_player.Speed, 0)));
            }
            else if (attack)
            {
                _player.ChangeState(new AttackState(_player, AttackType.Hand));
            }
            else if (legAttack)
            {
                _player.ChangeState(new AttackState(_player, AttackType.Leg));
            }
            else if (comboAttack)
            {
                _player.ChangeState(new AttackState(_player, AttackType.Combo));
            }
            else if (block)
            {
                _player.ChangeState(new BlockState(_player));
            }
            else
            {
                if (_player.Health <= 0)
                {
                    _player.ChangeState(new DeadState(_player));
                }
                else
                {
                    _player.ChangeState(new IdleState(_player));
                }
            }

            _player.Update();
        }
    }
}
