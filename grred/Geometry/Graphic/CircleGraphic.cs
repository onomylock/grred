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

//все-таки достаточно 3 точки для эллипса
        void AddPolyArc(IEnumerable<Vector> lines) { 
            PathGeometry pathGeom = new PathGeometry();
            Path path = new Path();
            PathFigure pathFig = new PathFigure();
            ArcSegment arcSegment = new ArcSegment();
            pathFig.StartPoint = new Point(lines[0].X, lines[0].Y);
            arcSegment.Point = new Point(lines[2].X, lines[2].Y);
            Size size = new Size(Math.Abs(lines[0].X - lines[1].X), Math.Abs(lines[0].Y - lines[1].Y));
            arcSegment.Size = size;
            pathFig.Segments.Add(arcSegment);
            pathGeom.Figures.Add(pathFig);

            ArcSegment arcSegment1 = new ArcSegment();
            arcSegment1.Point = new Point(lines[0].X, lines[0].Y);
            arcSegment1.Size = size;
            pathFig.Segments.Add(arcSegment1);

            path.Data = pathGeom;
            path.Stroke = Brushes.Black;
        };
        void FillPolyArc(IEnumerable<Vector> lines) 
        {
            path.Fill = Brushes.YellowGreen;
        }

    }
}

