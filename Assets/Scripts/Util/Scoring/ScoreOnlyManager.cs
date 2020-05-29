﻿namespace Util.Scoring
{
    public class ScoreOnlyManager
    {
        public virtual float Score { get; protected set; }

        private readonly float _initialScore;
        private readonly float _incrementAmount;

        public ScoreOnlyManager(float initialScore = 0f, float incrementAmount = 1f)
        {
            Score = initialScore;
            _initialScore = initialScore;
            _incrementAmount = incrementAmount;
        }

        public float IncrementScore()
        {
            Score += _incrementAmount;
            return Score;
        }

        public float ResetScore()
        {
            Score = _initialScore;
            return Score;
        }
    }
}