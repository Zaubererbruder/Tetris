using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model
{
    public class DefaultOnStartBlockGenerator : IOnStartBlockGenerator
    {
        public IEnumerable<Block> GenerateBlocks(int xLength, int yLength)
        {
            return new Block[0];
        }
    }
}
