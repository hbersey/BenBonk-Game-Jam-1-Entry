using UnityEngine;

namespace Util.Scoring
{
    public class ScoreAndHighScoreManager : ScoreOnlyManager
    {
        private float _highScore;
        private readonly string _prefix;
        private readonly bool _doAutoSave;

        public float HighScore
        {
            get
            {
                UpdateHighScore();
                return _highScore;
            }
            set => _highScore = value;
        }

        private readonly float _initialHighScore;

        public ScoreAndHighScoreManager(float initialScore = 0f, float incrementAmount = 1f,
            float initialHighScore = 0f, bool doAutoSave = false, string prefix = null)
            : base(initialScore, incrementAmount)
        {
            _initialHighScore = initialHighScore;
            HighScore = initialHighScore;
            _doAutoSave = doAutoSave;
            _prefix = prefix;
        }

        private void UpdateHighScore()
        {
            if (Score > _highScore) _highScore = Score;
            if (_doAutoSave)
                Save();
        }

        public float ResetHighScore()
        {
            HighScore = _initialHighScore;
            if (_doAutoSave)
                Save();
            return HighScore;
        }

        public void Save()
        {
            PlayerPrefs.SetFloat(_prefix != null ? @$"{_prefix}HighScore" : "HighScore", HighScore);
        }
    }
}