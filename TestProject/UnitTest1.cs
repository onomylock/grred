using GrRed;
using GrRed.Geometry.Factory;
using System;
using Newtonsoft.Json;

using NUnit.Framework;

namespace TestProject
{
    public class Json_Test
    {
        [SetUp]
        public void Setup()
        {
        }

        //[Test]
        //public void Test1()
        //{
        //    var ellipse = new EllipseFactory().GetFigure(3, new Vector(1, 0), new Vector(2, 2));
        //    var str=JsonConvert.SerializeObject(ellipse);
        //    var ellipse2 = JsonConvert.DeserializeObject<GrRed.Geometry.Domain.Ellipse>(str);
        //    Assert.AreEqual("GrRed.Geometry.Domain.Ellipse", ellipse2.GetType().FullName);
        //    Assert.AreEqual(1.0, ellipse2.Center.X);
        //    Assert.AreEqual(0.0, ellipse2.Center.Y);
        //}
        //[Test]
        //public void Test2()
        //{
        //    var ellipse = new EllipseFactory().GetFigure(3, new Vector(1, 0), new Vector(2, 2));
        //    var settings = new JsonSerializerSettings()
        //    {
        //        Formatting = Formatting.Indented, 
        //        TypeNameHandling = TypeNameHandling.All
        //    };

        //    var str=JsonConvert.SerializeObject(ellipse,settings);
        //    var ellipse2 = JsonConvert.DeserializeObject(str,settings);
        //    Assert.AreEqual("GrRed.Geometry.Domain.Ellipse", ellipse2.GetType().FullName);
        //    var ellipse3= ellipse2 as IFigure;
        //    Assert.AreEqual(1.0, ellipse3.Center.X);
        //    Assert.AreEqual(0.0, ellipse3.Center.Y);
        //}
    }

    public class Triangle_test
    {
        [Test]
        public void IsIn_triangle_in()
        {
            Vector[] points = new Vector[3];
            points[0] = new Vector(0, 0);
            points[1] = new Vector(0, 8);
            points[2] = new Vector(10, 0);
            var triangle = new TriangleFactory().GetFigure(points);

            var point1 = new GrRed.Vector(1, 0);
            var point2 = new GrRed.Vector(0, 1);
            double eps = 0.1;

            Assert.AreEqual(true, triangle.IsIn(point1, eps));
            Assert.AreEqual(true, triangle.IsIn(point2, eps));

        }
        [Test]
        public void IsIn_triangle_out()
        {
            Vector[] points = new Vector[3];
            points[0] = new Vector(0, 0);
            points[1] = new Vector(0, 8);
            points[2] = new Vector(10, 0);
            var triangle = new TriangleFactory().GetFigure(points);

            var point1 = new GrRed.Vector(15, 0);
            var point2 = new GrRed.Vector(0, 10);
            double eps = 0.1;

            Assert.AreEqual(true, triangle.IsIn(point1, eps));
            Assert.AreEqual(true, triangle.IsIn(point2, eps));
        }

        [Test]
        public void Rotate_triangle_360_points()
        {
            Vector[] points = new Vector[3];
            points[0] = new Vector(0, 0);
            points[1] = new Vector(6, 8);
            points[2] = new Vector(10, 0);
            var triangle = new TriangleFactory().GetFigure(points);
            var triangle2 = triangle.Rotate(2 * Math.PI);

            for (int i = 0; i < 3; i++)
            {
                Assert.AreEqual(triangle.Points[i].X, triangle2.Points[i].X);
                Assert.AreEqual(triangle.Points[i].Y, triangle2.Points[i].Y);
            }
        }

        [Test]
        public void Rotate_triangle_360_angle()
        {
            Vector[] points = new Vector[3];
            points[0] = new Vector(0, 0);
            points[1] = new Vector(6, 8);
            points[2] = new Vector(10, 0);
            var triangle = new TriangleFactory().GetFigure(points);
            var triangle2 = triangle.Rotate(2 * Math.PI);

            Assert.AreEqual(triangle.Angle, triangle2.Angle);
        }

        [Test]
        public void Rotate_triangle_360_center()
        {
            Vector[] points = new Vector[3];
            points[0] = new Vector(0, 0);
            points[1] = new Vector(6, 8);
            points[2] = new Vector(10, 0);
            var triangle = new TriangleFactory().GetFigure(points);
            var triangle2 = triangle.Rotate(2 * Math.PI);

            Assert.AreEqual(triangle.Center, triangle2.Center);
        }

