using System;

namespace GrRed.Geometry.Domain
{
    class Square : IFigure
    {
        private readonly Vector _Center = new(1.0, 1.0);
        private readonly double _Angle = 0.0;
        private readonly Vector _Scale = new(1.0, 1.0);

        public Square() { }

        public Square(double Angle, Vector Center, Vector Scale)
        {
            _Center = Center;
            _Angle = Angle;
            _Scale = Scale;
        }

        public string TypeName => "Square";
        public double Angle => _Angle;
        public Vector Center => _Center;
        public Vector Scale => _Scale;

        public (double l, double t, double r, double b) Gabarit =>
            (Center.X - Scale.X, Center.Y + Scale.Y, Center.X + Scale.X, Center.Y - Scale.Y);


        public void Draw(IGraphic graphic)
        {
            throw new NotImplementedException();
        }

        public IFigure Intersection(IFigure fig2)
        {
            throw new NotImplementedException();
        }

        public bool IsIn(Vector p, double eps)
        {
            double side_A;
            double side_B;

            if (Math.Abs(Math.PI + Angle) % 2.0 * Math.PI <= eps) // Случай, когда угол кратен пи
            {
                side_B = Scale.X;
                side_A = Scale.Y;
            }
            else if (Math.Abs(Angle) % 2.0 * Math.PI <= eps) // Случай, когда угол кратен 2пи (0, в частности)
            {
                side_A = Scale.X;
                side_B = Scale.Y;
            }
            else                                          // Любой другой случай
            {
                side_B = Math.Sqrt(  Math.Pow((Gabarit.r - Gabarit.l), 2) + Math.Pow((Gabarit.t - Gabarit.b), 2)  );
                side_A = Math.Sqrt(  (4.0 * Scale.Y * Scale.Y) / (Math.Sin(Angle) * Math.Sin(Angle)) - side_B * side_B  );
            }

            double A_bigger_2 = Math.Abs(side_A) / 2.0;
            double B_bigger_2 = Math.Abs(side_B) / 2.0;

            double IsInCheck = Math.Abs((p.X - Center.X) * Math.Cos(Math.PI / 4.0 + Angle) / A_bigger_2 + (p.Y - Center.Y) * Math.Sin(Math.PI / 4.0 + Angle) / B_bigger_2) + Math.Abs((p.X - Center.X) * Math.Sin(Math.PI / 4.0 + Angle) / (-A_bigger_2) + (p.Y - Center.Y) * Math.Cos(Math.PI / 4.0 + Angle) / B_bigger_2);

            if (IsInCheck + eps <= Math.Sqrt(2.0) || IsInCheck - eps <= Math.Sqrt(2.0))
                return true;
            else
                return false;
        }

        public IFigure Move(Vector delta)
        {
            throw new NotImplementedException();
        }

        public IFigure Reflection(bool axe)
        {
            throw new NotImplementedException();
        }

        public IFigure Rotate(double delta)
        {
            throw new NotImplementedException();
        }

        public IFigure SetScale(double dx, double dy)
        {
            Vector newScale = new(Scale.X + dx / 2, Scale.Y + dy / 2);
            return new Square(Angle, Center, newScale);
        }

        public IFigure Subtruct(IFigure fig2)
        {
            throw new NotImplementedException();
        }

        public IFigure Union(IFigure fig2)
        {
            throw new NotImplementedException();
        }
    }
}
