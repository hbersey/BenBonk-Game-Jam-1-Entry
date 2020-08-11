using StateMachine;

namespace Game
{
    internal class InGameState : State<GameManager>
    {
        public InGameState(GameManager controlling) : base(controlling)
        {
        }
    }
}