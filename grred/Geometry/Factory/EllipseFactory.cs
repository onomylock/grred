using GrRed.Geometry.Domain;
using System.Linq;
using System.Collections.Generic;

namespace GrRed.Geometry.Factory
{
    public class EllipseFactory : FigureFactory
    {
        public override int NumOfVertex => 4;

        public override IFigure GetFigure(IEnumerable<Vector> Points)
        {
            if (Points.Count() == NumOfVertex)
                return new Ellipse(Points);
            else return null;
        }

        public override IFigure GetFigure(double Angle, Vector Center, Vector Scale)
        {
            return new Ellipse(Angle, Center, Scale);
        }
    }
}
