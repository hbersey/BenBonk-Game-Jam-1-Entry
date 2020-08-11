using StateMachine;

namespace Player
{
    internal class PlayerMoveState : State<Player>
    {
        public PlayerMoveState(Player controlling) : base(controlling)
        {
        }

        public override void OnPhysicsTick()
        {
            Controlling.Rigidbody.velocity = InputUtil.GetMovementRaw() * Controlling.speed;
            if (!InputUtil.IsMoving()) SetState(new PlayerIdleState(Controlling));
        } 
    }
}