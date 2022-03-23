using System;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace GrRed.Geometry.Domain
{
    [DataContract]
    public class Triangle : IFigure
    {
        public Triangle() { }

        [JsonConstructor]
        public Triangle(double angle, Vector center, Vector scale)
        {
            Center = center;
            Angle = angle;
            Scale = scale;
            Points = SetInputPoints();
        }

        public Triangle(IEnumerable<Vector> Points)
        {
            this.Points = Points.ToArray();
            Scale = new Vector(this.Points[2].X - this.Points[0].X, this.Points[1].Y - this.Points[2].Y);
            Center = SetInputCenter(this.Points);
            Angle = SetInputAngle(this.Points);
        }

        public string TypeName => "Triangle";
        [DataMember]
        public Vector[] Points { get; }
        public double Angle { get; }
        [DataMember]
        public Vector Center { get; }
        [DataMember]
        public Vector Scale { get; }

        public (double l, double t, double r, double b) Gabarit =>
            (Center.X - Scale.X, Center.Y + Scale.Y * 2.0 / 3.0, Center.X, Center.Y - Scale.Y * 2.0 / 3.0);

        private double ScalarMult(Vector a, Vector b) => a.X * b.X + a.Y * b.Y;

        private double VectorModul(Vector a) => Math.Sqrt(Math.Pow(a.X, 2) + Math.Pow(a.Y, 2));

        public void Draw(IGraphic graphic)
        {
            //Path path = new Path();
            //LineGrafic lineGrafic = new LineGrafic(paintingCanvas, path);
            graphic.AddLines(Points);
            //object brush2 = "#ffc0cb";

            //Brush brush2 = Brushes.Firebrick;
            //graphic.FillPolygon(brush2);
        }

        //public void Draw(IGraphic graphic, object o, object oo)
        //{
        //    graphic.AddLines(Points);
        //    graphic.conturColor = o;
        //    graphic.fillColor = oo;
        //}

        private Vector[] SetInputPoints()
        {
            Vector[] newPoints = new Vector[3];
            // newPoints[0] = Scale;
            // newPoints[1] = Center;
            // newPoints[2] = new Vector(Scale.X, Center.Y);
            newPoints[0] = new Vector(Scale.X * Math.Cos(Angle) - Scale.Y * Math.Sin(Angle),
            Scale.X * Math.Sin(Angle) + Scale.Y * Math.Cos(Angle));
            newPoints[1] = new Vector(Center.X * Math.Cos(Angle) - Center.Y * Math.Sin(Angle),
            Center.X * Math.Sin(Angle) + Center.Y * Math.Cos(Angle));
            newPoints[2] = new Vector(Scale.X * Math.Cos(Angle) - Center.Y * Math.Sin(Angle),
            Scale.X * Math.Sin(Angle) + Center.Y * Math.Cos(Angle));
            return newPoints;
        }
        private double SetInputAngle(Vector[] Points) => Math.Asin((Points[2].Y - Points[0].Y) / VectorModul(Points[2] - Points[0]));

        private Vector SetInputCenter(Vector[] Points) => (Points[0] + Points[1] + Points[2]) / 3.0;

        public IFigure Intersection(IFigure fig2)
        {
            throw new NotImplementedException();
        }

        public bool IsIn(Vector p, double eps)
        {
            Vector p1 = Points[0];
            Vector p2 = Points[1];
            Vector p3 = Points[2];

            double Sabc = Math.Abs((p2.X - p1.X) * (p3.Y - p1.Y) - (p3.X - p1.X) * (p2.Y - p1.Y));
            double Sabp = Math.Abs((p2.X - p1.X) * (p.Y - p1.Y) - (p.X - p1.X) * (p2.Y - p1.Y));
            double Sbcp = Math.Abs((p2.X - p.X) * (p3.Y - p.Y) - (p3.X - p.X) * (p2.Y - p.Y));
            double Sacp = Math.Abs((p.X - p1.X) * (p3.Y - p1.Y) - (p3.X - p1.X) * (p.Y - p1.Y));
            double sum = Sabp + Sbcp + Sacp;

            if (Math.Abs(Sabc - sum) < eps) return true;
            else return false;
        }

        public IFigure Move(Vector delta)
        {
            Vector[] newPoints = new Vector[3];
            for (int i = 0; i < newPoints.Count(); i++)
            {
                newPoints[i] = Points[i] + delta;
            }
            return new Triangle(newPoints);
        }

        public IFigure Reflection(bool axe)
        {
            Vector[] newPoints = new Vector[3];
            // newPoints.Add(_Points.ElementAt(0));
            // newPoints.Add(_Points.ElementAt(1));
            // newPoints.Add(_Points.ElementAt(2));

            if (axe) //Вертикальное
            {
                for (int i = 0; i < newPoints.Count(); i++)
                {
                    if (Points[i].Y > Center.Y) newPoints[i] = new Vector(Points[i].Y, Points[i].Y - 2 * Center.Y);
                    else if (Points[i].Y < Center.Y) newPoints[i] = new Vector(newPoints[i].Y, newPoints[i].Y + 2 * Center.Y);
                }
                return new Triangle(newPoints);
            }
            else //Горизонтальное
            {
                for (int i = 0; i < newPoints.Count(); i++)
                {
                    if (Points[i].X > Center.X) newPoints[i] = new Vector(Points[i].X - 2 * Center.X, Points[i].Y);
                    else if (Points[i].X < Center.X) newPoints[i] = new Vector(Points[i].X + 2 * Center.X, Points[i].Y);
                }
                return new Triangle(newPoints);
            }
        }

        public IFigure Rotate(double delta)
        {
            double newAngle = (Angle + delta) % Math.PI;
            Vector[] newPoints = new Vector[3];

            for (int i = 0; i < newPoints.Count(); i++)
            {
                newPoints[i] = new Vector(Points[i].X * Math.Cos(newAngle) - Points[i].Y * Math.Sin(newAngle),
                Points[i].X * Math.Sin(newAngle) + Points[i].Y * Math.Cos(newAngle));
            }
            return new Triangle(newPoints);
        }

        public IFigure SetScale(double dx, double dy)
        {
            Vector newScale = new(Scale.X + dx / 2, Scale.Y + dy / 2);
            return new Triangle(Angle, Center, newScale);
            // Vector[] newPoints = new Vector[3];
            // for (int i = 0; i < Points.Count(); i++)
            // {
            //     newPoints[i].X = Points[i].X 
            // }
        }

        public IFigure Subtruct(IFigure fig2)
        {
            throw new NotImplementedException();
        }

        public IFigure Union(IFigure fig2)
        {
            throw new NotImplementedException();
        }
    }
}
