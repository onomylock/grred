using System;

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
            // Проверяем (x-x0)^2/a^2 + (y-y0)^2/b^2 +- eps <= 1, но для повёрнутого эллипса (немного другая формула для более общего случая)

            double AxisX = (_Gabarit.r - _Center.X) / Math.Cos(_Angle); // Полуоси
            double AxisY = (_Gabarit.t - _Center.Y) / Math.Cos(_Angle); // повёрнутого эллипса

            if (  Math.Pow((p.X - _Center.X ) * Math.Cos(_Angle) + (p.Y - _Center.Y) * Math.Sin(_Angle), 2) / (AxisX * AxisX) + Math.Pow((- p.X + _Center.X) * Math.Sin(_Angle) + (p.Y - _Center.Y) * Math.Cos(_Angle), 2) / (AxisY * AxisY) + eps <= 1.0  )
                return true;
            else if (  Math.Pow((p.X - _Center.X) * Math.Cos(_Angle) + (p.Y - _Center.Y) * Math.Sin(_Angle), 2) / (AxisX * AxisX) + Math.Pow((-p.X + _Center.X) * Math.Sin(_Angle) + (p.Y - _Center.Y) * Math.Cos(_Angle), 2) / (AxisY * AxisY) - eps <= 1.0  )
                return true;
            else
                return false;

            // Да, тут можно было бы два ифа в одном ифе через ||, но это нечитабельно было бы
        }

        public IFigure Move(Vector delta)
        {
            Vector deltaCenter = new (_Center.X + delta.X, _Center.Y + delta.Y);
            Circle circle = new (_Angle, deltaCenter, _Scale, (_Gabarit.l + delta.X, _Gabarit.t + delta.Y, _Gabarit.r + delta.X, _Gabarit.b + delta.Y));
            return circle;
        }

        public IFigure Reflection(Vector axe) // Vector axe - что-то странное. Лучше бы булевскую переменную,
                                              // говорящую о горизонтальном или вертикальном отражении.
                                              // А так буду пока проверять axe.X == 0 - верт. отраж.
        {
            (double l, double t, double r, double b) newGabarit = _Gabarit;
            double newAngle;

            if (axe.X == 0) // Вертикальное отражение
            {
                newGabarit.t = _Gabarit.b;
                newGabarit.b = _Gabarit.t;
                newAngle = 2.0 * Math.PI - 2.0 * _Angle;  
            }
            else // Горизонтальное отражение
            {
                newGabarit.l = _Gabarit.r;
                newGabarit.r = _Gabarit.l;
                newAngle = Math.PI - 2.0 * _Angle;
            }

            Circle circle = new Circle(newAngle, _Center, _Scale, newGabarit);
            return circle;
        }

        public IFigure Rotate(double delta, bool clockwise)
        {
            (double l, double t, double r, double b) newGabarit = (0.0, 0.0, 0.0, 0.0);
            if (_Gabarit.t > _Center.Y)
            {
                newGabarit.t = (_Gabarit.t - _Center.Y) * Math.Cos(_Angle) + _Center.Y;
                newGabarit.b = - (_Center.Y - _Gabarit.b) * Math.Cos(_Angle) + _Center.Y;
            }
            else
            {
                newGabarit.b = (_Gabarit.b - _Center.Y) * Math.Cos(_Angle) + _Center.Y;
                newGabarit.t = - (_Center.Y - _Gabarit.t) * Math.Cos(_Angle) + _Center.Y;
            }

            if (_Gabarit.r > _Center.X)
            {
                newGabarit.r = (_Gabarit.r - _Center.X) * Math.Cos(_Angle) + _Center.X;
                newGabarit.l = - (_Center.X - _Gabarit.l) * Math.Cos(_Angle) + _Center.X;
            }
            else
            {
                newGabarit.l = (_Gabarit.l - _Center.X) * Math.Cos(_Angle) + _Center.X;
                newGabarit.r = - (_Center.X - _Gabarit.r) * Math.Cos(_Angle) + _Center.X;
            }

            double newAngle;
            if (!clockwise)
                newAngle = _Angle + delta;
            else
                newAngle = _Angle - delta;
            

            Circle circle = new Circle(newAngle, _Center, _Scale, newGabarit);

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
