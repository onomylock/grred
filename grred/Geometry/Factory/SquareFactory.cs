using GrRed.Geometry.Domain;

using System.Collections.Generic;
namespace GrRed.Geometry.Factory
{
    public class SquareFactory : FigureFactory
    {
        public override IFigure GetFigure(IEnumerable<Vector> Points)
        {
            return new Square(Points);
        }
        public override IFigure GetFigure(double Angle, Vector Center, Vector Scale)
        {
            return new Square(Angle, Center, Scale);
        }
    }
}
