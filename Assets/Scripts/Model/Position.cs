using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model
{
    public struct Position
    {
        //Убрал из матриц поворота 0. Также инвертированная ось Y
        private static Position _rotationMatrixClockwise = new Position(-1, 1);
        private static Position _rotationMatrixCounter = new Position(1, -1);

        public int X;
        public int Y;

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Position operator +(Position a, Position b) => new Position(a.X + b.X, a.Y + b.Y);

        public static Position operator -(Position a, Position b) => new Position(a.X - b.X, a.Y - b.Y);

        private Position MultiplyOnMatrix(Position matrix)
        {
            return new Position(
                    Y * matrix.X,
                    X * matrix.Y);
        }

        public Position RotateAt0(bool clockwise)
        {
            if (clockwise)
                return MultiplyOnMatrix(_rotationMatrixClockwise);
            else
                return MultiplyOnMatrix(_rotationMatrixCounter);
        }
    }
}

