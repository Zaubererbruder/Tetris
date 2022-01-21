using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public static class PositionTranslator
    {
        private static float _scale = 0.5f;
        private static float _offsetY = 12;
        public static Vector3 ToUnityPosition(Position position)
        {
            return new Vector3(position.X * _scale, (-position.Y + _offsetY) * _scale);
        }
    }
}
