namespace GrRed.Geometry.Domain
{
    class Square : IFigure
    {

        private readonly string _TypeName;
        private readonly Vector _Center = new Vector(0, 0); // тоже для шаблона (для первой перегрузки конструктора)
        private readonly double _Angle;
        private readonly Vector _Scale = new Vector(1.0, 1.0); // для шаблона (для первой перегрузки конструктора)
        private readonly (double l, double t, double r, double b) _Gabarit; // договоримся, что x0, y0, x1, y1 соответственно


        // Перегрузка №0 конструктора, когда создаём фигуру новую-новую
        public Square()
        {
            _TypeName = "Square";
            _Angle = 0;
            _Gabarit = (-1.0, -1.0, 1.0, 1.0);
        }

        // Перегрузка №1 конструктора, когда создаём новую фигуру, являющуюся изменением старой
        public Square(Vector _Center, (double l, double t, double r, double b) _Gabarit)
        {
            _TypeName = "Square";
            _Angle = 0;
            this._Center = _Center;
            this._Gabarit = _Gabarit;
        }

        // Перегрузка №2 конструктора, когда создаём новую фигуру, являющуюся изменением старой
        public Square(double _Angle, Vector _Center, Vector _Scale, (double l, double t, double r, double b) _Gabarit)
        {
            _TypeName = "Square";
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
