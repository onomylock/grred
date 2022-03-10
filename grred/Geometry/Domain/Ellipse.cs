﻿using Newtonsoft.Json;

using System;
using System.Runtime.Serialization;

namespace GrRed.Geometry.Domain
{
    [DataContract]
    class Ellipse : IFigure
    {
        private readonly Vector _Center = new(1.0, 1.0);
        private readonly double _Angle = 0.0;
        private readonly Vector _Scale = new(1.0, 1.0);

        public Ellipse() { }

        [JsonConstructor]
        public Ellipse(double Angle, Vector Center, Vector Scale)
        {
            _Center = Center;
            _Angle = Angle;
            _Scale = Scale;
        }

        public string TypeName => "Ellipse";
        [DataMember]
        public double Angle => _Angle;
        [DataMember]
        public Vector Center => _Center;
        [DataMember]
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
            double AxisX;
            double AxisY;

            // Вычисляем полуоси повёрнутого эллипса
            if (Math.Abs(Angle) % Math.PI/2.0 <= eps) // Случай, когда угол кратен пи/2 
            {
                AxisY = Scale.X;
                AxisX = Scale.Y;
            }
            else                                      // Любой другой случай
            {
                AxisX = Scale.X / Math.Cos(Angle);
                AxisY = Scale.Y / Math.Cos(Angle); 
            }

            // Проверяем (x-x0)^2/a^2 + (y-y0)^2/b^2 +- eps <= 1, но для повёрнутого эллипса (немного другая формула для более общего случая)
            
            double IsInCheck = Math.Pow((p.X - Center.X) * Math.Cos(Angle) + (p.Y - Center.Y) * Math.Sin(Angle), 2) / (AxisX * AxisX) + Math.Pow((-p.X + Center.X) * Math.Sin(Angle) + (p.Y - Center.Y) * Math.Cos(Angle), 2) / (AxisY * AxisY);

            if (IsInCheck + eps <= 1.0 || IsInCheck - eps <= 1.0)
                return true;
            else
                return false;
        }

        public IFigure Move(Vector delta)
        {
            Vector deltaCenter = Center + delta;
            return new Ellipse(Angle, deltaCenter, Scale);
        }

        public IFigure Reflection(bool axe)
        {
            double newAngle;

            if (axe) // Вертикальное отражение
            {
                newAngle = Math.PI - 2.0 * Angle;
                Vector newScale = new(Scale.X, -Scale.Y);
                return new Ellipse(newAngle, Center, newScale);
            }
            else // Горизонтальное отражение
            {
                newAngle = 2.0 * Math.PI - 2.0 * Angle;
                Vector newScale = new(-Scale.X, Scale.Y);
                return new Ellipse(newAngle, Center, newScale);
            }
        }

        public IFigure Rotate(double delta)
        {
            double newAngle = Angle + delta;
            Vector newScale = new(Scale.X * Math.Cos(newAngle), Scale.Y * Math.Cos(newAngle));
            return new Ellipse(newAngle, Center, newScale);
        }

        public IFigure SetScale(double dx, double dy)
        {
            Vector newScale = new(Scale.X + dx / 2, Scale.Y + dy / 2);
            return new Ellipse(Angle, Center, newScale);
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