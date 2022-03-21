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
        public readonly Path path;
        private readonly InkCanvas canvas;
        private Brush conturColor, fillColor;
        private double thickness;
        public EllipseGrafic(InkCanvas canvas, Path path)
        {
            this.canvas = canvas;
            this.path = path;
            //conturColor = curConturBrush;
            //fillColor = curFillBrush;
            //thickness = curThickness;
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
            //pathFig.StartPoint = new Point(50, 50);
            //arcSegment.Point = new Point(100, 50);
            //Size size = new Size(25, 10);
            Point center = new Point(Math.Abs((lines[0].X + lines[2].X) / 2), Math.Abs((lines[0].Y + lines[2].Y) / 2));
            Size size = new Size(Math.Abs(lines[0].X + lines[1].X - 2 * center.X), Math.Abs(lines[0].Y + lines[1].Y - 2 * center.Y));
            arcSegment.Size = size;
            pathFig.Segments.Add(arcSegment);
            pathGeom.Figures.Add(pathFig);

            ArcSegment arcSegment1 = new ArcSegment();
            arcSegment1.Point = new Point(lines[0].X, lines[0].Y);
            //arcSegment1.Point = new Point(50, 50);
            arcSegment1.Size = size;
            pathFig.Segments.Add(arcSegment1);

            path.Data = pathGeom;
            path.Stroke = Brushes.Black;
            //path.StrokeThickness = thickness;
            //path.Fill = fillColor;

            canvas.Children.Add(path);
        }
        public void FillPolygon(object color) => path.Fill = (Brush)color;
    }
    public class TriangleGrafic : IGraphic
    {
        private readonly InkCanvas canvas;
        public readonly Path path;
        public TriangleGrafic(InkCanvas canvas, Path path)
        {
            this.canvas = canvas;
            this.path = path;
            //conturColor = curConturBrush;
            //fillColor = curFillBrush;
            //thickness = curThickness;
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
            if (path.Stroke != Brushes.Black)
                path.Stroke = Brushes.Black;
            //path.StrokeThickness = thickness;
            //path.Fill = fillColor;

            canvas.Children.Add(path);
        }
        public void FillPolygon(object color) => path.Fill = (Brush)color;
    }

    public class LineGrafic : IGraphic
    {
        private readonly InkCanvas canvas;
        public readonly Path path;
        public LineGrafic(InkCanvas canvas, Path path)
        {
            this.canvas = canvas;
            this.path = path;
            //conturColor = curConturBrush;
            //thickness = curThickness;
        }
        public void AddPolyArc(IEnumerable<GrRed.Vector> Ilines) { }
        public void AddLines(IEnumerable<GrRed.Vector> Ilines)
        {
            PathGeometry pathGeom = new PathGeometry();
            PolyLineSegment polyLine = new PolyLineSegment(); //множество линий
            PathFigure pathFig = new PathFigure();

            List<GrRed.Vector> lines = Ilines.ToList();

            pathFig.StartPoint = new Point(lines[0].X, lines[0].Y); //начальная точка
            polyLine.Points.Add(new Point(lines[1].X, lines[1].Y));
            //pathFig.StartPoint = new Point(50, 50); //начальная точка
            //polyLine.Points.Add(new Point(200, 200));
            pathFig.Segments.Add(polyLine);

            pathGeom.Figures.Add(pathFig);

            //path.Data = pathGeom;
            path.Stroke = Brushes.Black;
            //path.StrokeThickness = thickness;

            canvas.Children.Add(path);
        }
        public void FillPolygon(object color) => path.Fill = (Brush)color;
    }

    public class RectangleGrafic : IGraphic
    {
        private readonly InkCanvas canvas;
        public readonly Path path;
        public RectangleGrafic(InkCanvas canvas, Path path)
        {
            this.canvas = canvas;
            this.path = path;
            //conturColor = curConturBrush;
            //fillColor = curFillBrush;
            //thickness = curThickness;
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
            path.Stroke = Brushes.Black;
            //path.StrokeThickness = thickness;
            //path.Fill = Brushes.Red;

            canvas.Children.Add(path);
        }
        public void FillPolygon(object color) => path.Fill = (Brush)color;
    }

    public static class GraphicFabric
    {
        public static IGraphic GetFactory(string name, InkCanvas canvas, Path path)
        {
            return name switch
            {
                "Ellipse" => new EllipseGrafic(canvas, path),
                "Triangle" => new TriangleGrafic(canvas, path),
                "Square" => new RectangleGrafic(canvas, path),
                _ => null,
            };
        }
    }
}
