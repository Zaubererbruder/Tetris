using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model.Bricks
{
    public class LBrickPattern : BrickPattern
    {
        private BrickForm _form;
        public LBrickPattern()
        {
            _form = new BrickForm(
                new Position(0, 0),
                new Position(0, 1),
                new Position(1, 1),
                new Position(0, -1));
        }

        public override int BlockCount { get => 4; }
        public override BrickForm Form => _form;
        public override bool Rotateable => true;
    }
}