        [Test]
        public void Rotate_triangle_pi_na_3()
        {
            Vector[] points = new Vector[3];
            points[0] = new Vector(0, 0);
            points[1] = new Vector(6, 8);
            points[2] = new Vector(10, 0);
            var triangle = new TriangleFactory().GetFigure(points);
            var triangle2 = triangle.Rotate(Math.PI / 3);

            Assert.AreEqual(Math.PI / 3, triangle2.Angle);
        }

        [Test]
        public void Reflection_triangle_goriz_2x()
        {
            Vector[] points = new Vector[3];
            points[0] = new Vector(0, 0);
            points[1] = new Vector(0, 8);
            points[2] = new Vector(10, 0);

            var triangle = new TriangleFactory().GetFigure(points);

            var triangle2 = triangle.Reflection(false);
            triangle2 = triangle2.Reflection(false);

            for (int i = 0; i < 3; i++)
            {
                Assert.AreEqual(triangle.Points[i].X, triangle2.Points[i].X);
                Assert.AreEqual(triangle.Points[i].Y, triangle2.Points[i].Y);
            }
            Assert.AreEqual(triangle.Angle, triangle2.Angle);
        }

        [Test]
        public void Reflection_triangle_vert_2x()
        {
            Vector[] points = new Vector[3];
            points[0] = new Vector(0, 0);
            points[1] = new Vector(0, 8);
            points[2] = new Vector(10, 0);

            var triangle = new TriangleFactory().GetFigure(points);

            var triangle2 = triangle.Reflection(true);
            triangle2 = triangle2.Reflection(true);

            for (int i = 0; i < 3; i++)
            {
                Assert.AreEqual(triangle.Points[i].X, triangle2.Points[i].X);
                Assert.AreEqual(triangle.Points[i].Y, triangle2.Points[i].Y);
            }
            Assert.AreEqual(triangle.Angle, triangle2.Angle);
        }

        [Test]
        public void Move_triangle_1x1()
        {
            var triangle1 = new EllipseFactory().GetFigure(0, new GrRed.Vector(0, 0), new GrRed.Vector(5, 5));
            var delta = new GrRed.Vector(1, 1);

            var triangle2 = triangle1.Move(delta);

            Assert.AreEqual(delta, triangle2.Center);
        }

        [Test]
        public void SetScale_triangle()
        {
            var triangle = new TriangleFactory().GetFigure(0, new GrRed.Vector(0, 0), new GrRed.Vector(15, 5));
            var triangle2 = triangle.SetScale(15, 5);
            var scale1 = new GrRed.Vector(22.5, 7.5);

            Assert.AreEqual(scale1, triangle2.Scale);
        }

        [Test]
        public void SetScale_triangle_ishod()
        {
            var triangle = new TriangleFactory().GetFigure(0, new GrRed.Vector(0, 0), new GrRed.Vector(10, 5));
            var triangle2 = triangle.SetScale(10, 5);
            triangle2 = triangle2.SetScale(-10, -5);

            Assert.AreEqual(triangle.Scale.X, triangle2.Scale.X);
            Assert.AreEqual(triangle.Scale.Y, triangle2.Scale.Y);
        }
    }

    public class Ellipse_test
    {

        [Test]
        public void IsIn_ellips_in() // внутри. 
        {
            var elipse = new EllipseFactory().GetFigure(0, new GrRed.Vector(0, 0), new GrRed.Vector(15, 5));
            var point1 = new GrRed.Vector(12, 0);
            var point2 = new GrRed.Vector(0, 4);
            double eps = 0.1;

            Assert.AreEqual(true, elipse.IsIn(point1, eps));
            Assert.AreEqual(true, elipse.IsIn(point2, eps));
        }
        [Test]
        public void IsIn_ellips_out() // снаружи
        {
            var elipse = new EllipseFactory().GetFigure(0, new GrRed.Vector(0, 0), new GrRed.Vector(15, 5));
            var point1 = new GrRed.Vector(16, 0);
            var point2 = new GrRed.Vector(0, 8);
            double eps = 0.1;

            Assert.AreEqual(false, elipse.IsIn(point1, eps));
            Assert.AreEqual(false, elipse.IsIn(point2, eps));
        }
        [Test]
        public void IsIn_ellips_granic() // на границе
        {
            var elipse = new EllipseFactory().GetFigure(0, new GrRed.Vector(0, 0), new GrRed.Vector(15, 5));
            var point1 = new GrRed.Vector(15, 0);
            var point2 = new GrRed.Vector(0, 5);
            double eps = 0.1;

            Assert.AreEqual(true, elipse.IsIn(point1, eps));
            Assert.AreEqual(true, elipse.IsIn(point2, eps));
        }


