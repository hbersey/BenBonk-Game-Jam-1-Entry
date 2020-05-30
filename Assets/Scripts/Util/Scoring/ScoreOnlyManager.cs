namespace Util.Scoring
{
    public class ScoreOnlyManager
    {
        public virtual float Score { get; protected set; }

        private readonly float _initialScore;
        internal float _incrementAmount;

        public ScoreOnlyManager(float initialScore = 0f, float incrementAmount = 1f)
        {
            Score = initialScore;
            _initialScore = initialScore;
            _incrementAmount = incrementAmount;
        }


        public void IncrementScore(float amount) => Score += amount;
        public void IncrementScore() => IncrementScore(_incrementAmount);

        public float ResetScore()
        {
            Score = _initialScore;
            return Score;
        }
    }
}