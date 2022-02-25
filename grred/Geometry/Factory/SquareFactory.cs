using GrRed.Geometry.Domain;
using GrRed.Geometry.Graphic;

namespace GrRed.Geometry.Factory
{
    public class SquareFactory : FigureFactory
    {
        private Square square;
        public override IFigure GetFigure(string TypeName, double Angle, Vector Center, Vector Scale, (double l, double t, double r, double b) Gabarit)
        {
            square = new(TypeName, Angle, Center, Scale, Gabarit){};
            return square;
        }

        public override IGraphic GetGraphic()
        {
            SquareGraphic squareGraphic = new(square){};
            return squareGraphic;
        }
    }
}