        [Test]
        public void Reflection_ellips_vert_2x()
        {
            var elipse = new EllipseFactory().GetFigure(0, new GrRed.Vector(0, 0), new GrRed.Vector(15, 5));
            var elipse2 = elipse.Reflection(true); // 1 otr
            elipse2 = elipse2.Reflection(true); // 2 otr

            Assert.AreEqual(Math.Round(elipse.Angle, 2), Math.Round(elipse2.Angle, 10));
        }

        [Test]
        public void Reflection_ellips_vert_pi4()
        {
            var elipse = new EllipseFactory().GetFigure(Math.PI / 4, new GrRed.Vector(0, 0), new GrRed.Vector(15, 5));
            var elipse2 = elipse.Reflection(true); // 1 otr

            Assert.AreEqual(-Math.PI / 4, elipse2.Angle);
        }

        [Test]
        public void Reflection_ellips_goriz_2x()
        {
            var elipse = new EllipseFactory().GetFigure(0, new GrRed.Vector(0, 0), new GrRed.Vector(15, 5));
            var elipse2 = elipse.Reflection(false); // 1 otr
            elipse2 = elipse2.Reflection(false); // 2 otr

            Assert.AreEqual(Math.Round(elipse.Angle, 2), Math.Round(elipse2.Angle, 10));
        }

        [Test]
        public void Reflection_ellips_goriz_pi4()
        {
            var elipse = new EllipseFactory().GetFigure(Math.PI / 4, new GrRed.Vector(0, 0), new GrRed.Vector(15, 5));
            var elipse2 = elipse.Reflection(false); // 1 otr

            Assert.AreEqual(3 * Math.PI / 4, elipse2.Angle);
        }

        [Test]
        public void Move_ellips_1х1()
        {
            var elipse1 = new EllipseFactory().GetFigure(0, new GrRed.Vector(0, 0), new GrRed.Vector(5, 5));
            var delta = new GrRed.Vector(1, 1);

            var elipse2 = elipse1.Move(delta);

            Assert.AreEqual(delta, elipse2.Center);
        }

        [Test]
        public void Rotate_ellips_360_center()
        {
            var elipse = new EllipseFactory().GetFigure(0, new GrRed.Vector(0, 0), new GrRed.Vector(15, 5));
            var elipse2 = elipse.Rotate(2 * Math.PI);

            GrRed.Vector centr1 = elipse.Center;
            GrRed.Vector centr2 = elipse2.Center;

            Assert.AreEqual(centr1, centr2);
        }
        [Test]
        public void Rotate_ellips_360_scale()
        {
            var elipse = new EllipseFactory().GetFigure(0, new GrRed.Vector(0, 0), new GrRed.Vector(15, 5));
            var elipse2 = elipse.Rotate(2 * Math.PI);

            GrRed.Vector scale1 = elipse.Scale;
            GrRed.Vector scale2 = elipse2.Scale;

            Assert.AreEqual(scale1.X, scale2.X);
            Assert.AreEqual(scale1.Y, scale2.Y);
        }
        [Test]
        public void Rotate_ellips_360_angle()
        {
            var elipse = new EllipseFactory().GetFigure(0, new GrRed.Vector(0, 0), new GrRed.Vector(15, 5));
            var elipse2 = elipse.Rotate(2 * Math.PI);

            double angle1 = elipse.Angle;
            double angle2 = elipse2.Angle;

            Assert.AreEqual(angle1, angle2);
        }
        [Test]
        public void Rotate_ellips_pi_na_3()
        {
            var elipse = new EllipseFactory().GetFigure(0, new GrRed.Vector(0, 0), new GrRed.Vector(15, 5));
            var elipse2 = elipse.Rotate(Math.PI / 3);

            double angle1 = elipse.Angle;
            double angle2 = elipse2.Angle;

            Assert.AreEqual(Math.PI / 3, angle2);
        }

