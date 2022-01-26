using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Settings;

namespace Assets.Scripts
{
    public class Root : MonoBehaviour
    {
        [SerializeField] private InputRouter _inputRouter;
        [SerializeField] private GameUIPresenter _uiPresenter;
        [SerializeField] private GameSettings _gameSettings;

        private List<LevelPresenter> _levelList;
        private LevelsConnector _levelsConnector;

        public void Awake()
        {
            var levels = GetComponentsInChildren<LevelPresenter>(true);
            _levelList = new List<LevelPresenter>(levels);
            _levelsConnector = new LevelsConnector(_levelList);
            _levelsConnector.Init(_gameSettings);
            _inputRouter.Init(_levelsConnector);

            _uiPresenter.Init(_levelsConnector);
        }
    }
}