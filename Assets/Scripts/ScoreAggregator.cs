using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class ScoreAggregator
    {
        private IReadOnlyList<ScoreCounter> _scoreCounters;
        private int _score;

        public event Action<int> ScoreChanged;

        public ScoreAggregator(IEnumerable<ScoreCounter> scoreCounters)
        {
            _scoreCounters = new List<ScoreCounter>(scoreCounters);
        }

        public void OnEnable()
        {
            foreach(var sconter in _scoreCounters)
            {
                sconter.ScoreChanged += ScoreCounterScoreChangedHandler;
            }
        }

        private void ScoreCounterScoreChangedHandler(int score)
        {
            _score = _scoreCounters.Sum((sc) => sc.Score);
            ScoreChanged?.Invoke(_score);
        }

        public void OnDisable()
        {
            foreach (var sconter in _scoreCounters)
            {
                sconter.ScoreChanged -= ScoreCounterScoreChangedHandler;
            }
        }
    }
}
