using Assets.Scripts.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class OnStartBlockGeneratorNormalDif : IOnStartBlockGenerator
    {
        public int Rows { get; set; }
        public IEnumerable<Block> GenerateBlocks(int xLength, int yLength)
        {
            var list = new List<Block>();

            for (int y = 0; y <= Rows; y++)
            {
                for (int x = 0; x < xLength; x++)
                {
                    if(Random.Range(0, 2) == 1)
                    {
                        list.Add(new Block(new Position(x, y), Color.white));
                    }
                }
            }
            return list;
        }
    }

    public class OnStartBlockGeneratorHardDif : IOnStartBlockGenerator
    {
        public IEnumerable<Block> GenerateBlocks(int xLength, int yLength)
        {
            var list = new List<Block>();

            for (int y = 0; y <= yLength / 6; y++)
            {
                for (int x = 0; y <= xLength; x++)
                {
                    if (Random.Range(0, 1) == 1)
                    {
                        list.Add(new Block(new Position(x, y), Color.white));
                    }
                }
            }
            return list;
        }
    }
}
