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

        //основная проблема это обращение к примитивам  для заливки и прочего
        //взаймодействия с ними
        //поэтому пока такой вариант отрисовки

        void AddLines(IEnumerable<Vector> lines) 
        {
            //так как каждая фигура у нас придставима как набор линий,
            //то фигура будет выглядить следующим образом
            PathGeometry pathGeom = new PathGeometry();
            Path path = new Path();
            PolyLineSegment polyLine = new PolyLineSegment(); //множество линий
            PathFigure pathFig = new PathFigure();

            //
            pathFig.StartPoint = new Point(lines[0].X, lines[0].Y); //начальная точка
            Point[] polyLinePointArray =
            new Point[] { new Point(lines[1].X, lines[1].Y), new Point(lines[2].X, lines[2].Y) }; //две оcтальные
            polyLine.Points =
            new PointCollection(polyLinePointArray);
            pathFig.Segments.Add(polyLine);
            pathFig.IsClosed = true;
            pathGeom.Figures.Add(pathFig);

            path.Data = pathGeom;
            path.Stroke = Brushes.Black; //контур

            PaintingGrid.Children.Add(path); //добавляем на сетку

        };
        void FillPolygon(IEnumerable<Vector> lines)
        { 
            //как-то нужно получить ссылку на этот примитив для его заливки
            path.Fill = Brushes.Black; //заливаем фигуру
        };
    }
}
