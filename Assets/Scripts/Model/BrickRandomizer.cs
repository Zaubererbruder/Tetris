using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.Model.Bricks;

namespace Assets.Scripts.Model
{
    public class BrickRandomizer
    {
        private IReadOnlyList<BrickPattern> _bricks;
        private BrickPattern _currentBrick;

        public BrickRandomizer(params BrickPattern[] bricks)
        {
            _bricks = new List<BrickPattern>(bricks);
        }

        public BrickPattern CurrentBrick => _currentBrick;

        public BrickPattern ChooseNextBrick()
        {
            var brickIndex = Random.Range(0, _bricks.Count - 1);
            _currentBrick = _bricks[brickIndex];
            return _currentBrick;
        }
    }
}
