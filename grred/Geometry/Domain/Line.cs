using System;

using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace GrRed.Geometry.Domain
{
    class Line : IFigure
    {
        public Line(IEnumerable<Vector> Points)
        {
            this.Points = Points.ToArray();
            Center = new Vector(this.Points[0] + (this.Points[1] - this.Points[0]) / 2);
            Angle = SetInputAngle(this.Points);
            Scale = new Vector(Center - this.Points[0]);
        }
        public string TypeName => "Line";

        public Vector[] Points { get; }

        public Vector Center { get; }

        public double Angle { get; }
        public Vector Scale { get; }
        public (double l, double t, double r, double b) Gabarit => throw new NotImplementedException();

        public void Draw(IGraphic graphic)
        {
            graphic.AddLines(Points);
        }

        private double SetInputAngle(Vector[] Points)
        {
            if (Points[1].Y >= Points[0].Y) return Math.Acos((Points[1].X - Points[0].X) / VectorModul(Points[1] - Points[0]));
            else return -Math.Acos((Points[1].X - Points[0].X) / VectorModul(Points[1] - Points[0]));
        }
        private double VectorModul(Vector a) => Math.Sqrt(Math.Pow(a.X, 2) + Math.Pow(a.Y, 2));

        public IFigure Intersection(IFigure fig2)
        {
            throw new NotImplementedException();
        }

        public bool IsIn(Vector p, double eps)
        {
            Vector div = new Vector(Points[1] - Points[0]);
            if (Angle % Math.PI / 2 < eps * 10e-5)
                if (Math.Abs(p.X - Center.X) < eps) return true;
                else return false;
            else if (Angle % Math.PI < eps * 10e-5)
                if (Math.Abs(p.Y - Center.Y) < eps) return true;
                else return false;
            else if (Math.Abs((p.X - Points[0].X) / div.X - (p.Y - Points[0].Y) / div.Y) < eps)
                return true;
            else return false;
        }

        public IFigure Move(Vector delta)
        {
            Vector[] newPoints = new Vector[2];
            newPoints[0] = new Vector(Points[0] + delta);
            newPoints[1] = new Vector(Points[1] + delta);
            return new Line(Points);
        }

        public IFigure Reflection(bool axe)
        {
            double newAngle = 0;
            Vector[] newPoints = new Vector[2];
            if (axe) // Вертикально
            {
                newAngle = (Angle - Math.PI / 2) % Math.PI;
            }
            else // Горизонтально
            {
                newAngle = (Angle + Math.PI / 2) % Math.PI;
            }
            newPoints[0] = new Vector(Points[0].X * Math.Cos(newAngle) + Points[0].Y * Math.Sin(newAngle),
            Points[0].X * Math.Sin(newAngle) + Points[0].Y * Math.Cos(newAngle));
            newPoints[1] = new Vector(Points[1].X * Math.Cos(newAngle) + Points[1].Y * Math.Sin(newAngle),
            Points[1].X * Math.Sin(newAngle) + Points[1].Y * Math.Cos(newAngle));

            return new Line(newPoints);
        }

        public IFigure Rotate(double delta)
        {
            Vector[] newPoints = new Vector[2];
            double newAngle = (Angle + delta) % Math.PI;
            newPoints[0] = new Vector(Points[0].X * Math.Cos(newAngle) + Points[0].Y * Math.Sin(newAngle),
            Points[0].X * Math.Sin(newAngle) + Points[0].Y * Math.Cos(newAngle));
            newPoints[1] = new Vector(Points[1].X * Math.Cos(newAngle) + Points[1].Y * Math.Sin(newAngle),
            Points[1].X * Math.Sin(newAngle) + Points[1].Y * Math.Cos(newAngle));

            return new Line(newPoints);
        }

        public IFigure SetScale(double dx, double dy)
        {
            throw new NotImplementedException();
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