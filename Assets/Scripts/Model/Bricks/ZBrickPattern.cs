using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model.Bricks
{
    public class ZBrickPattern : BrickPattern
    {
        private BrickForm _form;
        public ZBrickPattern()
        {
            _form = new BrickForm(
                new Position(0, 0),
                new Position(-1, 0),
                new Position(0, 1),
                new Position(1, 1));
        }

        public override int BlockCount { get => 4; }
        public override BrickForm Form => _form;
        public override bool Rotateable => true;
    }
}
