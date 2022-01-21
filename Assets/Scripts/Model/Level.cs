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
        private float _defaultTickInterval = 0.5f;
        private float _acceleratedTickInterval = 0.1f;
        private float _tickInterval = 0.5f;
        private float _tick = 0f;
        private bool _gameActive;

        public event Action<Brick> BrickLaunched;

        public Level()
        {
            _map = new Map(12, 24);
        }

        private void LaunchBrick()
        {
            _currentBrick = _nextBrick.Create(_map);
            BrickLaunched?.Invoke(_currentBrick);
            _nextBrick = _brickRandomizer.ChooseNextBrick();
        }

        public void StartGame()
        {
            _gameActive = true;
            _map.MapOverFilled += GameOver;
            _brickRandomizer = new BrickRandomizer(new IBrickPattern());
            _nextBrick = _brickRandomizer.ChooseNextBrick();
            LaunchBrick();
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

            if (_map.RotateAccesible(_currentBrick.Blocks, _currentBrick.Blocks[0].Position, clockwise, out var xOffset))
                _currentBrick.DoRotate(clockwise, xOffset);
        }

    }
}