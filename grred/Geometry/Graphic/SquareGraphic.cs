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

        void AddLines(IEnumerable<Vector> lines) 
        { 
            PathGeometry pathGeom = new PathGeometry();
            Path path = new Path();
            PolyLineSegment polyLine = new PolyLineSegment();
            PathFigure pathFig = new PathFigure();

            pathFig.StartPoint = new Point(lines[0].X, lines[0].Y);
            Point[] polyLinePointArray =
                new Point[] { new Point(lines[1].X, lines[1].Y), new Point(lines[2].X, lines[2].Y), new Point(lines[3].X, lines[3].Y) };
            polyLine.Points =
                 new PointCollection(polyLinePointArray);
            pathFig.Segments.Add(polyLine);
            pathFig.IsClosed = true;
            pathGeom.Figures.Add(pathFig);
            path.Data = pathGeom;
            path.Stroke = Brushes.Black;
            PaintingGrid.Children.Add(path);

        };
        
        void FillPolygon(IEnumerable<Vector> lines) 
        {
            path.Fill = Brushes.Red;
        };

    }
}