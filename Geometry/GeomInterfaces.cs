using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrRed.Geometry.Factory;
using GrRed.Geometry.Domain;
using GrRed.Geometry.Graphic;

namespace GrRed.Geometry
{
    public readonly struct Vector
    {
        readonly public double X;
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
        //Добавить перегрузки если нужно
    }
    public interface IFigure
    {
        string TypeName { get; }
        Vector Center { get; } //Центр фигуры
        double Angle { get; }  //Угол поворота
        Vector Scale { get; } //для шаблона
        (double l, double t, double r, double b) Gabarit { get; } //Габариты фигуры
        IFigure Move(Vector delta); //Движение фигуры
        IFigure Reflection(Vector axe); //Отображение фигуры
        IFigure Rotate(double delta, bool clockwise);
        IFigure SetScale(double sx, double dy);
        bool IsIn(Vector p, double eps); //Точка внутри фигуры
        IFigure Intersection(IFigure fig2);
        IFigure Union(IFigure fig2);
        IFigure Subtruct(IFigure fig2);
        void Draw(IGraphic graphic);
    }

    public interface IGraphic
    {
        void AddLines(IEnumerable<Vector> lines);
        void FillPolygon(IEnumerable<Vector> lines);
        void AddPolyArc(IEnumerable<Vector> lines);// каждые 3 точки -- дуга окружности
        void FillPolyArc(IEnumerable<Vector> lines);// каждые 3 точки -- дуга окружности
    }

    public static class Main
    {   
        public static FigureFactory GetFactory(string name)
        {
            return name switch
            {
                "Circle" => new CircleFactory(),
                "Triangle" => new TriangleFactory(),
                "Square" => new SquareFactory(),
                _ => null,
            };
        }
    }
}