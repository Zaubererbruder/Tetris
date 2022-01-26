using Assets.Scripts.Model;
using Assets.Scripts.Model.Bricks;
using Assets.Scripts.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class LevelsConnector
    {
        private IReadOnlyList<LevelPresenter> _levelList;

        public LevelsConnector(IEnumerable<LevelPresenter> levelList)
        {
            _levelList = new List<LevelPresenter>(levelList);
        }

        public event Action<int> ScoreChanged;

        public void Init(GameSettings settings)
        {
            foreach (var level in _levelList)
            {
                level.Init(settings);
                level.ScoreCounter.ScoreChanged += (score) => ScoreChanged?.Invoke(_levelList.Sum((level)=>level.ScoreCounter.Score));
            }
        }

        public void SendMoveCommand(int direction)
        {
            foreach(var level in _levelList)
            {
                level.SendMoveCommand(direction);
            }
        }

        public void SendAccelerationCommand()
        {
            foreach (var level in _levelList)
            {
                level.SendAccelerationCommand();
            }
        }

        public void SendCancelAccelerationCommand()
        {
            foreach (var level in _levelList)
            {
                level.SendCancelAccelerationCommand();
            }
        }

        public void SendRotateCommand(bool clockwise)
        {
            foreach (var level in _levelList)
            {
                level.SendRotateCommand(clockwise);
            }
        }
    }
}
