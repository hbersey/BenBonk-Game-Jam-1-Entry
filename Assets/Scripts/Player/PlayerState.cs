namespace Player
{
    public abstract class PlayerState : StateMachine.State
    {
        protected readonly PlayerController Player;

        protected PlayerState(PlayerController player)
        {
            Player = player;
        }
    }
}