        [Test]
        public void SetScale_ellips()
        {
            var elipse = new EllipseFactory().GetFigure(0, new GrRed.Vector(0, 0), new GrRed.Vector(15, 5));
            var elipse2 = elipse.SetScale(15, 5);
            var scale1 = new GrRed.Vector(22.5, 7.5);

            Assert.AreEqual(scale1, elipse2.Scale);
        }

        [Test]
        public void SetScale_ellips_ishod()
        {
            var elipse = new EllipseFactory().GetFigure(0, new GrRed.Vector(0, 0), new GrRed.Vector(10, 5));
            var elipse2 = elipse.SetScale(10, 5);
            elipse2 = elipse2.SetScale(-10, -5);

            Assert.AreEqual(elipse.Scale.X, elipse2.Scale.X);
            Assert.AreEqual(elipse.Scale.Y, elipse2.Scale.Y);
        }

    }

    public class Square_test
    {
        [Test]
        public void IsIn_square_in()
        {
            var square = new SquareFactory().GetFigure(0, new GrRed.Vector(0, 0), new GrRed.Vector(10, 5));
            var point1 = new GrRed.Vector(8, 0);
            var point2 = new GrRed.Vector(0, 1);
            double eps = 0.1;

            Assert.AreEqual(true, square.IsIn(point1, eps));
            Assert.AreEqual(true, square.IsIn(point2, eps));
        }

        [Test]
        public void IsIn_square_out()
        {
            var square = new SquareFactory().GetFigure(0, new GrRed.Vector(0, 0), new GrRed.Vector(10, 5));
            var point1 = new GrRed.Vector(12, 0);
            var point2 = new GrRed.Vector(0, 10);
            double eps = 0.1;

            Assert.AreEqual(false, square.IsIn(point1, eps));
            Assert.AreEqual(false, square.IsIn(point2, eps));
        }

        [Test]
        public void Reflection_square_goriz_2x()
        {
            Vector[] points = new Vector[4];
            points[0] = new Vector(0, 0);
            points[1] = new Vector(0, 10);
            points[2] = new Vector(10, 10);
            points[3] = new Vector(10, 0);


            var square = new SquareFactory().GetFigure(points);

            var square2 = square.Reflection(false);
            square2 = square2.Reflection(false);

            for (int i = 0; i < 4; i++)
            {
                Assert.AreEqual(square.Points[i].X, square2.Points[i].X);
                Assert.AreEqual(square.Points[i].Y, square2.Points[i].Y);
            }
        }

        [Test]
        public void Reflection_square_goriz_pi4()
        {

            Vector[] points = new Vector[4];
            points[0] = new Vector(2, 0);
            points[1] = new Vector(0, 2);
            points[2] = new Vector(3, 5);
            points[3] = new Vector(5, 3);


            var square = new SquareFactory().GetFigure(points);

            var square2 = square.Reflection(true);

            Vector[] points2 = new Vector[4]; //ожидаем
            points2[0] = new Vector(3, 0);
            points2[1] = new Vector(5, 2);
            points2[2] = new Vector(2, 5);
            points2[3] = new Vector(0, 3);

            for (int i = 0; i < 4; i++)
            {
                Assert.AreEqual(points2[i].X, square2.Points[i].X);
                Assert.AreEqual(points2[i].Y, square2.Points[i].Y);
            }
            Assert.AreEqual(3 * Math.PI / 4, square2.Angle);
        }

        [Test]
        public void Reflection_square_vert_2x()
        {

            Vector[] points = new Vector[4];
            points[0] = new Vector(0, 0);
            points[1] = new Vector(0, 10);
            points[2] = new Vector(10, 10);
            points[3] = new Vector(10, 0);


            var square = new SquareFactory().GetFigure(points);

            var square2 = square.Reflection(true);
            square2 = square2.Reflection(true);

            for (int i = 0; i < 4; i++)
            {
                Assert.AreEqual(square.Points[i].X, square2.Points[i].X);
                Assert.AreEqual(square.Points[i].Y, square2.Points[i].Y);
            }
            Assert.AreEqual(square.Angle, square2.Angle);
        }

