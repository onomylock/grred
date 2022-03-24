using GrRed.Geometry.Domain;
using System.Linq;
using System.Collections.Generic;
namespace GrRed.Geometry.Factory
{
    public class TriangleFactory : FigureFactory
    {
        public override int NumOfVertex => 3;

        public override IFigure GetFigure(IEnumerable<Vector> Points, double Angle)
        {
            if (Points.Count() == NumOfVertex)
                return new Triangle(Points, Angle);
            else return null;
        }

        public override IFigure GetFigure(double Angle, Vector Center, Vector Scale)
        {
            return new Triangle(Angle, Center, Scale);
        }
    }
}
