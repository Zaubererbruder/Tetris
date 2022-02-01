using Assets.Scripts.Model.Bricks;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public class DefaultBrickPatternRandomizer : IBrickPatternPicker
    {
        private IReadOnlyList<BrickPattern> _brickPatterns;

        public DefaultBrickPatternRandomizer()
        {
            _brickPatterns = new List<BrickPattern>(ReflectionHelper.GetEnumerableOfType<BrickPattern>());
        }

        public BrickPattern PickBrickPattern()
        {
            var brickIndex = Random.Range(0, _brickPatterns.Count - 1);
            return _brickPatterns[brickIndex];
        }
    }
}
