﻿using System;

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
        public Vector Center => Center;
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
                newAngle = Math.PI - 2.0 * Angle;
                Circle circle = new Circle(newAngle, Center, Scale);
                return circle;
            }
            else // Горизонтальное отражение
            {
                newAngle = 2.0 * Math.PI - 2.0 * Angle;
                Circle circle = new Circle(newAngle, Center, Scale);
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
