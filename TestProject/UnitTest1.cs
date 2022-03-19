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
        public void IsIn_triangle1() //внутри
        {
            var triangle = new TriangleFactory().GetFigure(0, new GrRed.Vector(0, 0), new GrRed.Vector(10, 5));
            var point1 = new GrRed.Vector(1, 0);
            var point2 = new GrRed.Vector(0, 1);
            double eps = 0.1;

            Assert.AreEqual(true, triangle.IsIn(point1, eps));
            Assert.AreEqual(true, triangle.IsIn(point2, eps));
        }
        [Test]
        public void IsIn_triangle2() //снаружи
        {
            var triangle = new TriangleFactory().GetFigure(0, new GrRed.Vector(0, 0), new GrRed.Vector(10, 5));
            var point1 = new GrRed.Vector(15, 0);
            var point2 = new GrRed.Vector(0, 10);
            double eps = 0.1;

            Assert.AreEqual(true, triangle.IsIn(point1, eps));
            Assert.AreEqual(true, triangle.IsIn(point2, eps));
        }

        [Test]
        public void Rotate_triangle()
        {
            Vector[] points = new Vector[3];
            points[0] = new Vector(0, 0);
            points[1] = new Vector(6, 8);
            points[2] = new Vector(10, 0);
            var triangle = new TriangleFactory().GetFigure(points);
            var triangle2 = triangle.Rotate(2 * Math.PI);

            Assert.AreEqual(triangle.Center.X, triangle2.Center.X);
            Assert.AreEqual(triangle.Center.Y, triangle2.Center.Y);
            Assert.AreEqual(triangle.Angle, triangle2.Angle);
        }

        [Test]
        public void Reflection_triangle_goriz()
        {
            //var triangle = new TriangleFactory().GetFigure(0, new GrRed.Vector(0, 0), new GrRed.Vector(15, 5));
            //var triangle2 = triangle.Reflection(false); // 1 otr
            //triangle2 = triangle2.Reflection(false); // 2 otr

            //Assert.AreEqual(triangle.Angle, triangle2.Angle);
            Vector[] points = new Vector[3];
            points[0] = new Vector(0, 0);
            points[1] = new Vector(0, 8);
            points[2] = new Vector(10, 0);

            var triangle = new TriangleFactory().GetFigure(points);

            var triangle2 = triangle.Reflection(false);
            triangle2 = triangle2.Reflection(false);

            Assert.AreEqual(triangle.Points[0].X, triangle2.Points[0].X);
            //Assert.AreEqual(triangle.Points[0].Y, triangle2.Points[0].Y);
        }

        [Test]
        public void Reflection_triangle_vert()
        {
            //var triangle = new TriangleFactory().GetFigure(0, new GrRed.Vector(0, 0), new GrRed.Vector(15, 5));
            //var triangle2 = triangle.Reflection(true); // 1 otr
            //triangle2 = triangle2.Reflection(true); // 2 otr

            //Assert.AreEqual(triangle.Angle, triangle2.Angle);

            Vector[] points = new Vector[3];
            points[0] = new Vector(0, 0);
            points[1] = new Vector(0, 8);
            points[2] = new Vector(10, 0);

            var triangle = new TriangleFactory().GetFigure(points);

            var triangle2 = triangle.Reflection(true);
            triangle2 = triangle2.Reflection(true);

            Assert.AreEqual(triangle.Points[0].X, triangle2.Points[0].X);
            //Assert.AreEqual(triangle.Points[0].Y, triangle2.Points[0].Y);
            //Assert.AreEqual(triangle.Angle, triangle2.Angle);
        }

        [Test]
        public void Move_triangle()
        {
            var triangle1 = new EllipseFactory().GetFigure(0, new GrRed.Vector(0, 0), new GrRed.Vector(5, 5));
            var delta = new GrRed.Vector(1, 1);

            var triangle2 = triangle1.Move(delta);

            Assert.AreEqual(delta, triangle2.Center);
        }
    }

    public class Ellipse_test
    {

        [Test]
        public void IsIn_ellips_1() // внутри. 
        {
            var elipse = new EllipseFactory().GetFigure(0, new GrRed.Vector(0, 0), new GrRed.Vector(15, 5));
            var point1 = new GrRed.Vector(12, 0);
            var point2 = new GrRed.Vector(0, 4);
            double eps = 0.1;

            Assert.AreEqual(true, elipse.IsIn(point1, eps));
            Assert.AreEqual(true, elipse.IsIn(point2, eps));
        }
        [Test]
        public void IsIn_ellips_2() // снаружи
        {
            var elipse = new EllipseFactory().GetFigure(0, new GrRed.Vector(0, 0), new GrRed.Vector(15, 5));
            var point1 = new GrRed.Vector(16, 0);
            var point2 = new GrRed.Vector(0, 8);
            double eps = 0.1;

            Assert.AreEqual(false, elipse.IsIn(point1, eps));
            Assert.AreEqual(false, elipse.IsIn(point2, eps));
        }
        [Test]
        public void IsIn_ellips_3() // на границе
        {
            var elipse = new EllipseFactory().GetFigure(0, new GrRed.Vector(0, 0), new GrRed.Vector(15, 5));
            var point1 = new GrRed.Vector(15, 0);
            var point2 = new GrRed.Vector(0, 5);
            double eps = 0.1;

            Assert.AreEqual(true, elipse.IsIn(point1, eps));
            Assert.AreEqual(true, elipse.IsIn(point2, eps));
        }


        [Test]
        public void Reflection_ellips_vert()
        {
            var elipse = new EllipseFactory().GetFigure(0, new GrRed.Vector(0, 0), new GrRed.Vector(15, 5));
            var elipse2 = elipse.Reflection(true); // 1 otr
            elipse2 = elipse2.Reflection(true); // 2 otr

            Assert.AreEqual(elipse.Angle, elipse2.Angle);
        }

        [Test]
        public void Reflection_ellips_vert_pi4() 
        {
            var elipse = new EllipseFactory().GetFigure(Math.PI / 4, new GrRed.Vector(0, 0), new GrRed.Vector(15, 5));
            var elipse2 = elipse.Reflection(true); // 1 otr

            Assert.AreEqual(-Math.PI / 4, elipse2.Angle);
            Assert.AreEqual(elipse.Scale.X, elipse2.Scale.X);
            Assert.AreEqual(elipse.Scale.Y, elipse2.Scale.Y);
        }

        [Test]
        public void Reflection_ellips_goriz()
        {
            var elipse = new EllipseFactory().GetFigure(0, new GrRed.Vector(0, 0), new GrRed.Vector(15, 5));
            var elipse2 = elipse.Reflection(false); // 1 otr
            elipse2 = elipse2.Reflection(false); // 2 otr

            Assert.AreEqual(elipse.Angle, elipse2.Angle);
        }

        [Test]
        public void Reflection_ellips_goriz_pi4() // вращение по часовой или против????
        {
            var elipse = new EllipseFactory().GetFigure(Math.PI / 4, new GrRed.Vector(0, 0), new GrRed.Vector(15, 5));
            var elipse2 = elipse.Reflection(false); // 1 otr

            Assert.AreEqual(3 * Math.PI / 4, elipse2.Angle);

            //Assert.AreEqual(elipse.Scale.X, elipse2.Scale.X);
            //Assert.AreEqual(elipse.Scale.Y, elipse2.Scale.Y);
        }

        [Test]
        public void Move_ellips()
        {
            var elipse1 = new EllipseFactory().GetFigure(0, new GrRed.Vector(0, 0), new GrRed.Vector(5, 5));
            var delta = new GrRed.Vector(1, 1);

            var elipse2 = elipse1.Move(delta);

            Assert.AreEqual(delta, elipse2.Center);
        }

        [Test]
        public void Rotate_ellips()
        {
            var elipse = new EllipseFactory().GetFigure(0, new GrRed.Vector(0, 0), new GrRed.Vector(15, 5));
            var elipse2 = elipse.Rotate(2 * Math.PI);

            GrRed.Vector centr1 = elipse.Center;
            GrRed.Vector centr2 = elipse2.Center;

            GrRed.Vector scale1 = elipse.Scale;
            GrRed.Vector scale2 = elipse2.Scale;

            double angle1 = elipse.Angle;
            double angle2 = elipse2.Angle;


            Assert.AreEqual(centr1, centr2);
            Assert.AreEqual(scale1, scale2);
            Assert.AreEqual(angle1, angle2);
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
        public void SetScale_ellips_2()
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
        public void IsIn_Square()
        {
            var square = new SquareFactory().GetFigure(0, new GrRed.Vector(0, 0), new GrRed.Vector(5, 5));
            var point = new GrRed.Vector(2, 2);
            double eps = 0.1;

            Assert.AreEqual(true, square.IsIn(point, eps));
        }

        [Test]
        public void Reflection_square()
        {
            var elipse = new EllipseFactory().GetFigure(0, new GrRed.Vector(0, 0), new GrRed.Vector(5, 5));
            var elipse2 = new EllipseFactory().GetFigure(Math.PI, new GrRed.Vector(0, 0), new GrRed.Vector(5, -5));
            bool axe = true;

            Assert.AreEqual(elipse2, elipse.Reflection(axe));
        }
    }
}