using GrRed.Geometry.Domain;

namespace GrRed.Geometry.Factory
{
    class TriangleFactory : FigureFactory
    {
        public override IFigure GetFigure(double Angle, Vector Center, Vector Scale)
        {
            return new Triangle(Angle, Center, Scale);
        }
    }
}
