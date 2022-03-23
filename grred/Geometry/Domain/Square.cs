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
            Center = scale;
            Angle = angle;
            Scale = center - scale;
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

        //public void Draw(IGraphic graphic, object o, object oo)
        //{
        //    graphic.AddLines(Points);
        //    graphic.conturColor = o;
        //    graphic.fillColor = oo;
        //}

        // Методы задания параметров класса

        private Vector[] SetInputPoints()
        {
            Vector[] newPoints = new Vector[4];

            // newPoints[0] = new Vector(Center.X - (Gabarit.l * Math.Cos(Angle) - Gabarit.b * Math.Sin(Angle)), Center.Y - (Gabarit.l * Math.Sin(Angle) + Gabarit.b * Math.Cos(Angle)));
            // newPoints[1] = new Vector(Center.X - (Gabarit.l * Math.Cos(Angle) - Gabarit.t * Math.Sin(Angle)), Center.Y + (Gabarit.l * Math.Sin(Angle) + Gabarit.t * Math.Cos(Angle)));
            // newPoints[2] = new Vector(Center.X + (Gabarit.r * Math.Cos(Angle) - Gabarit.t * Math.Sin(Angle)), Center.Y + (Gabarit.r * Math.Sin(Angle) + Gabarit.t * Math.Cos(Angle)));
            // newPoints[3] = new Vector(Center.X + (Gabarit.r * Math.Cos(Angle) - Gabarit.b * Math.Sin(Angle)), Center.Y - (Gabarit.r * Math.Sin(Angle) + Gabarit.b * Math.Cos(Angle)));
            newPoints[0] = new Vector(Gabarit.l, Gabarit.b);
            newPoints[1] = new Vector(Gabarit.l, Gabarit.t);
            newPoints[2] = new Vector(Gabarit.r, Gabarit.t);
            newPoints[3] = new Vector(Gabarit.r, Gabarit.b);

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
            double avgX = 0, avgY = 0;

            for (int i = 0; i < 4; i++)
            {
                avgX += Points[i].X;
                avgY += Points[i].Y;
            }

            return new Vector(avgX / 4, avgY / 4);
            // double n = 0;
            // if ((p2.Y - p1.Y) != 0)
            // {
            //     double q = (p3.X - p1.X) / (p1.Y - p3.Y);
            //     double sn = (p2.X - p4.X) + (p2.Y - p4.Y) * q;
            //     double fn = (p3.X - p1.X) + (p2.Y - p1.Y) * q;
            //     n = fn / sn;
            // }
            // else
            // {
            //     n = (p2.Y - p1.Y) / (p2.Y - p4.Y);
            // }
            // return new Vector(p2.X + (p4.X - p2.X) * n, p2.Y + (p4.Y - p2.X) * n);

        }

        // Внешние методы класса
        public IFigure Intersection(IFigure fig2)
        {
            throw new NotImplementedException();
        }

        public bool IsIn(Vector p, double eps)
        {
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
            // double newAngle = 0;
            // if (axe) // Вертикально
            // {
            //     //newAngle = (Angle - Math.PI / 2) % Math.PI;
            //     newAngle = -Angle % Math.PI;
            // }
            // else // Горизонтально
            // {
            //     newAngle = (Math.PI - Angle) % (Math.PI);
            // }
            // Vector newScale = new Vector(Scale.X * Math.Cos(newAngle) + Scale.Y * Math.Sin(newAngle),
            // Scale.X * Math.Sin(newAngle) + Scale.Y * Math.Cos(newAngle));

            // return new Square(newAngle, Center, newScale);

            double newAngle = 0;
            Vector[] newPoints = new Vector[4];
            if (axe) // Вертикально
            {
                newAngle = -Angle % Math.PI;
                // if (Angle % (Math.PI / 2) == 0) newAngle = - Angle;
                // newAngle = (-3 * Math.PI / 2 - Angle) % Math.PI;
                for (int i = 0; i < 4; i++)
                    newPoints[i] = new Vector(Points[i].X, Points[i].Y * Math.Cos(newAngle));
            }
            else // Горизонтально
            {
                newAngle = (Math.PI - Angle) % Math.PI;
                for (int i = 0; i < 4; i++)
                    newPoints[i] = new Vector(Points[i].X * Math.Cos(newAngle), Points[i].Y);
            }
            Vector newScale = new Vector(Scale.X * Math.Cos(newAngle) + Scale.Y * Math.Sin(newAngle),
            Scale.X * Math.Sin(newAngle) + Scale.Y * Math.Cos(newAngle));

            return new Square(newPoints);
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
