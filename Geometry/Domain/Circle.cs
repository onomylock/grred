namespace GrRed.Geometry.Domain
{
    class Circle : IFigure
    {

        // Ваня делал отсюда
        private readonly string TypeName;
        private readonly Vector Center;
        private readonly double Angle;
        private readonly Vector Scale;//для шаблона
        private readonly (double l, double t, double r, double b) _Gabarit;

        public Circle(string TypeName, double Angle, Vector Center, Vector Scale, (double l, double t, double r, double b) Gabarit)
        {
            this.TypeName = TypeName;
            this.Angle = Angle;
            this.Center = Center;
            this.Scale = Scale;
            this.Gabarit = Gabarit;
        }

        //public string TypeName => _TypeName;
        //public double Angle => _Angle;
        //public Vector Center => _Center;
        //public Vector Scale => _Scale;

        // До сюда. В Triangle.cs, Square.cs и в Circle.cs

        /*public string TypeName => throw new NotImplementedException();

        public Vector Center => throw new NotImplementedException();

        public double Angle => throw new NotImplementedException();

        public Vector Scale => throw new NotImplementedException();

        public (double l, double t, double r, double b) Gabarit => throw new NotImplementedException();*/

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
