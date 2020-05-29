using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using Util.Scoring;

namespace Examples.Clicker
{
    public class ClickerExampleGameManager : MonoBehaviour
    {
        [SerializeField] private Text scoreText;
        [SerializeField] private Text highScoreText;

        private ScoreAndHighScoreManager _scorer;

        private void Start()
        {
            _scorer = new ScoreAndHighScoreManager(doAutoSave: true, prefix: "clicker_example");
            RenderBoth();
        }

        // ReSharper disable once UnusedMember.Global
        public void ResetHighScore()
        {
            _scorer.ResetScore();
            _scorer.ResetHighScore();
            RenderBoth();
        }

        // ReSharper disable once UnusedMember.Global
        public void ResetScore()
        {
            _scorer.ResetScore();
           RenderScore();
        }

        // ReSharper disable once UnusedMember.Global
        public void Increment()
        {
            _scorer.IncrementScore();
            RenderBoth();
        }

        private void RenderScore() => scoreText.text = _scorer.Score.ToString(CultureInfo.InvariantCulture);

        private void RenderHighScore() => highScoreText.text = $"Best: {_scorer.HighScore.ToString(CultureInfo.InvariantCulture)}";

        private void RenderBoth()
        {
            RenderScore();
            RenderHighScore();
        }

    }
}