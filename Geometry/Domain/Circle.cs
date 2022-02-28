using System;

namespace GrRed.Geometry.Domain
{
    class Circle : IFigure
    {
        private readonly Vector _Center = new(1.0, 1.0);
        private readonly double _Angle = 0.0;
        private readonly Vector _Scale = new(1.0, 1.0);

        public Circle() { }

        public Circle(double Angle, Vector Center, Vector Scale)
        {
            _Center = Center;
            _Angle = Angle;
            _Scale = Scale;
        }

        public string TypeName => "Circle";
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
            // Проверяем (x-x0)^2/a^2 + (y-y0)^2/b^2 +- eps <= 1, но для повёрнутого эллипса (немного другая формула для более общего случая)
            double AxisX = Scale.X / Math.Cos(Angle); // Полуоси 
            double AxisY = Scale.Y / Math.Cos(Angle); // повёрнутого эллипса
            double IsInCheck = Math.Pow((p.X - Center.X) * Math.Cos(Angle) + (p.Y - Center.Y) * Math.Sin(Angle), 2) / (AxisX * AxisX) + Math.Pow((-p.X + Center.X) * Math.Sin(Angle) + (p.Y - Center.Y) * Math.Cos(Angle), 2) / (AxisY * AxisY);

            if (IsInCheck + eps <= 1.0 || IsInCheck - eps <= 1.0)
                return true;
            else
                return false;
        }

        public IFigure Move(Vector delta)
        {
            Vector deltaCenter = Center + delta;
            Circle circle = new(Angle, deltaCenter, Scale);
            return circle;
        }

        public IFigure Reflection(bool axe)
        {
            double newAngle;

            if (axe) // Вертикальное отражение
            {
                Vector newScale = new(Scale.X, -Scale.Y);
                newAngle = Math.PI - 2.0 * Angle;
                Circle circle = new Circle(newAngle, Center, newScale);
                return circle;
            }
            else // Горизонтальное отражение
            {
                Vector newScale = new(-Scale.X, Scale.Y);
                newAngle = 2.0 * Math.PI - 2.0 * Angle;
                Circle circle = new Circle(newAngle, Center, newScale);
                return circle;
            }
        }

        public IFigure Rotate(double delta)
        {
            double newAngle = Angle + delta;

            Vector newScale = new(Scale.X * Math.Cos(newAngle), Scale.Y * Math.Cos(newAngle));

            Circle circle = new Circle(newAngle, Center, newScale);
            return circle;
        }

        public IFigure SetScale(double sx, double dy, bool a, bool b, bool scaleX)
        {
            if(a && b && scaleX) // Тянем за право-верх: координата x Scale и обе коры центра одновременно увеличиваются/уменьшаются
            {
                Vector newCenter = new(Center.X + 0.5 * sx, Center.Y + 0.5 * dy);
                Vector newScale = new(Scale.X + sx, Scale.Y);
                Circle circle = new Circle(Angle, newCenter, newScale);
                return circle;
            }
            else if(!a && b && scaleX) // Тянем за лево-верх: координата x Scale и y-координата центра одновременно увеличиваются, а x-координата центра уменьшается
            {
                Vector newCenter = new(Center.X - 0.5 * sx, Center.Y + 0.5 * dy);
                Vector newScale = new(Scale.X + sx, Scale.Y);
                Circle circle = new Circle(Angle, newCenter, newScale);
                return circle;
            }
            else if (!a && !b && scaleX) // Тянем за лево-низ: координата x Scale увеличивается, а обе координаты центра одновременно уменьшаются
            {
                Vector newCenter = new(Center.X - 0.5 * sx, Center.Y - 0.5 * dy);
                Vector newScale = new(Scale.X + sx, Scale.Y);
                Circle circle = new Circle(Angle, newCenter, newScale);
                return circle;
            }
            else if (a && !b && scaleX) // Тянем за лево-право: координата x Scale и x-координата центра одновременно увеличиваются, а y-координата центра уменьшается
            {
                Vector newCenter = new(Center.X + 0.5 * sx, Center.Y - 0.5 * dy);
                Vector newScale = new(Scale.X + sx, Scale.Y);
                Circle circle = new Circle(Angle, newCenter, newScale);
                return circle;
            }
            else if (a && b && !scaleX) // Тянем за право-верх: координата y Scale и обе коры центра одновременно увеличиваются/уменьшаются
            {
                Vector newCenter = new(Center.X + 0.5 * sx, Center.Y + 0.5 * dy);
                Vector newScale = new(Scale.X, Scale.Y + dy);
                Circle circle = new Circle(Angle, newCenter, newScale);
                return circle;
            }
            else if (!a && b && !scaleX) // Тянем за лево-верх: координата y Scale и y-координата центра одновременно увеличиваются, а x-координата центра уменьшается
            {
                Vector newCenter = new(Center.X - 0.5 * sx, Center.Y + 0.5 * dy);
                Vector newScale = new(Scale.X, Scale.Y + dy);
                Circle circle = new Circle(Angle, newCenter, newScale);
                return circle;
            }
            else if (!a && !b && !scaleX) // Тянем за лево-низ: координата y Scale увеличивается, а обе координаты центра одновременно уменьшаются
            {
                Vector newCenter = new(Center.X - 0.5 * sx, Center.Y - 0.5 * dy);
                Vector newScale = new(Scale.X, Scale.Y + dy);
                Circle circle = new Circle(Angle, newCenter, newScale);
                return circle;
            }
            else // Тянем за лево-право: координата y Scale и x-координата центра одновременно увеличиваются, а y-координата центра уменьшается
            {
                Vector newCenter = new(Center.X + 0.5 * sx, Center.Y - 0.5 * dy);
                Vector newScale = new(Scale.X, Scale.Y + dy);
                Circle circle = new Circle(Angle, newCenter, newScale);
                return circle;
            }
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
