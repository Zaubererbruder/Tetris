using Assets.Scripts.Model;
using Assets.Scripts.Model.Bricks;
using Assets.Scripts.SerializableDictionaries;
using Assets.Scripts.Settings;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class LevelPresenter : MonoBehaviour
    {
        [SerializeField] PositionTranslator _positionTranslator;
        [SerializeField] private BlockPresenterFactory _blockFactory;
        [SerializeField] private BrickPatternPresenterFactory _patternFactory;
        [SerializeField] private Transform _blocksParrent;
        [SerializeField] private Transform _patternParrent;

        private GamePreferences _gameSettings;
        private DifficultyPreset _difficultyPreset;
        private InputRouter _inputRouter;
        private Level _level;
        private ScoreAggregator _scoreAggregator;

        public ScoreCounter ScoreCounter => _level.ScoreCounter;

        public void Init(GamePreferences gameSettings, DifficultyPreset difficultyPreset, InputRouter inputRouter)
        {
            _gameSettings = gameSettings;
            _difficultyPreset = difficultyPreset;
            _inputRouter = inputRouter;

            _blockFactory.Init(_gameSettings);
            _patternFactory.Init(_gameSettings);

            _level = new Level(_difficultyPreset.GetGameSettings());
        }

        private void BrickLaunchedHandler(Brick brick)
        {
            CreateBrick(brick);
            _patternFactory.Create(_level.NextBrick, _positionTranslator, _patternParrent);
        }

        private void CreateBrick(Brick brick)
        {
            foreach (var block in brick.Blocks)
            {
                _blockFactory.Create(block, _positionTranslator, _blocksParrent);
            }
        }

        public void OnEnable()
        {
            _level.BrickLaunched += BrickLaunchedHandler;
            _level.OnStartBlockGenerated += OnStartBlockGeneratedHandler;

            _inputRouter.MovePressed += _level.DoMove;
            _inputRouter.RotatePressed += _level.DoRotate;
            _inputRouter.AcceleratePressed += _level.AccelerateFall;
            _inputRouter.AccelerateReleased += _level.CancelAcceleration;

            _level.StartGame();
        }

        private void OnStartBlockGeneratedHandler(IEnumerable<Block> blocks)
        {
            foreach (var block in blocks)
            {
                _blockFactory.Create(block, _positionTranslator, _blocksParrent);
            }
        }

        public void OnDisable()
        {
            _level.BrickLaunched -= BrickLaunchedHandler;
            _level.OnStartBlockGenerated -= OnStartBlockGeneratedHandler;

            _inputRouter.MovePressed -= _level.DoMove;
            _inputRouter.RotatePressed -= _level.DoRotate;
            _inputRouter.AcceleratePressed -= _level.AccelerateFall;
            _inputRouter.AccelerateReleased -= _level.CancelAcceleration;
        }

        public void Update() => _level?.Update(Time.deltaTime);
    }
}
