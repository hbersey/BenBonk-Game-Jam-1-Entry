namespace Util.Scoring
{
    public class ScoreAndHighScoreManager : ScoreOnlyManager
    {
        private float _highScore;

        public float HighScore
        {
            get
            {
                UpdateHighScore();
                return _highScore;
            }
            set => _highScore = value;
        }

        private float _initialHighScore;

        public ScoreAndHighScoreManager(float intialScore = 0f, float incrementAmount = 1f, float initialHighScore = 0f)
            : base(intialScore, incrementAmount)
        {
            _initialHighScore = initialHighScore;
            HighScore = initialHighScore;
        }

        private void UpdateHighScore()
        {
            if (Score > _highScore) _highScore = Score;
        }
    }
}