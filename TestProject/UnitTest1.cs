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
        public void IsIn_triangle()
        {
            var triangle = new TriangleFactory().GetFigure(0, new Vector(0, 0), new Vector(5, 5));
            var point = new Vector(2, 2);
            double eps = 0.1;

            Assert.AreEqual(true, triangle.IsIn(point, eps));
        }

        [Test]
        public void Reflection_triangle()
        {
            var triangle= new EllipseFactory().GetFigure(0, new Vector(0, 0), new Vector(5, 5));
            var triangle2 = new EllipseFactory().GetFigure(Math.PI, new Vector(0, 0), new Vector(5, -5));
            bool axe = true;

            Assert.AreEqual(triangle2, triangle.Reflection(axe));
        }
    }

    public class Ellips_test
    {
        [Test]
        public void IsIn_ellips()
        {
            var elipse = new EllipseFactory().GetFigure(0, new Vector(0, 0), new Vector(5, 5)); //храним элипс
            var point = new Vector(2, 2);
            double eps = 0.1;

            Assert.AreEqual(true, elipse.IsIn(point, eps));
        }

        [Test]
        public void Reflection_ellips()
        {
            var elipse = new EllipseFactory().GetFigure(0, new Vector(0, 0), new Vector(5, 5));
            var elipse2 = new EllipseFactory().GetFigure(Math.PI, new Vector(0, 0), new Vector(5, -5));
            bool axe = true;

            Assert.AreEqual(elipse2, elipse.Reflection(axe));
        }

        [Test]
        public void Move_ellips()
        {
            var elipse = new EllipseFactory().GetFigure(0, new Vector(0, 0), new Vector(5, 5));
            var elipse2 = new EllipseFactory().GetFigure(0, new Vector(1, 1), new Vector(5, 5));
            var delta = new Vector(1, 1);

            Assert.AreEqual(elipse2, elipse.Move(delta));
        }

        [Test]
        public void Rotate_ellips()
        {
            var elipse = new EllipseFactory().GetFigure(0, new Vector(0, 0), new Vector(5, 5));
            var elipse2 = elipse.Rotate(360);

            Vector centr1 = elipse.Center;
            Vector centr2 = elipse2.Center;

            Vector scale1 = elipse.Scale;
            Vector scale2 = elipse2.Scale;

            Assert.AreEqual(centr1, centr2);
            Assert.AreEqual(scale1, scale2);
        }

    }

    public class Square_test
    {
        [Test]
        public void IsIn_Square()
        {
            var square = new SquareFactory().GetFigure(0, new Vector(0, 0), new Vector(5, 5));
            var point = new Vector(2, 2);
            double eps = 0.1;

            Assert.AreEqual(true, square.IsIn(point, eps));
        }

        [Test]
        public void Reflection_square()
        {
            var elipse = new EllipseFactory().GetFigure(0, new Vector(0, 0), new Vector(5, 5));
            var elipse2 = new EllipseFactory().GetFigure(Math.PI, new Vector(0, 0), new Vector(5, -5));
            bool axe = true;

            Assert.AreEqual(elipse2, elipse.Reflection(axe));
        }
    }
}