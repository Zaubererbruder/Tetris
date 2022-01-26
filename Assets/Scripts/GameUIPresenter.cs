using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameUIPresenter : MonoBehaviour
    {
        [SerializeField] private Text _scoreUI;
        private LevelsConnector _levelsConnector;

        public void Init(LevelsConnector scoreCounter)
        {
            _levelsConnector = scoreCounter;
            enabled = true;
        }

        public void OnEnable()
        {
            _levelsConnector.ScoreChanged += ScoreChangedHandler;
        }

        private void ScoreChangedHandler(int score)
        {
            _scoreUI.text = $"Score: {score}";
        }

        public void OnDisable()
        {
            _levelsConnector.ScoreChanged -= ScoreChangedHandler;
        }
    }
}
