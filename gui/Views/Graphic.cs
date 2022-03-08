using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using GrRed;

namespace gui
{
    public class EllipseGrafic : IGraphic
    {
        private readonly Canvas canvas;
        private readonly Path path;
        public EllipseGrafic(Canvas canvas, Path path)
        {
            this.canvas = canvas;
            this.path = path;
        }
        public void AddLines(IEnumerable<GrRed.Vector> Ilines) { }
        public void AddPolyArc(IEnumerable<GrRed.Vector> Ilines)
        {
            PathGeometry pathGeom = new PathGeometry();
            PathFigure pathFig = new PathFigure();
            ArcSegment arcSegment = new ArcSegment();

            List<GrRed.Vector> lines = Ilines.ToList();

            pathFig.StartPoint = new Point(lines[0].X, lines[0].Y);
            arcSegment.Point = new Point(lines[2].X, lines[2].Y);
            //Size size = new Size(25, 10);
            Point center = new Point(Math.Abs((lines[0].X + lines[2].X) / 2), Math.Abs((lines[0].Y + lines[2].Y) / 2));
            Size size = new Size(Math.Abs(lines[0].X + lines[1].X - 2 * center.X), Math.Abs(lines[0].Y + lines[1].Y - 2 * center.Y));
            arcSegment.Size = size;
            pathFig.Segments.Add(arcSegment);
            pathGeom.Figures.Add(pathFig);

            ArcSegment arcSegment1 = new ArcSegment();
            arcSegment1.Point = new Point(lines[0].X, lines[0].Y);
            arcSegment1.Size = size;
            pathFig.Segments.Add(arcSegment1);

            path.Data = pathGeom;
            path.Stroke = Brushes.Black;

            canvas.Children.Add(path);
        }
        public void FillPolygon(object color) => path.Fill = (Brush)color;
    }
    public class TriangleGrafic : IGraphic
    {
        private readonly Canvas canvas;
        private readonly Path path;
        public TriangleGrafic(Canvas canvas, Path path)
        {
            this.canvas = canvas;
            this.path = path;
        }
        public void AddPolyArc(IEnumerable<GrRed.Vector> Ilines) { }
        public void AddLines(IEnumerable<GrRed.Vector> Ilines)
        {
            //так как каждая фигура у нас придставима как набор линий,
            //то фигура будет выглядить следующим образом
            PathGeometry pathGeom = new PathGeometry();
            PolyLineSegment polyLine = new PolyLineSegment(); //множество линий
            PathFigure pathFig = new PathFigure();

            List<GrRed.Vector> lines = Ilines.ToList();

            pathFig.StartPoint = new Point(lines[0].X, lines[0].Y); //начальная точка
            for (int i = 1; i <= 2; i++)
                polyLine.Points.Add(new Point(lines[i].X, lines[i].Y));
            pathFig.Segments.Add(polyLine);
            pathFig.IsClosed = true;
            pathGeom.Figures.Add(pathFig);

            path.Data = pathGeom;
            path.Stroke = Brushes.Black; //контур

            canvas.Children.Add(path);
        }
        public void FillPolygon(object color) => path.Fill = (Brush)color;
    }
    public class RectangleGrafic : IGraphic
    {
        private readonly Canvas canvas;
        private readonly Path path;
        public RectangleGrafic(Canvas canvas, Path path)
        {
            this.canvas = canvas;
            this.path = path;
        }
        public void AddPolyArc(IEnumerable<GrRed.Vector> Ilines) { }
        public void AddLines(IEnumerable<GrRed.Vector> Ilines)
        {
            //так как каждая фигура у нас придставима как набор линий,
            //то фигура будет выглядить следующим образом
            PathGeometry pathGeom = new PathGeometry();
            PolyLineSegment polyLine = new PolyLineSegment(); //множество линий
            PathFigure pathFig = new PathFigure();

            List<GrRed.Vector> lines = Ilines.ToList();

            pathFig.StartPoint = new Point(lines[0].X, lines[0].Y); //начальная точка
            for (int i = 1; i <= 3; i++)
                polyLine.Points.Add(new Point(lines[i].X, lines[i].Y));
            pathFig.Segments.Add(polyLine);
            pathFig.IsClosed = true;
            pathGeom.Figures.Add(pathFig);

            path.Data = pathGeom;
            path.Stroke = Brushes.Black; //контур

            canvas.Children.Add(path);
        }
        public void FillPolygon(object color) => path.Fill = (Brush)color;
    }
}
