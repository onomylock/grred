using System;

using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace GrRed.Geometry.Domain
{
    class Line : IFigure
    {
        public Line(double angle, Vector center, Vector scale)
        {
            Center = center;
            Angle = angle;
            Scale = scale;
            Points = new Vector[2] { Center, Scale };
            //Angle = SetInputAngle(this.Points);

            // Points[0] = Center;
            // Points[1] = Scale;
        }
        public Line(IEnumerable<Vector> Points, double Angle)
        {
            this.Points = Points.ToArray();
            // Center = new Vector(this.Points[0] + (this.Points[1] - this.Points[0]) / 2);
            // Scale = new Vector(Center - this.Points[0]);
            Center = this.Points[0];
            Scale = this.Points[1];
            this.Angle = Angle;
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

        //public void Draw(IGraphic graphic, object o, object oo)
        //{
        //    graphic.AddLines(Points);
        //    graphic.conturColor = o;
        //    //graphic.fillColor = oo;
        //}

        private double SetInputAngle(Vector[] Points, bool axe)
        {
            if (axe)
                if (Points[1].Y >= Points[0].Y) return Math.Acos((Math.Abs(Points[1].Y - Points[0].Y)) / VectorModul(Points[1] - Points[0]));
                else return -Math.Acos((Math.Abs(Points[1].Y - Points[0].Y)) / VectorModul(Points[1] - Points[0]));
            else
                if (Points[1].X >= Points[0].X) return Math.Asin((Points[1].Y - Points[0].Y) / VectorModul(Points[1] - Points[0]));
                else return -Math.Asin((Points[1].Y - Points[0].Y) / VectorModul(Points[1] - Points[0]));

            //if (Points[1].X >= Points[0].X) return Math.Asin((Points[1].Y - Points[0].Y) / VectorModul(Points[1] - Points[0]));
            //else return -Math.Asin((Points[1].Y - Points[0].Y) / VectorModul(Points[1] - Points[0]));
            //return Math.Asin((Points[1].Y - Points[0].Y) / VectorModul(Points[1] - Points[0]));
        }
        private double VectorModul(Vector a) => Math.Sqrt(Math.Pow(a.X, 2) + Math.Pow(a.Y, 2));

        public IFigure Intersection(IFigure fig2)
        {
            throw new NotImplementedException();
        }

        public bool IsIn(Vector p, double eps)
        {
            double line = VectorModul(Points[1] - Points[0]);
            double line1 = VectorModul(Points[0] - p);
            double line2 = VectorModul(Points[1] - p);

            if (Math.Abs(line - (line1 + line2)) < eps) return true;
            else return false;
        }

        public IFigure Move(Vector delta)
        {
            Vector[] newPoints = new Vector[2] { (Points[0] + delta), (Points[1] + delta) };
            return new Line(newPoints, Angle);
        }

        public IFigure Reflection(bool axe)
        {
            double newAngle = SetInputAngle(Points, axe);
            Vector[] newPoints = new Vector[2];

            if (axe) // Вертикально
            {
                if (Points[0].Y >= Points[1].Y && Points[0].X >= Points[1].X) newAngle = -2 * newAngle;
                else if (Points[0].Y >= Points[1].Y && Points[0].X < Points[1].X) newAngle = 2 * newAngle;
                else if (Points[0].Y < Points[1].Y && Points[0].X < Points[1].X) newAngle = 2 * newAngle;
                else newAngle = - 2 * newAngle;


                newPoints[0] = Points[0];

               
                newPoints[1] = new Vector(Points[0].X + (Points[1].X - Points[0].X) * Math.Cos(newAngle) - (Points[1].Y - Points[0].Y) * Math.Sin(newAngle),
                Points[0].Y + (Points[1].X - Points[0].X) * Math.Sin(newAngle) + (Points[1].Y - Points[0].Y) * Math.Cos(newAngle));
            }
            else // Горизонтально
            {
                newAngle = 2 * Math.PI - 2 * newAngle;

                newPoints[0] = Points[0];
                newPoints[1] = new Vector(Points[0].X + (Points[1].X - Points[0].X) * Math.Cos(newAngle) - (Points[1].Y - Points[0].Y) * Math.Sin(newAngle),
                Points[0].Y + (Points[1].X - Points[0].X) * Math.Sin(newAngle) + (Points[1].Y - Points[0].Y) * Math.Cos(newAngle));
            }

            return new Line(newAngle, newPoints[0], newPoints[1]);
        }

        public IFigure Rotate(double delta)
        {
            Vector[] newPoints = new Vector[2];
            double newAngle = (Angle + delta) % Math.PI;
            //newPoints[0] = new Vector(Points[0].X * Math.Cos(newAngle) + Points[0].Y * Math.Sin(newAngle),
            //Points[0].X * Math.Sin(newAngle) + Points[0].Y * Math.Cos(newAngle));
            newPoints[0] = Points[0];

            newPoints[1] = new Vector(Points[0].X + (Points[1].X - Points[0].X) * Math.Cos(newAngle) - (Points[1].Y - Points[0].Y) * Math.Sin(newAngle),
            Points[0].Y + (Points[1].X - Points[0].X) * Math.Sin(newAngle) + (Points[1].Y - Points[0].Y) * Math.Cos(newAngle));

            return new Line(newPoints, Angle);
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