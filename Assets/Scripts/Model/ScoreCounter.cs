using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class ScoreCounter
    {
        private int _score = 0;

        public int Score => _score;
        public event Action<int> ScoreChanged;

        public void AddScore()
        {
            _score++;
            ScoreChanged?.Invoke(_score);
        }

        public void ClearScore()
        {
            _score = 0;
            ScoreChanged?.Invoke(_score);
        }
    }
}
