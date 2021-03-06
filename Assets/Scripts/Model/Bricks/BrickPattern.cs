using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Model.Bricks
{
    public abstract class BrickPattern
    {
        public abstract BrickForm Form { get; }

        public abstract bool Rotateable {get;}
        public abstract int BlockCount { get; }

        public Color Color { get; set; }

    }
}