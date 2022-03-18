﻿using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;

namespace GrRed.Geometry.Domain
{
    [DataContract]
    class Ellipse : IFigure
    {
        private readonly Vector _Center = new(1.0, 1.0);
        private readonly double _Angle = 0.0;
        private readonly Vector _Scale = new(1.0, 1.0);
        private readonly bool _Flag90 = false;

        public Ellipse() { }

        [JsonConstructor]
        public Ellipse(double Angle, Vector Center, Vector Scale, bool Flag90)
        {
            _Center = Center;
            _Angle = Angle;
            _Scale = Scale;
            _Flag90 = Flag90;
        }

        public Ellipse(IEnumerable<Vector> Points)
        {
            _Scale = new Vector(Math.Abs(Points.ElementAt(0).X), Math.Abs(Points.ElementAt(1).Y));
            _Center = SetInputCenter(Points);
            Vector point = Points.ElementAt(1);
            _Angle = Math.Asin((point.Y - _Center.Y) / VectorModul(point - _Center));
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

        private double VectorModul(Vector a) => Math.Sqrt(Math.Pow(a.X, 2) + Math.Pow(a.Y, 2));

        public void Draw(IGraphic graphic)
        {
            //Path path = new Path();
            //EllipseGrafic ellipseGrafic = new EllipseGrafic(paintingCanvas, path);
            List<Vector> vector1 = new List<GrRed.Vector>();
            vector1.Add(new GrRed.Vector(500, 500));
            vector1.Add(new GrRed.Vector(450, 450));
            vector1.Add(new GrRed.Vector(400, 400));
            graphic.AddPolyArc(vector1);
            //Brush brush1 = Brushes.BlueViolet;
            //ellipseGrafic.FillPolygon(brush1);

        }

        private Vector SetInputCenter(IEnumerable<Vector> Points)
        {
            Vector p1 = Points.ElementAt(0);
            Vector p2 = Points.ElementAt(1);
            Vector p3 = Points.ElementAt(2);
            Vector p4 = Points.ElementAt(3);

            return new Vector(p3.X - p1.X, p2.Y - p4.Y);
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
            if(Math.Abs(Math.PI / 2.0 + Angle) % Math.PI <= eps) // Случай, когда угол кратен пи/2
            {
                AxisY = Scale.X;
                AxisX = Scale.Y;
            }
            else                                      // Любой другой случай
            {
                if (_Flag90)
                {
                    AxisY = Scale.X / Math.Cos(Angle);
                    AxisX = Scale.Y / Math.Cos(Angle);
                }
                else
                {
                    AxisX = Scale.X / Math.Cos(Angle);
                    AxisY = Scale.Y / Math.Cos(Angle);
                }
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
            return new Ellipse(Angle, deltaCenter, Scale, _Flag90);
        }

        public IFigure Reflection(bool axe)
        {
            return this.Rotate(-2.0 * Angle);
        }

        public IFigure Rotate(double delta)
        {
            double newAngle = Angle + delta;
            double eps = 0.1;
            Vector newScale;
            double AxisX;
            double AxisY;

            if (Math.Abs(Math.PI / 2.0 + Angle) % Math.PI <= eps/1000) // Случай, когда угол кратен пи/2
            {
                AxisX = Scale.X;
                AxisY = Scale.Y;
            }
            else                                      // Любой другой случай
            {
                AxisX = Scale.X / Math.Cos(Angle);
                AxisY = Scale.Y / Math.Cos(Angle);
            }

            if (Math.Abs(Math.PI / 2.0 + newAngle) % Math.PI <= eps/1000) // Случай, когда новый угол кратен пи/2 
            {
                if (_Flag90)
                    newScale = new(AxisX, AxisY);
                else
                    newScale = new(AxisY, AxisX);

                return new Ellipse(newAngle, Center, newScale, true);
            }
            else if (Math.Abs(newAngle) % Math.PI <= eps/1000) // Случай, когда новый угол кратен пи
            {
                if (!_Flag90)
                    newScale = new(AxisX, AxisY);
                else
                    newScale = new(AxisY, AxisX);

                return new Ellipse(newAngle, Center, newScale, false);
            }
            else
            {
                newScale = new(AxisX * Math.Cos(newAngle), AxisY * Math.Cos(newAngle));
                return new Ellipse(newAngle, Center, newScale, _Flag90);
            }
        }

        public IFigure SetScale(double dx, double dy)
        {
            Vector newScale = new(Scale.X + dx / 2.0, Scale.Y + dy / 2.0);
            return new Ellipse(Angle, Center, newScale, _Flag90);
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
