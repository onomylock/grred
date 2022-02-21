namespace GrRed.Geometry.Domain
{
    class Circle : IFigure
    {

        private readonly string _TypeName = "Circle";
        private readonly Vector _Center = new (1.0, 1.0);
        private readonly double _Angle = 0.0;
        private readonly Vector _Scale = new (1.0, 1.0);
        private readonly (double l, double t, double r, double b) _Gabarit = new(0.0, 1.0, 2.0, 0.0);


        public Circle() { } // Оказывается, пустой конструктор нужен, потому что иначе нельзя
                            // будет создать, скажем, Circle mox = new Circle().
                            // Это можно было бы делать без пустого конструктора,
                            // если бы не было конструктора с параметрами ниже


        // Перегрузка конструктора, когда создаём новую фигуру, являющуюся изменением старой
        public Circle(double _Angle, Vector _Center, Vector _Scale, (double l, double t, double r, double b) _Gabarit)
        { // Здесь черту у параметров конструктора решил не убирать, потому что опасаюсь, что могут возникнуть проблемы
          // из-за того, что в интерфейсе они без черты
            this._Center = _Center;
            this._Angle = _Angle;
            this._Scale = _Scale;
            this._Gabarit = _Gabarit;
        }


        public string TypeName => _TypeName;       // Здесь я оставил public потому что иначе на третьей строке опять ругается
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
