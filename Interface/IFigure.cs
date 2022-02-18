using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrRed
{
    public readonly struct Vector
    {
        readonly public double X;
        readonly public double Y;
    }
    public interface IFigure
    {
        string TypeName { get; }
        Vector Center { get; }
        double Angle { get; }
        Vector Scale { get; } //для шаблона
        (double l, double t, double r, double b) Gabarit { get; }
        IFigure Move(Vector delta);
        IFigure Reflection(Vector axe);
        IFigure Rotate(Vector delta);
        IFigure SetScale(double sx, double dy);
        bool IsIn(Vector p, double eps);
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


    public static class FigureFabric
    {
        public static IFigure Create(string name)
        {
            switch (name)
            {
                //                case "Circle": return new Circle();
            }

            return null;
        }
    }
}