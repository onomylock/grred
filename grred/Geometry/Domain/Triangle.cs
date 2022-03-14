using System;

using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace GrRed.Geometry.Domain
{
    [DataContract]
    public class Triangle : IFigure
    {

        private readonly Vector _Center = new(1.0, 1.0); // Центр тяжести
        private readonly double _Angle = 0.0;
        private readonly Vector _Scale = new(0.866, 0.75);

        public Triangle() { }

        [JsonConstructor]
        public Triangle(double Angle, Vector Center, Vector Scale)
        {
            _Center = Center;
            _Angle = Angle;
            _Scale = Scale;
        }

        public string TypeName => "Triangle";
        [DataMember]
        public double Angle => _Angle;
        [DataMember]
        public Vector Center => _Center;
        [DataMember]
        public Vector Scale => _Scale;

        public (double l, double t, double r, double b) Gabarit =>
            (Center.X - Scale.X,         Center.Y + Scale.Y * 2.0 / 3.0,       Center.X,       Center.Y - Scale.Y * 2.0 / 3.0);


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

        public IFigure Intersection(IFigure fig2)
        {
            throw new NotImplementedException();
        }

        public bool IsIn(Vector p, double eps)
        {
            throw new NotImplementedException();
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
