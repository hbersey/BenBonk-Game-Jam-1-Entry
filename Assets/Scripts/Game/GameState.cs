namespace Game
{
    public abstract class GameState: StateMachine.State
    {

        protected GameManager Game;

        public GameState(GameManager game)
        {
            Game = game;
        }



    }
}