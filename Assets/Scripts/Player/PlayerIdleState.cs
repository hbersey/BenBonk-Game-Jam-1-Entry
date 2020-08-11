using StateMachine;

namespace Player
{
    internal class PlayerIdleState : State<Player>
    {
        public PlayerIdleState(Player controlling) : base(controlling)
        {
        }

        public override void OnPhysicsTick()
        {
            if (InputUtil.IsMoving()) SetState(new PlayerMoveState(Controlling));
        }
    }
}