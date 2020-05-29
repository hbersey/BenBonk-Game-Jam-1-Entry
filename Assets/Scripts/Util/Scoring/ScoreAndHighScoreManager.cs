using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Util.Scoring
{
    public class ScoreAndHighScoreManager : ScoreOnlyManager
    {
        public override float Score
        {
            protected set
            {
                base.Score = value;
                UpdateHighScore();
            }
            get => base.Score;
        }

        private readonly string _prefix;
        private readonly bool _doAutoSave;

        public float HighScore;

        private readonly float _initialHighScore;

        public ScoreAndHighScoreManager(float initialScore = 0f, float incrementAmount = 1f,
            float initialHighScore = 0f, bool doAutoSave = false, [NotNull] string prefix = "", bool doTryLoad = true)
            : base(initialScore, incrementAmount)
        {
            _initialHighScore = initialHighScore;
            _doAutoSave = doAutoSave;
            _prefix = prefix;
            HighScore = doTryLoad && PlayerPrefs.HasKey(HighScoreKey())
                ? PlayerPrefs.GetFloat(HighScoreKey())
                : initialHighScore;
        }

        private void UpdateHighScore()
        {
            if (Score > HighScore) HighScore = Score;
            if (_doAutoSave)
                Save();
        }

        public void ResetHighScore()
        {
            HighScore = _initialHighScore;
            if (_doAutoSave)
                Save();
        }

        public void Save() => PlayerPrefs.SetFloat(HighScoreKey(), HighScore);
        private string HighScoreKey() => $"{_prefix}HighScore";
    }
}