        [Test]
        public void Reflection_square_vert_pi4()
        {

            Vector[] points = new Vector[4];
            points[0] = new Vector(2, 0);
            points[1] = new Vector(0, 2);
            points[2] = new Vector(3, 5);
            points[3] = new Vector(5, 3);


            var square = new SquareFactory().GetFigure(points);

            var square2 = square.Reflection(true);

            Vector[] points2 = new Vector[4]; //ожидаем
            points2[0] = new Vector(2, 5);
            points2[1] = new Vector(0, 3);
            points2[2] = new Vector(3, 0);
            points2[3] = new Vector(5, 2);

            for (int i = 0; i < 4; i++)
            {
                Assert.AreEqual(points2[i].X, square2.Points[i].X);
                Assert.AreEqual(points2[i].Y, square2.Points[i].Y);
            }
            Assert.AreEqual(-Math.PI / 4, square2.Angle);
        }

        [Test]
        public void Move_square_1x1()
        {
            var square1 = new SquareFactory().GetFigure(0, new GrRed.Vector(0, 0), new GrRed.Vector(5, 5));
            var delta = new GrRed.Vector(1, 1);

            var square2 = square1.Move(delta);

            Assert.AreEqual(delta, square2.Center);
        }

        [Test]
        public void Rotate_square_360_angle()
        {
            Vector[] points = new Vector[4];
            points[0] = new Vector(0, 0);
            points[1] = new Vector(0, 10);
            points[2] = new Vector(10, 10);
            points[3] = new Vector(10, 0);

            var square = new SquareFactory().GetFigure(points);
            var square2 = square.Rotate(2 * Math.PI);

            Assert.AreEqual(square.Angle, square2.Angle);
        }

        [Test]
        public void Rotate_square_360_center()
        {
            Vector[] points = new Vector[4];
            points[0] = new Vector(0, 0);
            points[1] = new Vector(0, 10);
            points[2] = new Vector(10, 10);
            points[3] = new Vector(10, 0);

            var square = new SquareFactory().GetFigure(points);
            var square2 = square.Rotate(2 * Math.PI);

            Assert.AreEqual(square.Center, square2.Center);
        }

        [Test]
        public void Rotate_square_360_points()
        {
            Vector[] points = new Vector[4];
            points[0] = new Vector(0, 0);
            points[1] = new Vector(0, 10);
            points[2] = new Vector(10, 10);
            points[3] = new Vector(10, 0);

            var square = new SquareFactory().GetFigure(points);
            var square2 = square.Rotate(2 * Math.PI);

            for (int i = 0; i < 4; i++)
            {
                Assert.AreEqual(square.Points[i].X, square2.Points[i].X); // ne rab 2, 3
                Assert.AreEqual(square.Points[i].Y, square2.Points[i].Y); // ne rab 0, 3
            }
        }

        [Test]
        public void Rotate_square_pi_na_3()
        {
            Vector[] points = new Vector[4];
            points[0] = new Vector(0, 0);
            points[1] = new Vector(0, 10);
            points[2] = new Vector(10, 10);
            points[3] = new Vector(10, 0);

            var square = new SquareFactory().GetFigure(points);
            var square2 = square.Rotate(Math.PI / 3);

            Assert.AreEqual(Math.PI / 3, square2.Angle);
        }

        [Test]
        public void SetScale_square()
        {
            var square = new SquareFactory().GetFigure(0, new GrRed.Vector(0, 0), new GrRed.Vector(15, 5));
            var square2 = square.SetScale(15, 5);
            var scale1 = new GrRed.Vector(22.5, 7.5);

            Assert.AreEqual(scale1, square2.Scale);
        }

        [Test]
        public void SetScale_square_ishod()
        {
            var square = new SquareFactory().GetFigure(0, new GrRed.Vector(0, 0), new GrRed.Vector(10, 5));
            var square2 = square.SetScale(10, 5);
            square2 = square2.SetScale(-10, -5);

            Assert.AreEqual(square.Scale.X, square2.Scale.X);
            Assert.AreEqual(square.Scale.Y, square2.Scale.Y);
        }
    }
    public class Line_test
    {
        [Test]
        public void IsIn_line_in()
        {
            Vector[] points = new Vector[2];
            points[0] = new Vector(0, 0);
            points[1] = new Vector(10, 0);

            var line = new LineFactory().GetFigure(points);

            var point1 = new GrRed.Vector(0, 0);
            var point2 = new GrRed.Vector(5, 0);
            double eps = 0.1;

            Assert.AreEqual(true, line.IsIn(point1, eps));
            Assert.AreEqual(true, line.IsIn(point2, eps));
        }

