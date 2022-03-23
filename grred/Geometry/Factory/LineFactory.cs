using GrRed.Geometry.Domain;
using System.Linq;
using System.Collections.Generic;

namespace GrRed.Geometry.Factory
{
    public class LineFactory : FigureFactory
    {
        public override int NumOfVertex => 2;

        public override IFigure GetFigure(IEnumerable<Vector> Points)
        {
            if (Points.Count() == NumOfVertex)
                return new Line(Points);
            else return null;
        }

        public override IFigure GetFigure(double Angle, Vector Center, Vector Scale)
        {
            Vector[] points = new Vector[2];
            points[1] = Scale; points[0] = Center;
            return new Line(points);
        }
    }
}
