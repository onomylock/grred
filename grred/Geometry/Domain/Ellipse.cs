using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;

namespace GrRed.Geometry.Domain
{
    [DataContract]
    class Ellipse : IFigure
    {
        // private readonly Vector _Center = new(1.0, 1.0);
        // private readonly double _Angle = 0.0;
        // private readonly Vector _Scale = new(1.0, 1.0);
        public Ellipse() { }

        [JsonConstructor]
        public Ellipse(double angle, Vector center, Vector scale)
        {
            Center = center;
            Angle = angle;
            Scale = scale;
            Gabarit = (Center.X - Scale.X, Center.Y + Scale.Y, Center.X + Scale.X, Center.Y - Scale.Y);
            Points = SetInputPoints();
        }

        public Ellipse(IEnumerable<Vector> Points)
        {
            this.Points = Points.ToArray();
            Scale = new Vector(Math.Abs(this.Points[0].X), Math.Abs(this.Points[1].Y));
            Center = SetInputCenter(this.Points);
            Angle = SetInputAngle();
            Gabarit = (Center.X - Scale.X, Center.Y + Scale.Y, Center.X + Scale.X, Center.Y - Scale.Y);
        }

        public string TypeName => "Ellipse";
        [DataMember]
        public Vector[] Points { get; }
        public double Angle { get; }
        [DataMember]
        public Vector Center { get; }
        [DataMember]
        public Vector Scale { get; }

        public (double l, double t, double r, double b) Gabarit { get; }

        private double VectorModul(Vector a) => Math.Sqrt(Math.Pow(a.X, 2) + Math.Pow(a.Y, 2));

        public void Draw(IGraphic graphic)
        {
            graphic.AddPolyArc(new ArraySegment<Vector>(Points, 0, 3));
        }

        private double SetInputAngle()
        {
            if (Points[1].X <= Center.X) return Math.Asin((this.Points[1].Y - Center.Y) / VectorModul(this.Points[1] - this.Center));
            else return -Math.Asin((this.Points[1].Y - Center.Y) / VectorModul(this.Points[1] - this.Center));
        }
        private Vector SetInputCenter(Vector[] Points) => new Vector(Points[2].X - Points[0].X, Points[1].Y - Points[3].Y);

        private Vector[] SetInputPoints()
        {
            Vector[] newPoints = new Vector[4];

            newPoints[0] = new Vector(Center.X - (Gabarit.l * Math.Cos(Angle) - Center.Y * Math.Sin(Angle)), Center.Y - (Gabarit.l * Math.Sin(Angle) + Center.Y * Math.Cos(Angle)));
            newPoints[1] = new Vector(Center.X - (Center.X * Math.Cos(Angle) - Gabarit.t * Math.Sin(Angle)), Center.Y + (Center.X * Math.Sin(Angle) + Gabarit.t * Math.Cos(Angle)));
            newPoints[2] = new Vector(Center.X + (Gabarit.r * Math.Cos(Angle) - Center.Y * Math.Sin(Angle)), Center.Y + (Gabarit.r * Math.Sin(Angle) + Center.Y * Math.Cos(Angle)));
            newPoints[3] = new Vector(Center.X + (Center.X * Math.Cos(Angle) - Gabarit.b * Math.Sin(Angle)), Center.Y - (Center.X * Math.Sin(Angle) + Gabarit.b * Math.Cos(Angle)));
            return newPoints;
        }

        public IFigure Intersection(IFigure fig2)
        {
            throw new NotImplementedException();
        }

