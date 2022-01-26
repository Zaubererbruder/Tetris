using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public struct BrickForm
    {
        private IReadOnlyList<Position> _blocksPos;

        public BrickForm(params Position[] blocksPos)
        {
            _blocksPos = blocksPos;
        }

        public BrickForm(IEnumerable<Position> blocksPos)
        {
            _blocksPos = new List<Position>(blocksPos);
            
        }

        public IReadOnlyList<Position> BlocksPosition => _blocksPos;

    }
}
