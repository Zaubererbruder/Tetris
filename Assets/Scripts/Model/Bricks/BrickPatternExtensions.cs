using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Model.Bricks
{
    public static class BrickPatternExtensions
    {
        public static Brick Create(this BrickPattern pattern, Map map)
        {
            return new Brick(pattern, map);
        }
    }
}