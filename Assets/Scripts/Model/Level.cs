using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.Model.Bricks;

namespace Assets.Scripts.Model
{
    public class Level
    {
        private Map _map;
        private BrickRandomizer _brickRandomizer;
        private Brick _currentBrick;
        private BrickPattern _nextBrick;
        private ScoreCounter _scoreCounter;
        private float _defaultTickInterval = 0.5f;
        private float _acceleratedTickInterval = 0.1f;
        private float _tickInterval = 0.5f;
        private float _tick = 0f;
        private bool _gameActive;

        public BrickPattern NextBrick => _nextBrick;
        public ScoreCounter ScoreCounter => _scoreCounter;
        public event Action<Brick> BrickLaunched;

        public Level()
        {
            _map = new Map(12, 24);
            _scoreCounter = new ScoreCounter();
        }

        public Level WithSettings(float fallsInSecond, float accelerationRate)
        {
            if (fallsInSecond == 0)
                throw new DivideByZeroException();

            _defaultTickInterval = 1 / fallsInSecond;
            _acceleratedTickInterval = _defaultTickInterval / accelerationRate;
            _tickInterval = _defaultTickInterval;
            return this;
        }

        private void LaunchBrick()
        {
            _currentBrick = _nextBrick.Create(_map);
            _nextBrick = _brickRandomizer.ChooseNextBrick();
            BrickLaunched?.Invoke(_currentBrick);
        }

        public void StartGame()
        {
            _gameActive = true;
            _scoreCounter.ClearScore();
            _map.MapOverFilled += GameOver;
            _map.LineRemoved += LineRemovedHandler;
            var patterns = ReflectionHelper.GetEnumerableOfType<BrickPattern>();
            _brickRandomizer = new BrickRandomizer(patterns);
            _nextBrick = _brickRandomizer.ChooseNextBrick();
            LaunchBrick();
        }

        private void LineRemovedHandler()
        {
            _scoreCounter.AddScore();
        }

        private void GameOver()
        {
            _gameActive = false;
            _tick = 0;
            _map.MapOverFilled -= GameOver;
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

        public bool DoMove(int direction)
        {
            if (!_gameActive)
                return false;

            if (_map.MoveAccesible(_currentBrick.Blocks, direction))
            {
                _currentBrick.DoMove(direction);
                return true;
            }

            return false;
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

            if (_map.RotateAccesible(_currentBrick.Blocks, _currentBrick.Blocks[0].Position, clockwise, out var xOffset))
                _currentBrick.DoRotate(clockwise, xOffset);
        }

    }
}