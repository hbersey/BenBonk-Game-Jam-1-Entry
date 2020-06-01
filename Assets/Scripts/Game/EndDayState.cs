namespace Game
{
    public class EndDayState: GameState
    {
        public EndDayState(GameManager game) : base(game)
        {
            
        }

        public override void Begin()
        {
            Game.EndOfDay();
        }
    }
}