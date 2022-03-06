using GrRed.Geometry.Domain;

namespace GrRed.Geometry.Factory
{
    public class SquareFactory : FigureFactory
    {
        public override IFigure GetFigure(double Angle, Vector Center, Vector Scale)
        {
            return new Square(Angle, Center, Scale);
        }
    }
}
