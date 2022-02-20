using System;

namespace GrRed.Geometry.Graphic
{
    class SquareGraphic : IGraphic
    {
        public SquareGraphic(IFigure square)
        {
            if (square is null)
            {
                throw new ArgumentNullException(nameof(square));
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