namespace GrRed.Geometry.Domain
{
    class Circle : IFigure
    {

        // Ваня делал отсюда
        private readonly string _TypeName;
        private readonly Vector _Center = new Vector(0, 0); // тоже для шаблона (для первой перегрузки конструктора)
        private readonly double _Angle;
        private readonly Vector _Scale = new Vector(1.0, 1.0); // для шаблона (для первой перегрузки конструктора)
        private readonly (double l, double t, double r, double b) _Gabarit;


        // Перегрузка конструктора, когда создаём фигуру новую-новую
        public Circle()
        {
            _TypeName = "Circle";
            _Angle = 0;
            _Gabarit = (1.0, 1.0, 1.0, 1.0);
        }


        // Перегрузка конструктора, когда создаём новую фигуру, являющуюся изменением старой
        public Circle(double _Angle, Vector _Center, Vector _Scale, (double l, double t, double r, double b) _Gabarit)
        {
            _TypeName = "Circle";
            this._Center = _Center;
            this._Angle = _Angle;
            this._Scale = _Scale;
            this._Gabarit = _Gabarit;
        }


        public string TypeName => _TypeName;
        public double Angle => _Angle;
        public Vector Center => _Center;
        public Vector Scale => _Scale;
        public (double l, double t, double r, double b) Gabarit => _Gabarit;

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
            Circle circle = new Circle();
            return circle;
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
