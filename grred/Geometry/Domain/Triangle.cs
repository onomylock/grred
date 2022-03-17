using System;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace GrRed.Geometry.Domain
{
    [DataContract]
    class Triangle : IFigure
    {

        private readonly Vector _Center = new(1.0, 1.0); // Центр тяжести
        private readonly double _Angle = 0.0;
        private readonly Vector _Scale = new(0.866, 0.75);
        private IEnumerable<Vector> _Points;

        public Triangle() { }

        [JsonConstructor]
        public Triangle(double Angle, Vector Center, Vector Scale)
        {
            _Center = Center;
            _Angle = Angle;
            _Scale = Scale;
        }

        public Triangle(IEnumerable<Vector> Points)
        {
            _Points = Points;
            _Center = SetInputCenter(Points);
            _Angle = SetInputAngle(Points);
        }

        public string TypeName => "Triangle";
        [DataMember]
        public double Angle => _Angle;
        [DataMember]
        public Vector Center => _Center;
        [DataMember]
        public Vector Scale => _Scale;

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

            //return new Vector((p1.X + p2.X + p3.X) / 3, (p1.Y + p2.Y + p3.X) / 3);
            return (p1 + p2 + p3) / 3.0;
        }

        public IFigure Intersection(IFigure fig2)
        {
            throw new NotImplementedException();
        }

        public bool IsIn(Vector p, double eps)
        {
            Vector p1 = _Points.ElementAt(0);
            Vector p2 = _Points.ElementAt(1);
            Vector p3 = _Points.ElementAt(2);
            Vector pa = p - p1;
            Vector ab = p2 - p1;
            Vector ac = p3 - p1;
            double u = ScalarMult(pa, ab) / Math.Pow(VectorModul(ab), 2);
            double v = ScalarMult(pa, ac) / Math.Pow(VectorModul(ac), 2);

            if (u < 0 || u > 1 || v < 0 || v > 1 || v + u > 1) return false;
            else return true;
        }

        public IFigure Move(Vector delta)
        {
            List<Vector> newPoints = new List<Vector>(3);
            int i = 0;
            foreach (Vector point in _Points)
            {
                newPoints[i] = point + delta;
                i++;
            }
            return new Triangle(newPoints);
        }

        public IFigure Reflection(bool axe)
        {
            List<Vector> newPoints = new List<Vector>();
            newPoints.Add(_Points.ElementAt(0));
            newPoints.Add(_Points.ElementAt(1));
            newPoints.Add(_Points.ElementAt(2));

            if (axe) //Вертикальное
            {
                for (int i = 0; i < newPoints.Count; i++)
                {
                    if (newPoints[i].Y > Center.Y) newPoints[i] = new Vector(newPoints[i].Y, newPoints[i].Y - 2 * Center.Y);
                    else if (newPoints[i].Y < Center.Y) newPoints[i] = new Vector(newPoints[i].Y, newPoints[i].Y + 2 * Center.Y);
                }
                return new Triangle(newPoints);
            }
            else //Горизонтальное
            {
                for (int i = 0; i < newPoints.Count; i++)
                {
                    if (newPoints[i].X > Center.X) newPoints[i] = new Vector(newPoints[i].X - 2 * Center.X, newPoints[i].Y);
                    else if (newPoints[i].X < Center.X) newPoints[i] = new Vector(newPoints[i].X + 2 * Center.X, newPoints[i].Y);
                }
                return new Triangle(newPoints);
            }
        }

        public IFigure Rotate(double delta)
        {
            double newAngle = Angle + delta;
            Vector p1 = new Vector(_Points.ElementAt(0).X * Math.Cos(newAngle) - _Points.ElementAt(0).Y * Math.Sin(newAngle),
            _Points.ElementAt(0).X * Math.Sin(newAngle) + _Points.ElementAt(0).Y * Math.Cos(newAngle));
            Vector p2 = new Vector(_Points.ElementAt(1).X * Math.Cos(newAngle) - _Points.ElementAt(1).Y * Math.Sin(newAngle),
            _Points.ElementAt(1).X * Math.Sin(newAngle) + _Points.ElementAt(1).Y * Math.Cos(newAngle));
            Vector p3 = new Vector(_Points.ElementAt(2).X * Math.Cos(newAngle) - _Points.ElementAt(2).Y * Math.Sin(newAngle),
            _Points.ElementAt(2).X * Math.Sin(newAngle) + _Points.ElementAt(2).Y * Math.Cos(newAngle));
            List<Vector> newPoints = new List<Vector>();
            newPoints.Add(p1);
            newPoints.Add(p2);
            newPoints.Add(p3);
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
