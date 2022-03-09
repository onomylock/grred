using System;

using Newtonsoft.Json;
using System.Runtime.Serialization;

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
            throw new NotImplementedException();
        }

        public IFigure Intersection(IFigure fig2)
        {
            throw new NotImplementedException();
        }

        public bool IsIn(Vector p, double eps)
        {
            if (p.X + eps >= Gabarit.l && p.X + eps <= Gabarit.r && p.Y + eps <= Gabarit.t && p.Y + eps >= Gabarit.b)
                if (p.X - eps >= Gabarit.l && p.X - eps <= Gabarit.r && p.Y - eps <= Gabarit.t && p.Y - eps >= Gabarit.b)
                    return true;
                else
                    return false;
            else
                return false;
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
