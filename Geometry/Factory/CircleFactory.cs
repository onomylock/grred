using GrRed.Geometry.Domain;

namespace GrRed.Geometry.Factory
{
    public class CircleFactory : FigureFactory
    {
        public override IFigure GetFigure(double Angle, Vector Center, Vector Scale)
        {
            Circle circle = new(Angle, Center, Scale);
            return circle;
        }
    }
}
