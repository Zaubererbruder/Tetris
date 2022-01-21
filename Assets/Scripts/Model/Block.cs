using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public class Block
    {
        private Color _color;
        private Position _position;

        public Block(Position position, Color color)
        {
            _position = position;
            _color = color;
        }

        public Position Position => _position;
        public Color Color => _color;

        public event Action BlockMoved;
        public event Action BlockDestroyed;

        public void Move(Position position)
        {
            _position = position;
            BlockMoved?.Invoke();
        }

        internal void Destroy()
        {
            BlockDestroyed?.Invoke();
        }
    }
}