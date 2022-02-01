using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.Model.Bricks;
using Assets.Scripts.Model.Settings;

namespace Assets.Scripts.Model
{
    public class Level
    {
        private Map _map;
        private BricksQueue _bricksQueue;
        private Brick _currentBrick;
        private BrickPattern _nextBrick;
        private ScoreCounter _scoreCounter;
        private float _defaultTickInterval = 0.5f;
        private float _acceleratedTickInterval = 0.1f;
        private float _tickInterval = 0.5f;
        private float _tick = 0f;
        private bool _gameActive;
        private GameSettings _settings;

        public BrickPattern NextBrick => _nextBrick;
        public ScoreCounter ScoreCounter => _scoreCounter;
        public event Action<Brick> BrickLaunched;
        public event Action<IEnumerable<Block>> OnStartBlockGenerated;

        public Level()
        {
            _settings = new GameSettings();
            ApplySettings();
        }

        public Level(GameSettings settings)
        {
            if (settings.LevelWidth <= 3 || settings.LevelHeight <= 3)
                throw new ArgumentException("Уровень должен быть размером не менее 4х4 ", nameof(settings));

            if (settings.FallsInSecond == 0 || settings.AccelerationRate == 0)
                throw new DivideByZeroException();

            _settings = settings;
            ApplySettings();
        }

        private void ApplySettings()
        {
            _map = new Map(_settings.LevelWidth, _settings.LevelHeight);
            _scoreCounter = new ScoreCounter();

            _defaultTickInterval = 1 / _settings.FallsInSecond;
            _acceleratedTickInterval = _defaultTickInterval / _settings.AccelerationRate;
            _tickInterval = _defaultTickInterval;
        }

        private void LaunchBrick()
        {
            _currentBrick = _bricksQueue.PickPatternFromQueue().Create(_map);
            _nextBrick = _bricksQueue.NextBrickPattern;
            BrickLaunched?.Invoke(_currentBrick);
        }

        public void StartGame()
        {
            _scoreCounter.ClearScore();

            var onStartBlocks = _settings.OnStartBlockGenerator.GenerateBlocks(_settings.LevelWidth, _settings.LevelHeight);
            _map.FillMap(onStartBlocks);
            OnStartBlockGenerated?.Invoke(onStartBlocks);

            _map.MapOverFilled += GameOver;
            _map.LineRemoved += _scoreCounter.AddScore;

            _bricksQueue = new BricksQueue(_settings.BrickPatternPicker, _settings.ColorPicker);
            _bricksQueue.PickPatternFromQueue();

            _gameActive = true;
            LaunchBrick();
        }

        private void GameOver()
        {
            _gameActive = false;
            _tick = 0;
            _map.MapOverFilled -= GameOver;
            _map.LineRemoved -= _scoreCounter.AddScore;
        }

        public void Update(float deltaTime)
        {
            if (_gameActive)
            {
                _tick += deltaTime;
                if (_tick >= _tickInterval)
                {
                    DoFall();
                    _tick = 0;
                }
            }
        }

        public void DoFall()
        {
            if (!_gameActive)
                return;

            if (_map.FallAccesible(_currentBrick.Blocks))
            {
                _currentBrick.DoFall();
            }
            else
            {
                _map.FillMap(_currentBrick.Blocks);
                LaunchBrick();
            }

        }

        public void DoMove(int direction)
        {
            if (!_gameActive)
                return;

            if (_map.MoveAccesible(_currentBrick.Blocks, direction))
            {
                _currentBrick.DoMove(direction);
            }
        }

        public void AccelerateFall()
        {
            if (!_gameActive)
                return;

            _tickInterval = _acceleratedTickInterval;
        }

        public void CancelAcceleration()
        {
            if (!_gameActive)
                return;

            _tickInterval = _defaultTickInterval;
        }

        public void DoRotate(bool clockwise)
        {
            if (!_gameActive)
                return;

            if (!_currentBrick.Rotateable)
                return;

            if (_map.RotateAccesible(_currentBrick.Blocks, _currentBrick.Blocks[0].Position, clockwise, out var xOffset))
                _currentBrick.DoRotate(clockwise, xOffset);
        }

    }
}