using System;

namespace GrRed.Geometry.Domain
{
    class Triangle : IFigure
    {

        private readonly Vector _Center = new(1.0, 1.0);
        private readonly double _Angle = 0.0;
        private readonly Vector _Scale = new(1.0, 1.0);

        public Triangle() { }

        public Triangle(double Angle, Vector Center, Vector Scale)
        {
            _Center = Center;
            _Angle = Angle;
            _Scale = Scale;
        }

        public string TypeName => "Triangle";
        public double Angle => _Angle;
        public Vector Center => _Center;
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
