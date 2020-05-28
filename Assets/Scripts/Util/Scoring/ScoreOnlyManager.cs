namespace Util.Scoring
{
    public class ScoreOnlyManager
    {
        public float Score;
        private readonly float _initialScore;
        private readonly float _incrementAmount;

        public ScoreOnlyManager(float initialScore = 0f, float incrementAmount = 1f)
        {
            Score = initialScore;
            _initialScore = initialScore;
            _incrementAmount = incrementAmount;
        }

        public float Increment()
        {
            Score += _incrementAmount;
            return Score;
        }

        public float Reset()
        {
            Score = _initialScore;
            return Score;
        }
    }
}