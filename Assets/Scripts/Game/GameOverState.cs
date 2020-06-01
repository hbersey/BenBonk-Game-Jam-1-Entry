namespace Game
{
    public class GameOverState: GameState
    {
        public GameOverState(GameManager game) : base(game)
        {
        }

        public override void Begin()
        {
            Game.inGameGui.SetActive(false);
            Game.gameOverGui.SetActive(true);
            Game.gameOverScoreText.text = $"Scored {(int) Game.Scorer.Score}";
            Game.gameOverHighScoreText.text =
                (int) Game.Scorer.Score  == (int) Game.Scorer.HighScore ? "New High Score!" : $"High Score: {(int)Game.Scorer.HighScore}";
            
        }
    }
}