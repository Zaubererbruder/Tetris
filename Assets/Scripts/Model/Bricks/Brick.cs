using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Model.Bricks
{
    public class Brick
    {
        private BrickPattern _pattern;
        private BrickForm _form;
        private Block _centerBlock;
        private Color _color;
        private IReadOnlyList<Block> _blocks;
        private Map _map;
       
        public Brick(BrickPattern pattern, Map map)
        {
            _pattern = pattern;
            _map = map;
            _form = _pattern.Form;
            _color = pattern.Color;

            var blockList = new List<Block>(pattern.BlockCount);
            for (var i = 0; i < pattern.BlockCount; i++)
            {
                var blockOffsetPos = _pattern.Form.BlocksPosition[i];
                var position = _map.StartPosition + blockOffsetPos;
                var block = new Block(position, _color);
                if (_centerBlock == null)
                    _centerBlock = block;

                blockList.Add(block);
            }
            _blocks = blockList;

        }

        public IReadOnlyList<Block> Blocks => _blocks;
        public bool Rotateable => _pattern.Rotateable;

        public void DoFall()
        {
            foreach (var block in _blocks)
            {
                var newPos = new Position(block.Position.X, block.Position.Y - 1);
                block.Move(newPos);
            }
        }

        public void DoMove(int direction)
        {
            foreach (var block in _blocks)
            {
                var newPos = new Position(block.Position.X + direction, block.Position.Y);
                block.Move(newPos);
            }
        }

        public void DoRotate(bool clockwise, int xOffset)
        {
            var centralPos = _centerBlock.Position;
            foreach(var block in _blocks)
            {
                var relativePosition = block.Position - centralPos;
                var rotatedPositionOffset = relativePosition.RotateAt90(clockwise);
                var resultPositionOffset = new Position(rotatedPositionOffset.X + xOffset, rotatedPositionOffset.Y);
                block.Move(centralPos + resultPositionOffset);
            }
        }
    }
}

