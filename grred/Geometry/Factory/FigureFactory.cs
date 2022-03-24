using GrRed.Geometry.Domain;

using System.Collections.Generic;
namespace GrRed.Geometry.Factory
{
    public abstract class FigureFactory
    {
        public abstract int NumOfVertex { get; }
        public abstract IFigure GetFigure(double Angle, Vector Center, Vector Scale);
        public abstract IFigure GetFigure(IEnumerable<Vector> Points, double Angle);
    }
}
