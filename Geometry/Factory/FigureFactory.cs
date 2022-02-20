using GrRed.Geometry.Domain;
using GrRed.Geometry.Graphic;

namespace GrRed.Geometry.Factory
{
    public abstract class FigureFactory
    {
        public abstract IFigure GetFigure(string TypeName, double Angle, Vector Center, Vector Scale, (double l, double t, double r, double b) Gabarit);
        public abstract IGraphic GetGraphic(IFigure figure);
    }
}
