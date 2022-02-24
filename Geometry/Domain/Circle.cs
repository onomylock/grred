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


        public Circle() { } 

        // Перегрузка конструктора, когда создаём новую фигуру, являющуюся изменением старой
        public Circle(string TypeName, double Angle, Vector Center, Vector Scale, (double l, double t, double r, double b) Gabarit)
        {
            _TypeName = TypeName;
            _Center = Center;
            _Angle = Angle;
            _Scale = Scale;
            _Gabarit = Gabarit;
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
            // Проверяем (x-x0)^2/a^2 + (y-y0)^2/b^2 +- eps <= 1, но для повёрнутого эллипса (немного другая формула для более общего случая)

            double AxisX = (Gabarit.r - Center.X) / Math.Cos(Angle); // Полуоси
            double AxisY = (Gabarit.t - Center.Y) / Math.Cos(Angle); // повёрнутого эллипса
            double IsInCheck = Math.Pow((p.X - Center.X) * Math.Cos(Angle) + (p.Y - Center.Y) * Math.Sin(Angle), 2) / (AxisX * AxisX) + Math.Pow((-p.X + Center.X) * Math.Sin(Angle) + (p.Y - Center.Y) * Math.Cos(Angle), 2) / (AxisY * AxisY);

            if (IsInCheck + eps <= 1.0 || IsInCheck - eps <= 1.0)
                return true;
            else
                return false;

            // Да, тут можно было бы два ифа в одном ифе через ||, но это нечитабельно было бы
        }

        public IFigure Move(Vector delta)
        {
            Vector deltaCenter = Center + delta;
            Circle circle = new (TypeName, Angle, deltaCenter, Scale, (Gabarit.l + delta.X, Gabarit.t + delta.Y, Gabarit.r + delta.X, Gabarit.b + delta.Y));
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
                newGabarit.t = Gabarit.b;
                newGabarit.b = Gabarit.t;
                newAngle = 2.0 * Math.PI - 2.0 * Angle;
            }
            else // Горизонтальное отражение
            {
                newGabarit.l = Gabarit.r;
                newGabarit.r = Gabarit.l;
                newAngle = Math.PI - 2.0 * Angle;
            }

            Circle circle = new Circle(TypeName, newAngle, Center, Scale, newGabarit);
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
