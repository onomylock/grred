using System.Collections.Generic;

namespace GrRed.Geometry.Graphic
{
    class TriangleGraphic : IGraphic
    {
        public TriangleGraphic(IFigure triangle)
        {
            if (triangle is null)
            {
                throw new ArgumentNullException(nameof(triangle));
            }
            //
            // TODO: Add constructor logic here
            //
        }

        void AddLines(IEnumerable<Vector> lines) { };
        void FillPolygon(IEnumerable<Vector> lines) { };
        void AddPolyArc(IEnumerable<Vector> lines) { };// каждые 3 точки -- дуга окружности
        void FillPolyArc(IEnumerable<Vector> lines) { };// каждые 3 точки -- дуга окружности
    }
}
