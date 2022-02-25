using GrRed.Geometry.Domain;
using GrRed.Geometry.Graphic;

namespace GrRed.Geometry.Factory
{
    class TriangleFactory : FigureFactory
    {
        private Triangle triangle; 
        public override IFigure GetFigure(string TypeName, double Angle, Vector Center, Vector Scale, (double l, double t, double r, double b) Gabarit)
        {
            triangle = new(TypeName, Angle, Center, Scale, Gabarit){};
            return triangle;
        }

        public override IGraphic GetGraphic()
        {
            TriangleGraphic triangleGraphic = new(triangle){};
            return triangleGraphic;
        }
    }
}
