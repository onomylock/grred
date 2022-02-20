namespace GrRed.Geometry.Graphic
{
    class CircleGraphic : IGraphic
    {
        public CircleGraphic(IFigure circle)
        {
            if (circle is null)
            {
                throw new ArgumentNullException(nameof(circle));
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

