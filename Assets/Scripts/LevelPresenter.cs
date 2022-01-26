using Assets.Scripts.Model;
using Assets.Scripts.Model.Bricks;
using Assets.Scripts.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private GameSettings _gameSettings;
        private Level _level;

        public ScoreCounter ScoreCounter => _level.ScoreCounter;

        public void Init(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
            _level = new Level().WithSettings(gameSettings.FallsInSecond, gameSettings.AcellerationRate);
            _blockFactory.Init(_gameSettings);
            _patternFactory.Init(_gameSettings);
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
            _level.StartGame();
        }

        public void OnDisable()
        {
            _level.BrickLaunched -= BrickLaunchedHandler;
            _level = null;
        }

        public void Update()
        {
            _level?.Update(Time.deltaTime);
        }

        public void StartLevel()
        {
            _level.StartGame();
        }

        public void SendMoveCommand(int direction)
        {
            _level.DoMove(direction);
        }

        public void SendAccelerationCommand()
        {

            _level.AccelerateFall();
        }

        public void SendCancelAccelerationCommand()
        {

            _level.CancelAcceleration();

        }

        public void SendRotateCommand(bool clockwise)
        {

            _level.DoRotate(clockwise);

        }
    }
}
