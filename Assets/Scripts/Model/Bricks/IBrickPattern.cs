using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Model.Bricks
{
    public class IBrickPattern : BrickPattern
    {
        private BrickForm _form;
        public IBrickPattern()
        {
            _form = new BrickForm(
                new Position(0, 0),
                new Position(0, -1),
                new Position(0, 1),
                new Position(0, 2));
        }

        public override int BlockCount { get => 4; }
        public override BrickForm Form => _form;
    }
}
