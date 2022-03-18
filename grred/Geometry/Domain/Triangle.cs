using System;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace GrRed.Geometry.Domain
{
    [DataContract]
    public class Triangle : IFigure
    {
        public Vector[] Points = new Vector[3];

        public Triangle() { }

        [JsonConstructor]
        public Triangle(double angle, Vector center, Vector scale)
        {
            Center = center;
            Angle = angle;
            Scale = scale;
        }

        public Triangle(IEnumerable<Vector> Points)
        {
            this.Points = Points.ToArray();
            Center = SetInputCenter(Points);
            Angle = SetInputAngle(Points);
        }

        public string TypeName => "Triangle";
        [DataMember]
        public double Angle { get; }
        [DataMember]
        public Vector Center { get; }
        [DataMember]
        public Vector Scale { get; }

        public (double l, double t, double r, double b) Gabarit =>
            (Center.X - Scale.X, Center.Y + Scale.Y * 2.0 / 3.0, Center.X, Center.Y - Scale.Y * 2.0 / 3.0);

        private double ScalarMult(Vector a, Vector b) => a.X * b.X + a.Y * b.Y;

        private double VectorModul(Vector a) => Math.Sqrt(Math.Pow(a.X, 2) + Math.Pow(a.Y, 2));

        public void Draw(IGraphic graphic)
        {
            //Path path = new Path();
            //LineGrafic lineGrafic = new LineGrafic(paintingCanvas, path);
            List<Vector> vector2 = new List<Vector>();
            vector2.Add(Center);
            vector2.Add(Scale);
            vector2.Add(new Vector(Scale.X, Center.Y));
            graphic.AddLines(vector2);
            //object brush2 = "#ffc0cb";

            //Brush brush2 = Brushes.Firebrick;
            //graphic.FillPolygon(brush2);
        }

        private double SetInputAngle(IEnumerable<Vector> Points)
        {
            Vector p1 = Points.ElementAt(0);
            Vector p2 = Points.ElementAt(2);
            return Math.Asin((p2.Y - p1.Y) / VectorModul(p2 - p1));
        }

        private Vector SetInputCenter(IEnumerable<Vector> Points)
        {
            Vector p1 = Points.ElementAt(0);
            Vector p2 = Points.ElementAt(1);
            Vector p3 = Points.ElementAt(2);
            return (p1 + p2 + p3) / 3.0;
        }

        public IFigure Intersection(IFigure fig2)
        {
            throw new NotImplementedException();
        }

        public bool IsIn(Vector p, double eps)
        {
            Vector p1 = Points.ElementAt(0);
            Vector p2 = Points.ElementAt(1);
            Vector p3 = Points.ElementAt(2);

            double Sabc = (p2.X - p1.X) * (p3.Y - p1.Y) - (p3.X - p1.X) * (p2.Y - p1.Y);
            double Sabp = (p2.X - p1.X) * (p.Y - p1.Y) - (p.X - p1.X) * (p2.Y - p1.Y);
            double Sbcp = (p2.X - p.X) * (p3.Y - p.Y) - (p3.X - p.X) * (p2.Y - p.Y);
            double Sacp = (p.X - p1.X) * (p3.Y - p1.Y) - (p3.X - p1.X) * (p.Y - p1.Y);
            double sum = Sabp + Sbcp + Sacp;

            if (Sabc - sum < eps) return false;
            else return true;
        }

        public IFigure Move(Vector delta)
        {
            Vector[] newPoints = new Vector[3];
            for (int i = 0; i < newPoints.Count(); i++)
            {
                newPoints[i] = Points[i] + delta;
            }
            return new Triangle(newPoints);
        }

        public IFigure Reflection(bool axe)
        {
            Vector[] newPoints = new Vector[3];
            // newPoints.Add(_Points.ElementAt(0));
            // newPoints.Add(_Points.ElementAt(1));
            // newPoints.Add(_Points.ElementAt(2));

            if (axe) //Вертикальное
            {
                for (int i = 0; i < newPoints.Count(); i++)
                {
                    if (Points[i].Y > Center.Y) newPoints[i] = new Vector(Points[i].Y, Points[i].Y - 2 * Center.Y);
                    else if (Points[i].Y < Center.Y) newPoints[i] = new Vector(newPoints[i].Y, newPoints[i].Y + 2 * Center.Y);
                }
                return new Triangle(newPoints);
            }
            else //Горизонтальное
            {
                for (int i = 0; i < newPoints.Count(); i++)
                {
                    if (Points[i].X > Center.X) newPoints[i] = new Vector(Points[i].X - 2 * Center.X, Points[i].Y);
                    else if (Points[i].X < Center.X) newPoints[i] = new Vector(Points[i].X + 2 * Center.X, Points[i].Y);
                }
                return new Triangle(newPoints);
            }
        }

        public IFigure Rotate(double delta)
        {
            double newAngle = Angle + delta;
            Vector[] newPoints = new Vector[3];

            for (int i = 0; i < newPoints.Count(); i++)
            {
                newPoints[0] = new Vector(Points[0].X * Math.Cos(newAngle) - Points[0].Y * Math.Sin(newAngle),
                Points[0].X * Math.Sin(newAngle) + Points[0].Y * Math.Cos(newAngle));
            }
            return new Triangle(newPoints);
        }

        public IFigure SetScale(double dx, double dy)
        {
            Vector newScale = new(Scale.X + dx / 2, Scale.Y + dy / 2);
            return new Triangle(Angle, Center, newScale);
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
