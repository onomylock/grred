using GrRed.Geometry.Domain;
using GrRed.Geometry.Graphic;

namespace GrRed.Geometry.Factory
{
    public class SquareFactory : FigureFactory
    {
        public override IFigure GetFigure(string TypeName, double Angle, Vector Center, Vector Scale, (double l, double t, double r, double b) Gabarit)
        {
            square = new(TypeName, Angle, Center, Scale, Gabarit);
            return square;
        }
    }
}
