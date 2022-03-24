using GrRed.Geometry.Domain;
using System.Linq;
using System.Collections.Generic;

namespace GrRed.Geometry.Factory
{
    public class LineFactory : FigureFactory
    {
        public override int NumOfVertex => 2;

        public override IFigure GetFigure(IEnumerable<Vector> Points, double Angle)
        {
            if (Points.Count() == NumOfVertex)
                return new Line(Points, Angle);
            else return null;
        }

        public override IFigure GetFigure(double Angle, Vector Center, Vector Scale)
        {
            return new Line(Angle, Center, Scale);
        }
    }
}
