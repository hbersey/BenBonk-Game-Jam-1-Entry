namespace Game
{
    public class EndGameState: GameState
    {
        public EndGameState(GameManager game) : base(game)
        {
            
        }

        public override void Begin()
        {
            Game.EndOfDay();
        }
    }
}