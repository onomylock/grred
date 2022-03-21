using GrRed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrRed.Geometry.Factory;
using System.Runtime.Serialization;

namespace GrRed
{
    [DataContract]
    public readonly struct Vector
    {
        [DataMember]
        readonly public double X;
        [DataMember]
        readonly public double Y;

        public Vector(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static Vector operator +(Vector a, Vector b)
            => new Vector(a.X + b.X, a.Y + b.Y);

        public static Vector operator -(Vector a, Vector b)
            => new Vector(a.X - b.X, a.Y - b.Y);

        public static Vector operator /(Vector a, double b)
            => new Vector(a.X / b, a.Y / b);

        public static bool operator ==(Vector a, Vector b)
            => a.X == b.X && a.Y == b.Y;

        public static bool operator !=(Vector a, Vector b)
            => a.X != b.X || a.Y != b.Y;
    }

    public interface IFigure
    {
        string TypeName { get; }
        Vector Center { get; } //Центр фигуры
        double Angle { get; }  //Угол поворота
        Vector Scale { get; } //для шаблона
        (double l, double t, double r, double b) Gabarit { get; } //Габариты фигуры
        IFigure Move(Vector delta); //Движение фигуры
        IFigure Reflection(bool axe); //Отображение фигуры
        IFigure Rotate(double delta);
        IFigure SetScale(double dx, double dy);
        bool IsIn(Vector p, double eps); //Точка внутри фигуры
        IFigure Intersection(IFigure fig2);
        IFigure Union(IFigure fig2);
        IFigure Subtruct(IFigure fig2);
        void Draw(IGraphic graphic);
    }

    public interface IGraphic
    {
        void AddLines(IEnumerable<Vector> lines);
        void FillPolygon(object color);
        void AddPolyArc(IEnumerable<Vector> lines);// каждые 3 точки -- дуга окружности
    }



    public static class FigureFabric
    {
        public static FigureFactory GetFactory(string name)
        {
            return name switch
            {
                "Ellipse" => new EllipseFactory(),
                "Triangle" => new TriangleFactory(),
                "Square" => new SquareFactory(),
                _ => null,
            };
        }
    }
}
