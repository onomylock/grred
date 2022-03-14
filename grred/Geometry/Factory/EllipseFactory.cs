using GrRed.Geometry.Domain;

using System.Collections.Generic;
namespace GrRed.Geometry.Factory
{
    public class EllipseFactory : FigureFactory
    {
        public override IFigure GetFigure(IEnumerable<Vector> Points)
        {
            return new Ellipse(Points);
        }
        public override IFigure GetFigure(double Angle, Vector Center, Vector Scale)
        {
            return new Ellipse(Angle, Center, Scale);
        }
    }
}