        public bool IsIn(Vector p, double eps)
        {
            // double AxisX;
            // double AxisY;

            // // Вычисляем полуоси повёрнутого эллипса
            // if (Math.Abs(Angle) % Math.PI / 2.0 <= eps) // Случай, когда угол кратен пи/2 
            // {
            //     AxisY = Scale.X;
            //     AxisX = Scale.Y;
            // }
            // else                                      // Любой другой случай
            // {
            //     AxisX = Scale.X / Math.Cos(Angle);
            //     AxisY = Scale.Y / Math.Cos(Angle);
            // }

            // // Проверяем (x-x0)^2/a^2 + (y-y0)^2/b^2 +- eps <= 1, но для повёрнутого эллипса (немного другая формула для более общего случая)

            // double IsInCheck = Math.Pow((p.X - Center.X) * Math.Cos(Angle) + (p.Y - Center.Y) * Math.Sin(Angle), 2) / (AxisX * AxisX) + Math.Pow((-p.X + Center.X) * Math.Sin(Angle) + (p.Y - Center.Y) * Math.Cos(Angle), 2) / (AxisY * AxisY);

            // if (IsInCheck + eps <= 1.0 || IsInCheck - eps <= 1.0)
            //     return true;
            // else
            //     return false;

            Vector RotatePoint = new Vector(p.X * Math.Cos(Angle) + p.Y * Math.Sin(Angle), -p.X * Math.Sin(Angle) + p.Y * Math.Cos(Angle));
            double a = Math.Sqrt(Math.Pow(Center.X - Points[0].X, 2) + Math.Pow(Center.Y - Points[0].Y, 2));
            double b = Math.Sqrt(Math.Pow(Center.X - Points[1].X, 2) + Math.Pow(Center.Y - Points[1].Y, 2));
            //double a = Scale.X;
            //double b = Scale.Y;
            double ellipseEq = Math.Pow(RotatePoint.X / a, 2) + Math.Pow(RotatePoint.Y / b, 2);

            if (ellipseEq - 1 <= eps) return true;
            else return false;
        }

        public IFigure Move(Vector delta)
        {
            Vector deltaCenter = Center + delta;
            return new Ellipse(Angle, deltaCenter, Scale);
        }

        public IFigure Reflection(bool axe)
        {
            double newAngle = 0;
            if (axe) // Вертикально
            {
                newAngle = (Angle + Math.PI / 2) % (2 * Math.PI);
            }
            else // Горизонтально
            {
                newAngle = (Angle - Math.PI / 2) % (2 * Math.PI);
            }
            Vector newScale = new Vector(Scale.X * Math.Cos(newAngle) + Scale.Y * Math.Sin(newAngle),
            Scale.X * Math.Sin(newAngle) + Scale.Y * Math.Cos(newAngle));

            return new Ellipse(newAngle, Center, newScale);

        }

        public IFigure Rotate(double delta)
        {
            // double newAngle = Angle + delta;
            // double eps = 0.1;
            // Vector newScale;
            // double AxisX;
            // double AxisY;

            // if (Math.Abs(Math.PI / 2.0 + Angle) % Math.PI <= eps) // Случай, когда угол кратен пи/2
            // {
            //     AxisY = Scale.Y;
            //     AxisX = Scale.X;
            // }
            // else                                      // Любой другой случай
            // {
            //     AxisX = Scale.X / Math.Cos(Angle);
            //     AxisY = Scale.Y / Math.Cos(Angle);
            // }

            // if (Math.Abs(Math.PI / 2.0 + newAngle) % Math.PI <= eps) // Случай, когда угол кратен пи/2 
            // {
            //     newScale = new(AxisY, AxisX);
            //     return new Ellipse(newAngle, Center, newScale);
            // }
            // else
            // {
            //     newScale = new(AxisX * Math.Cos(newAngle), AxisY * Math.Cos(newAngle));
            //     return new Ellipse(newAngle, Center, newScale);
            // }

            // double newAngle = Angle + delta;
            // if (Math.Abs(newAngle) >= 2 * Math.PI) newAngle = newAngle % (2 * Math.PI);
            // Vector[] newPoints = new Vector[4];
            // for (int i = 0; i < Points.Count(); i++)
            // {
            //     newPoints[i] = new Vector(Points[i].X * Math.Cos(newAngle) + Points[i].Y * Math.Sin(newAngle),
            //     -Points[i].X * Math.Sin(newAngle) + Points[i].Y * Math.Cos(newAngle));
            // }
            // return new Ellipse(newPoints);
            double newAngle = Angle + delta;
            if (Math.Abs(newAngle) >= 2 * Math.PI) newAngle = newAngle % (2 * Math.PI);
            Vector newScale = new Vector(Scale.X * Math.Cos(newAngle) + Scale.Y * Math.Sin(newAngle),
            Scale.X * Math.Sin(newAngle) + Scale.Y * Math.Cos(newAngle));

            return new Ellipse(newAngle, Center, newScale);
        }

        public IFigure SetScale(double dx, double dy)
        {
            Vector newScale = new(Scale.X + dx / 2.0, Scale.Y + dy / 2.0);
            return new Ellipse(Angle, Center, newScale);
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
