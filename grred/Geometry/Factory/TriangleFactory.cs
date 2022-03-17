using GrRed.Geometry.Domain;

using System.Collections.Generic;
namespace GrRed.Geometry.Factory
{
    public class TriangleFactory : FigureFactory
    {
        public override IFigure GetFigure(IEnumerable<Vector> Points)
        {
            return new Triangle(Points);
        }
        public override IFigure GetFigure(double Angle, Vector Center, Vector Scale)
        {
            return new Triangle(Angle, Center, Scale);
        }
    }
}
