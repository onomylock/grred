namespace GrRed.Geometry.Domain
{
    class Circle : IFigure
    {

        private readonly string _TypeName;
        private readonly Vector _Center = new Vector(0, 0); // тоже для шаблона (для первой перегрузки конструктора)
        private readonly double _Angle;
        private readonly Vector _Scale = new Vector(1.0, 1.0); // для шаблона (для первой перегрузки конструктора)
        private readonly (double l, double t, double r, double b) _Gabarit; // договоримся, что x0, y0, x1, y1 соответственно


        // Перегрузка №0 конструктора, когда создаём фигуру новую-новую
        public Circle()
        {
            _TypeName = "Circle";
            _Angle = 0;
            _Gabarit = (-1.0, -1.0, 1.0, 1.0);
        }

        // Перегрузка №1 конструктора, когда создаём новую фигуру, являющуюся изменением старой
        public Circle(Vector _Center, (double l, double t, double r, double b) _Gabarit)
        {
            _TypeName = "Circle";
            _Angle = 0;
            this._Center = _Center;
            this._Gabarit = _Gabarit;
        }

        // Перегрузка №2 конструктора, когда создаём новую фигуру, являющуюся изменением старой
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
            // Проверяем x^2 + y^2 +- eps <= R^2

            if (p.X * p.X + p.Y * p.Y + eps <= (_Gabarit.r - _Gabarit.l) * (_Gabarit.r - _Gabarit.l))
                return true;
            else if (p.X * p.X + p.Y * p.Y - eps <= (_Gabarit.r - _Gabarit.l) * (_Gabarit.r - _Gabarit.l))
                return true;
            else
                return false;

            // Да, тут можно было бы два ифа в одной строке, но это нечитабельно было бы
        }

        public IFigure Move(Vector delta)
        {
            Vector deltaCenter = new Vector (_Center.X + delta.X, _Center.Y + delta.Y);
            Circle circle = new Circle(deltaCenter, (_Gabarit.l + delta.X, _Gabarit.t + delta.Y, _Gabarit.r + delta.X, _Gabarit.b + delta.Y));
            return circle;
        }

        public IFigure Reflection(Vector axe) // Отражение круга ничего не меняет, поэтому функция отражения круга возвращает отражаемый круг
        {
            Circle circle = new Circle(_Angle, _Center, _Scale, _Gabarit);
            return circle;
        }

        public IFigure Rotate(Vector delta)   // Поворот круга ничего не меняет, поэтому функция поворота круга возвращает поворачиваемый круг
        {
            Circle circle = new Circle(_Angle, _Center, _Scale, _Gabarit);
            return circle;
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
