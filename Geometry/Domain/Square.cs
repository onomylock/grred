﻿namespace GrRed.Geometry.Domain
{
    class Square : IFigure
    {
        public string TypeName => throw new NotImplementedException();

        public Vector Center => throw new NotImplementedException();

        public double Angle => throw new NotImplementedException();

        public Vector Scale => throw new NotImplementedException();

        public (double l, double t, double r, double b) Gabarit => throw new NotImplementedException();

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

        public IFigure Reflection(Vector axe)
        {
            throw new NotImplementedException();
        }

        public IFigure Rotate(Vector delta)
        {
            throw new NotImplementedException();
        }

        public IFigure SetScale(double sx, double dy)
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
