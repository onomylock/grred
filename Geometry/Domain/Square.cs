namespace GrRed.Geometry.Domain
{
    class Square : IFigure
    {

        private readonly string _TypeName = "Square";
        private readonly Vector _Center = new (1.0, 1.0);
        private readonly double _Angle = 0.0;
        private readonly Vector _Scale = new (1.0, 1.0);
        private readonly (double l, double t, double r, double b) _Gabarit = new(0.0, 1.0, 2.0, 0.0);


        public Square() { } // Оказывается, пустой конструктор нужен, потому что иначе нельзя
                            // будет создать, скажем, Circle mox = new Circle().
                            // Это можно было бы делать без пустого конструктора,
                            // если бы не было конструктора с параметрами ниже
                            // Перегрузка №0 конструктора, когда создаём фигуру новую-новую


        // Перегрузка конструктора, когда создаём новую фигуру, являющуюся изменением старой
        public Square(double Angle, Vector Center, Vector Scale, (double l, double t, double r, double b) Gabarit)
        { // Здесь черту у параметров конструктора решил не убирать, потому что опасаюсь, что могут возникнуть проблемы
          // из-за того, что в интерфейсе они без черты
            _Center = Center;
            _Angle = Angle;
            _Scale = Scale;
            _Gabarit = Gabarit;
        }

        public string TypeName => _TypeName;      // Здесь я оставил public потому что иначе на третьей строке опять ругается
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
