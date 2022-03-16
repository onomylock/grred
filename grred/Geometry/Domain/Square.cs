using System;

using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
namespace GrRed.Geometry.Domain
{
    [DataContract]
    class Square : IFigure
    {
        private readonly Vector _Center = new(1.0, 1.0);
        private readonly double _Angle = 0.0;
        private readonly Vector _Scale = new(1.0, 1.0);

        public Square() { }

        [JsonConstructor]
        public Square(double Angle, Vector Center, Vector Scale)
        {
            _Center = Center;
            _Angle = Angle;
            _Scale = Scale;
        }

        public Square(IEnumerable<Vector> Points)
        {
            _Center = SetInputCentr(Points);
            _Angle = SetInputAngle(Points);

        }

        public string TypeName => "Square";
        [DataMember]
        public double Angle => _Angle;
        [DataMember]
        public Vector Center => _Center;
        [DataMember]
        public Vector Scale => _Scale;

        public (double l, double t, double r, double b) Gabarit =>
            (Center.X - Scale.X, Center.Y + Scale.Y, Center.X + Scale.X, Center.Y - Scale.Y);


        public void Draw(IGraphic graphic)
        {
        }

        // private Vector SetInputScale(IEnumerable<Vector> Points, Vector center)
        // {

        // }

        private double SetInputAngle(IEnumerable<Vector> Points)
        {
            Vector p1 = Points.ElementAt(1);
            Vector p2 = Points.ElementAt(2);
            return Math.Asin((p2.Y - p1.Y) / Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.X, 2));
        }

        private Vector SetInputCentr(IEnumerable<Vector> Points)
        {
            Vector p1 = Points.ElementAt(0);
            Vector p2 = Points.ElementAt(1);
            Vector p3 = Points.ElementAt(2);
            Vector p4 = Points.ElementAt(3);

            double n = 0;
            if ((p2.Y - p1.Y) != 0)
            {
                double q = (p3.X - p1.X) / (p1.Y - p3.Y);
                double sn = (p2.X - p4.X) + (p2.Y - p4.Y) * q;
                double fn = (p3.X - p1.X) + (p2.Y - p1.Y) * q;
                n = fn / sn;
            }
            else
            {
                n = (p2.Y - p1.Y) / (p2.Y - p4.Y);
            }
            return new Vector(p2.X + (p4.X - p2.X) * n, p2.Y + (p4.Y - p2.X) * n);
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
                side_B = Math.Sqrt(Math.Pow((Gabarit.r - Gabarit.l), 2) + Math.Pow((Gabarit.t - Gabarit.b), 2));
                side_A = Math.Sqrt((4.0 * Scale.Y * Scale.Y) / (Math.Sin(Angle) * Math.Sin(Angle)) - side_B * side_B);
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
            Vector deltaCenter = Center + delta;
            return new Square(Angle, deltaCenter, Scale);
        }

        public IFigure Reflection(bool axe)
        {
            double newAngle;

            if (axe) // Вертикальное отражение
            {
                newAngle = Math.PI - 2.0 * Angle;
                Vector newScale = new(Scale.X, -Scale.Y);
                return new Square(newAngle, Center, newScale);
            }
            else // Горизонтальное отражение
            {
                newAngle = 2.0 * Math.PI - 2.0 * Angle;
                Vector newScale = new(-Scale.X, Scale.Y);
                return new Square(newAngle, Center, newScale);
            }
        }

        public IFigure Rotate(double delta)
        {
            double newAngle = Angle + delta;
            Vector newScale = new(Scale.X * Math.Cos(newAngle), Scale.Y * Math.Cos(newAngle));
            return new Square(newAngle, Center, newScale);
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
