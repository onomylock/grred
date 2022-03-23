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
        // private readonly Vector _Center = new(1.0, 1.0);
        // private readonly double _Angle = 0.0;
        // private readonly Vector _Scale = new(1.0, 1.0); 

        public Square() { }

        [JsonConstructor]
        public Square(double angle, Vector center, Vector scale)
        {
            Center = center;
            Angle = angle;
            Scale = scale;
            Gabarit = (Center.X - Scale.X, Center.Y + Scale.Y, Center.X + Scale.X, Center.Y - Scale.Y);
            Points = SetInputPoints();
        }

        public Square(IEnumerable<Vector> Points)
        {
            this.Points = Points.ToArray();
            Center = SetInputCenter(this.Points);
            Angle = SetInputAngle(this.Points);
            Scale = SetInputScale(this.Points);
            Gabarit = (Center.X - Scale.X, Center.Y + Scale.Y, Center.X + Scale.X, Center.Y - Scale.Y);
        }

        public string TypeName => "Square";
        [DataMember]
        public Vector[] Points { get; }
        public double Angle { get; }
        [DataMember]
        public Vector Center { get; }
        [DataMember]
        public Vector Scale { get; }
        public (double l, double t, double r, double b) Gabarit { get; }

        public void Draw(IGraphic graphic)
        {
            graphic.AddLines(Points);
        }

        // Методы задания параметров класса

        private Vector[] SetInputPoints()
        {
            Vector[] newPoints = new Vector[4];

            newPoints[0] = new Vector(Center.X - (Gabarit.l * Math.Cos(Angle) - Gabarit.b * Math.Sin(Angle)), Center.Y - (Gabarit.l * Math.Sin(Angle) + Gabarit.b * Math.Cos(Angle)));
            newPoints[1] = new Vector(Center.X - (Gabarit.l * Math.Cos(Angle) - Gabarit.t * Math.Sin(Angle)), Center.Y + (Gabarit.l * Math.Sin(Angle) + Gabarit.t * Math.Cos(Angle)));
            newPoints[2] = new Vector(Center.X + (Gabarit.r * Math.Cos(Angle) - Gabarit.t * Math.Sin(Angle)), Center.Y + (Gabarit.r * Math.Sin(Angle) + Gabarit.t * Math.Cos(Angle)));
            newPoints[3] = new Vector(Center.X + (Gabarit.r * Math.Cos(Angle) - Gabarit.b * Math.Sin(Angle)), Center.Y - (Gabarit.r * Math.Sin(Angle) + Gabarit.b * Math.Cos(Angle)));
            return newPoints;
        }
        private Vector SetInputScale(Vector[] Points) => new Vector(Center.X - (Points[1] - Points[0]).X / 2, (Points[2] - Points[1]).Y / 2 - Center.Y);
        //private double SetInputAngle(Vector[] Points) => Math.Acos((Points[3].X - Points[0].X) / VectorModul(Points[1] - Points[0]));

        private double SetInputAngle(Vector[] Points)
        {
            if (Points[3].Y >= Points[0].Y) return Math.Acos((Points[3].X - Points[0].X) / VectorModul(Points[3] - Points[0]));
            else return -Math.Acos((Points[3].X - Points[0].X) / VectorModul(Points[3] - Points[0]));
        }
        private double VectorModul(Vector a) => Math.Sqrt(Math.Pow(a.X, 2) + Math.Pow(a.Y, 2));
        private Vector SetInputCenter(Vector[] Points)
        {
            Vector p1 = Points[0];
            Vector p2 = Points[1];
            Vector p3 = Points[2];
            Vector p4 = Points[3];

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

        // Внешние методы класса
        public IFigure Intersection(IFigure fig2)
        {
            throw new NotImplementedException();
        }

        public bool IsIn(Vector p, double eps)
        {
            // double side_A;
            // double side_B;

            // if (Math.Abs(Math.PI + Angle) % 2.0 * Math.PI <= eps) // Случай, когда угол кратен пи
            // {
            //     side_B = Scale.X;
            //     side_A = Scale.Y;
            // }
            // else if (Math.Abs(Angle) % 2.0 * Math.PI <= eps) // Случай, когда угол кратен 2пи (0, в частности)
            // {
            //     side_A = Scale.X;
            //     side_B = Scale.Y;
            // }
            // else                                          // Любой другой случай
            // {
            //     side_B = Math.Sqrt(Math.Pow((Gabarit.r - Gabarit.l), 2) + Math.Pow((Gabarit.t - Gabarit.b), 2));
            //     side_A = Math.Sqrt((4.0 * Scale.Y * Scale.Y) / (Math.Sin(Angle) * Math.Sin(Angle)) - side_B * side_B);
            // }

            // double A_bigger_2 = Math.Abs(side_A) / 2.0;
            // double B_bigger_2 = Math.Abs(side_B) / 2.0;

            // double IsInCheck = Math.Abs((p.X - Center.X) * Math.Cos(Math.PI / 4.0 + Angle) / A_bigger_2 + (p.Y - Center.Y) * Math.Sin(Math.PI / 4.0 + Angle) / B_bigger_2) + Math.Abs((p.X - Center.X) * Math.Sin(Math.PI / 4.0 + Angle) / (-A_bigger_2) + (p.Y - Center.Y) * Math.Cos(Math.PI / 4.0 + Angle) / B_bigger_2);

            // if (IsInCheck + eps <= Math.Sqrt(2.0) || IsInCheck - eps <= Math.Sqrt(2.0))
            //     return true;
            // else
            //     return false;

            Vector RotatePoint = new Vector(p.X * Math.Cos(Angle) + p.Y * Math.Sin(Angle), -p.X * Math.Sin(Angle) + p.Y * Math.Cos(Angle));
            if (Gabarit.l - RotatePoint.X < eps && Gabarit.r - RotatePoint.X > eps && Gabarit.t - RotatePoint.Y > eps && Gabarit.b - RotatePoint.Y < eps)
                return true;
            else return false;
        }

        public IFigure Move(Vector delta)
        {
            Vector deltaCenter = Center + delta;
            return new Square(Angle, deltaCenter, Scale);
        }

        public IFigure Reflection(bool axe)
        {
            // if (axe) // Вертикальное отражение
            // {
            //     newAngle = Math.PI - Angle;
            //     Vector newScale = new(Scale.X, -Scale.Y);
            //     return new Ellipse(newAngle, Center, newScale);
            // }
            // else // Горизонтальное отражение
            // {
            //     newAngle = 2.0 * Math.PI - Angle;
            //     Vector newScale = new(-Scale.X, Scale.Y);
            //     return new Ellipse(newAngle, Center, newScale);
            // }

            // if (axe)
            // {
            //     return this.Rotate(-Math.PI + 2 * Angle);
            // }
            // else
            // {
            //     return this.Rotate(-2 * Angle);
            // }
            double newAngle = 0;
            if (axe) // Вертикально
            {
                newAngle = (Angle - Math.PI / 2) % Math.PI;
            }
            else // Горизонтально
            {
                newAngle = (Angle + Math.PI / 2) % Math.PI;
            }
            Vector newScale = new Vector(Scale.X * Math.Cos(newAngle) + Scale.Y * Math.Sin(newAngle),
            Scale.X * Math.Sin(newAngle) + Scale.Y * Math.Cos(newAngle));

            return new Square(newAngle, Center, newScale);
        }

        public IFigure Rotate(double delta)
        {
            // double newAngle = (Angle + delta) % Math.PI;
            // double eps = 0.1;
            // Vector newScale;
            // if (Math.Abs(newAngle) % Math.PI / 2 < eps)
            // {
            //     newScale = new(Scale.Y, Scale.X);
            //     return new Square(newAngle, Center, newScale);
            // }
            // else
            // {
            //     newScale = new(Scale.X * Math.Cos(newAngle), Scale.Y * Math.Cos(newAngle));
            //     return new Square(newAngle, Center, newScale);
            // }
            double newAngle = (Angle + delta) % (2 * Math.PI);
            //if (Math.Abs(newAngle) > 2 * Math.PI) newAngle = newAngle % (2 * Math.PI);
            //else if (Math.Abs(newAngle) == 2 * Math.PI) return new Square(Angle, Center, Scale);
            Vector newScale = new Vector(Scale.X * Math.Cos(newAngle) + Scale.Y * Math.Sin(newAngle),
            Scale.X * Math.Sin(newAngle) + Scale.Y * Math.Cos(newAngle));

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
