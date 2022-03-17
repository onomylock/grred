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
        private readonly IEnumerable<Vector> _Points;

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
            return Math.Asin((p2.Y - p1.Y) / Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.X, 2)));
        }

        private Vector SetInputCenter(IEnumerable<Vector> Points)
        {
            Vector p1 = Points.ElementAt(0);
            Vector p2 = Points.ElementAt(1);
            Vector p3 = Points.ElementAt(2);

            return new Vector((p1.X + p2.X + p3.X) / 3, (p1.Y + p2.Y + p3.X) / 3);
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
            Vector pa = new Vector(p.X - p1.X, p.Y - p.Y);
            Vector ab = new Vector(p2.X - p1.X, p2.Y - p1.Y);
            Vector ac = new Vector(p3.X - p1.X, p3.Y - p1.Y);
            double u = ScalarMult(pa, ab) / Math.Pow(VectorModul(ab), 2);
            double v = ScalarMult(pa, ac) / Math.Pow(VectorModul(ac), 2);

            if (u < 0 || u > 1 || v < 0 || v > 1 || v + u > 1) return false;
            else return true;
        }

        public IFigure Move(Vector delta)
        {
            // for (int i = 0; i < Int32(_Points.Count); i++)
            // {
            //     _Points[i] += delta;
            // }
            return new Triangle(_Points);
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
