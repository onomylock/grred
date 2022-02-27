using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace gui
{
    public struct Vector
    {
        public double X;
        public double Y;
        public Vector(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }
    }
    public interface IGraphic
    {
        void AddLines(List<Vector> lines) 
        {
            //так как каждая фигура у нас придставима как набор линий,
            //то фигура будет выглядить следующим образом
            PathGeometry pathGeom = new PathGeometry();
            Path path = new Path();
            PolyLineSegment polyLine = new PolyLineSegment(); //множество линий
            PathFigure pathFig = new PathFigure();

            pathFig.StartPoint = new Point(lines[0].X, lines[0].Y); //начальная точка
            for (int i = 1; i < lines.Count(); i++)
                polyLine.Points.Add(new Point(lines[i].X, lines[i].Y));
            pathFig.Segments.Add(polyLine);
            pathFig.IsClosed = true;
            pathGeom.Figures.Add(pathFig);

            path.Data = pathGeom;
            path.Stroke = Brushes.Black; //контур

            MainWindow.PaintingGrid.Children.Add(path); //добавляем на сетку
        }
        void AddPolyArc(List<Vector> lines)
        {
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

            PaintingGrid.Children.Add(path);
        }
        void FillPolygon(Path path, Brush brush)
        {
            path.Fill = brush;
        }
    }
}
