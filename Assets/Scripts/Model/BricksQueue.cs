using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.Model.Bricks;

namespace Assets.Scripts.Model
{
    public class BricksQueue
    {
        private BrickPattern _nextBrickPattern;
        private IBrickPatternPicker _brickPatternPicker;
        private IColorPicker _colorPicker;

        public BricksQueue(IBrickPatternPicker patternPicker, IColorPicker colorPicker)
        {
            _brickPatternPicker = patternPicker;
            _colorPicker = colorPicker;
        }

        public BrickPattern NextBrickPattern => _nextBrickPattern;

        public BrickPattern PickPatternFromQueue()
        {
            var oldPattern = _nextBrickPattern;
            var pattern = _brickPatternPicker.PickBrickPattern();
            pattern.Color = _colorPicker.PickColor();
            _nextBrickPattern = pattern;
            return oldPattern;
        }
    }
}
