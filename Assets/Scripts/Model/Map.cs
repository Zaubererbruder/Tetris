using Assets.Scripts.Model.Bricks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public class Map
    {
        private Block[,] _map;
        private Position _startPosition = new Position(5, 0);
        private int _startY;
        private const int dimensionY = 1;
        private const int dimensionX = 0;

        private int xLength => _map.GetLength(dimensionX);
        private int yLength => _map.GetLength(dimensionY);

        public Map(int width, int height)
        {
            _map = new Block[width, height];
        }

        public Position StartPosition => _startPosition;
        public event Action MapOverFilled;
        public event Action LineRemoved;

        private bool CellUnaccesible(int x, int y)
        {
            return x < 0 || x >= xLength
                || y < 0 || y >= yLength
                || _map[x, y] != null;
        }

        private bool CellUnaccesible(Position pos)
        {
            return CellUnaccesible(pos.X, pos.Y);
        }

        public bool FallAccesible(IEnumerable<Block> blocks)
        {
            foreach (var block in blocks)
            {
                if (block.Position.Y + 1 >= yLength)
                    return false;

                var cell = _map[block.Position.X, block.Position.Y + 1];
                if (cell != null)
                    return false;
            }

            return true;
        }

        public bool MoveAccesible(IEnumerable<Block> blocks, int direction)
        {
            foreach (var block in blocks)
            {
                var newBlockPosX = block.Position.X + direction;
                if (newBlockPosX >= xLength || newBlockPosX == -1)
                    return false;

                if (block.Position.Y < 0)
                    continue;

                var cell = _map[newBlockPosX, block.Position.Y];
                if (cell != null)
                    return false;
            }

            return true;
        }

        public void FillMap(IEnumerable<Block> blocks)
        {
            foreach (var block in blocks)
            {
                if(block.Position.Y <0)
                {
                    MapOverFilled?.Invoke();
                    return;
                }
                _map[block.Position.X, block.Position.Y] = block;
            }

            while(LineFilled(out var lineIndex))
            {
                RemoveLine(lineIndex);
                LineRemoved?.Invoke();
            }
        }

        private void RemoveLine(int lineIndex)
        {
            for(int i = lineIndex;i>0;i--)
            {
                for (int j = 0; j < xLength; j++)
                {
                    if (i == lineIndex)
                        _map[j, i].Destroy();

                    _map[j, i] = i != 0 ? _map[j, i - 1] : null;
                    _map[j, i]?.Move(new Position(_map[j, i].Position.X, _map[j, i].Position.Y+1));
                }
            }
        }

        private bool LineFilled(out int lineIndex)
        {
            for(int i = 0;i<yLength;i++)
            {
                bool lineFilled = true;
                for (int j = 0; j < xLength; j++)
                {
                    if(!CellUnaccesible(j,i))
                    {
                        lineFilled = false;
                        break;
                    }
                }
                if (lineFilled)
                {
                    lineIndex = i;
                    return true;
                }
            }
            lineIndex = -1;
            return false;
        }

        public bool RotateAccesible(IEnumerable<Block> blocks, Position center, bool clockwise, out int xOffset)
        {
            //Как это упростить?
            xOffset = 0;
            var lxOffset = 0;
            List<(Block, Position)> newPositions = new List<(Block, Position)>();
            foreach (var block in blocks)
            {
                var relativePosition = block.Position - center;
                var rotatedPositionOffset = relativePosition.RotateAt0(clockwise);
                //if (CellUnaccesible(resultPosition))
                newPositions.Add((block, rotatedPositionOffset));
            }

            foreach(var blockTuple in newPositions)
            {
                if (!CellUnaccesible(center + blockTuple.Item2))
                    continue;

                if (blockTuple.Item2.Y != 0 && blockTuple.Item2.X == 0)
                    return false;

                if (lxOffset == 0)
                {
                    lxOffset = blockTuple.Item2.X;
                    continue;
                }
                else
                {
                    if (Math.Sign(lxOffset) != Math.Sign(blockTuple.Item2.X))
                        return false;
                    else
                    {
                        lxOffset = Math.Sign(lxOffset) * Math.Max(Math.Abs(lxOffset), Math.Abs(blockTuple.Item2.X));
                    }
                }
            }

            var movedPositions = newPositions.Select((t) => (t.Item1, new Position(t.Item2.X - lxOffset, t.Item2.Y))).ToList();
            foreach (var tuple in movedPositions)
            {
                if (CellUnaccesible(center + tuple.Item2))
                    return false;
            }

            xOffset = -lxOffset;
            return true;
        }

    }
}
