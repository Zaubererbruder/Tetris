using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Settings;
using Assets.Scripts.SerializableDictionaries;

namespace Assets.Scripts
{
    public class Root : MonoBehaviour
    {
        [SerializeField] private InputRouter _inputRouter;
        [SerializeField] private GameUIPresenter _uiPresenter;
        [SerializeField] private GamePreferences _gameSettings;
        [SerializeField] private DifficultyDictionary _difficultySettings;

        private GameDifficulty _gameDifficulty = GameDifficulty.Easy;
        private List<LevelPresenter> _levelList;
        private ScoreAggregator _scoreAggregator;

        public void InitLevels()
        {
            var includeInactive = false;
            var levels = GetComponentsInChildren<LevelPresenter>(includeInactive);
            _levelList = new List<LevelPresenter>(levels);
            _levelList.Init(_gameSettings, _difficultySettings[_gameDifficulty], _inputRouter);
            _scoreAggregator = _levelList.GetScoreAggregator();
            _scoreAggregator.ScoreChanged += _uiPresenter.ScoreChangedHandler;
            _levelList.SetEnabled(true);
            _scoreAggregator.OnEnable();
        }

        public void SetDifficulty(float indexDifficulty)
        {
            _gameDifficulty = (GameDifficulty)(int)indexDifficulty;
        }
    }
}