        [Test]
        public void IsIn_line_out()
        {
            Vector[] points = new Vector[2];
            points[0] = new Vector(0, 0);
            points[1] = new Vector(10, 0);

            var line = new LineFactory().GetFigure(points);

            var point1 = new GrRed.Vector(0, 20);
            var point2 = new GrRed.Vector(15, 0);
            double eps = 0.1;

            Assert.AreEqual(true, line.IsIn(point1, eps));
            Assert.AreEqual(true, line.IsIn(point2, eps));
        }

        [Test]
        public void Move_line_0x1()
        {
            Vector[] points = new Vector[2];
            points[0] = new Vector(0, 0);
            points[1] = new Vector(10, 0);

            var line = new LineFactory().GetFigure(points);
            var delta = new GrRed.Vector(0, 1);

            var line2 = line.Move(delta);

            Assert.AreEqual(line.Points[0].X, line2.Points[0].X);
            Assert.AreEqual(1, line2.Points[0].Y);

            Assert.AreEqual(line.Points[1].X, line2.Points[1].X);
            Assert.AreEqual(1, line2.Points[1].Y);
        }

        [Test]
        public void Reflection_line_goriz_2x()
        {
            Vector[] points = new Vector[2];
            points[0] = new Vector(0, 0);
            points[1] = new Vector(10, 0);

            var line = new LineFactory().GetFigure(points);

            var line2 = line.Reflection(false);
            line2 = line2.Reflection(false);

            for (int i = 0; i < 2; i++)
            {
                Assert.AreEqual(line.Points[i].X, line2.Points[i].X);
                Assert.AreEqual(line.Points[i].Y, line2.Points[i].Y);
            }
        }

        [Test]
        public void Reflection_line_vert_2x()
        {
            Vector[] points = new Vector[2];
            points[0] = new Vector(0, 0);
            points[1] = new Vector(10, 0);

            var line = new LineFactory().GetFigure(points);

            var line2 = line.Reflection(true);
            line2 = line2.Reflection(true);

            for (int i = 0; i < 2; i++)
            {
                Assert.AreEqual(line.Points[i].X, line2.Points[i].X);
                Assert.AreEqual(line.Points[i].Y, line2.Points[i].Y);
            }
        }

        [Test]
        public void Rotate_line_360_points()
        {
            Vector[] points = new Vector[3];
            points[0] = new Vector(0, 0);
            points[2] = new Vector(10, 0);
            var line = new LineFactory().GetFigure(points);
            var line2 = line.Rotate(2 * Math.PI);

            for (int i = 0; i < 2; i++)
            {
                Assert.AreEqual(line.Points[i].X, line2.Points[i].X);
                Assert.AreEqual(line.Points[i].Y, line2.Points[i].Y);
            }
        }

        [Test]
        public void Rotate_line_360_angle()
        {
            Vector[] points = new Vector[3];
            points[0] = new Vector(0, 0);
            points[1] = new Vector(10, 0);
            var line = new LineFactory().GetFigure(points);
            var line2 = line.Rotate(2 * Math.PI);

            Assert.AreEqual(line.Angle, line2.Angle);
        }

        [Test]
        public void Rotate_line_360_center()
        {
            Vector[] points = new Vector[3];
            points[0] = new Vector(0, 0);
            points[1] = new Vector(10, 0);
            var line = new LineFactory().GetFigure(points);
            var line2 = line.Rotate(2 * Math.PI);

            Assert.AreEqual(line.Center, line2.Center);
        }

        [Test]
        public void Rotate_line_pi_na_3()
        {
            Vector[] points = new Vector[4];
            points[0] = new Vector(0, 0);
            points[1] = new Vector(10, 0);

            var square = new SquareFactory().GetFigure(points);
            var square2 = square.Rotate(Math.PI / 3);

            Assert.AreEqual(Math.PI / 3, square2.Angle);
        }

    }
}