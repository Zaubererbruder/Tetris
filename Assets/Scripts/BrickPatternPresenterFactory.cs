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
    public class BrickPatternPresenterFactory : MonoBehaviour
    {
        private List<Block> _blockList = new List<Block>();

        private GameSettings _settings;

        public void Init(GameSettings settings)
        {
            _settings = settings;
        }

        public void Create(BrickPattern pattern, PositionTranslator positionTranslator, Transform patternParrent)
        {
            DestroyPrevious();
            foreach (var pos in pattern.Form.BlocksPosition)
            {
                var block = new Block(pos, Color.red);
                _blockList.Add(block);
                var instance = Instantiate(_settings.BlockPrefab, positionTranslator.GetPatternPosition(block.Position), Quaternion.identity, patternParrent);
                instance.Init(block, positionTranslator);
            }
        }

        private void DestroyPrevious()
        {
            foreach(var block in _blockList)
            {
                block.Destroy();
            }
            _blockList.Clear();
        }
    }
}
