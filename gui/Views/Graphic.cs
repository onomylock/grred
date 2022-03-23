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
        private readonly Path path;
        private Brush conturColor, fillColor;
        private double thickness;
        private readonly InkCanvas canvas;

        object IGraphic.path => (Path)this.path;

        object IGraphic.conturColor { get => (Brush)this.conturColor; set => this.conturColor = (Brush)value; }
        object IGraphic.fillColor { get => (Brush)this.fillColor; set => this.fillColor = (Brush)value; }
        double IGraphic.thickness { get => this.thickness; set => this.thickness = value; }

        public EllipseGrafic(InkCanvas canvas)
        {
            this.canvas = canvas;
            this.path = new Path();
            conturColor = Brushes.Black;
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
            path.Stroke = conturColor;
            path.StrokeThickness = thickness;
            path.Fill = fillColor;

            canvas.Children.Add(path);
        }
        public void FillPolygon(object color) => path.Fill = (Brush)color;

    }
    public class TriangleGrafic : IGraphic
    {
        private readonly Path path;
        private Brush conturColor, fillColor;
        private double thickness;
        private readonly InkCanvas canvas;

        object IGraphic.path => (Path)this.path;

        object IGraphic.conturColor { get => (Brush)this.conturColor; set => this.conturColor = (Brush)value; }
        object IGraphic.fillColor { get => (Brush)this.fillColor; set => this.fillColor = (Brush)value; }
        double IGraphic.thickness { get => this.thickness; set => this.thickness = value; }
        public TriangleGrafic(InkCanvas canvas)
        {
            this.canvas = canvas;
            this.path = new();
            this.conturColor = Brushes.Black;
            this.thickness = 1;
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
            path.Stroke = conturColor;
            path.StrokeThickness = thickness;
            path.Fill = fillColor;

            canvas.Children.Add(path);
        }
        public void FillPolygon(object color) => path.Fill = (Brush)color;

    }

    public class LineGrafic : IGraphic
    {
        private readonly Path path;
        private Brush conturColor, fillColor;
        private double thickness;

        private readonly InkCanvas canvas;
        object IGraphic.path => (Path)this.path;

        object IGraphic.conturColor { get => (Brush)this.conturColor; set => this.conturColor = (Brush)value; }
        object IGraphic.fillColor { get => (Brush)this.fillColor; set => this.fillColor = (Brush)value; }
        double IGraphic.thickness { get => this.thickness; set => this.thickness = value; }
        public LineGrafic(InkCanvas canvas)
        {
            this.canvas = canvas;
            this.path = new();
            this.conturColor = Brushes.Black;
            this.thickness = 1;
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
            pathFig.Segments.Add(polyLine);
            pathFig.IsClosed = true;

            pathGeom.Figures.Add(pathFig);

            path.Data = pathGeom;
            path.Stroke = conturColor;
            path.StrokeThickness = thickness;
            path.Fill = fillColor;

            canvas.Children.Add(path);
        }
        public void FillPolygon(object color) => path.Fill = (Brush)color;

    }

    public class RectangleGrafic : IGraphic
    {
        private readonly Path path;
        private Brush conturColor, fillColor;
        private double thickness;
        private readonly InkCanvas canvas;
        object IGraphic.path => (Path)this.path;

        object IGraphic.conturColor { get => (Brush)this.conturColor; set => this.conturColor = (Brush)value; }
        object IGraphic.fillColor { get => (Brush)this.fillColor; set => this.fillColor = (Brush)value; }
        double IGraphic.thickness { get => this.thickness; set => this.thickness = value; }

        public RectangleGrafic(InkCanvas canvas)
        {
            this.canvas = canvas;
            this.path = new();
            conturColor = Brushes.Black;
            this.thickness = 1;
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
            path.Stroke = conturColor;
            path.StrokeThickness = thickness;
            path.Fill = fillColor;

            canvas.Children.Add(path);
        }
        public void FillPolygon(object color) => path.Fill = (Brush)color;

    }

    public static class GraphicFabric
    {
        public static IGraphic GetFactory(string name, InkCanvas canvas)
        {
            return name switch
            {
                "Ellipse" => new EllipseGrafic(canvas),
                "Triangle" => new TriangleGrafic(canvas),
                "Square" => new RectangleGrafic(canvas),
                "Line" => new LineGrafic(canvas),
                _ => null,
            };
        }
    }
}
