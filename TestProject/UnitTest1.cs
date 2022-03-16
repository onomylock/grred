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

        //[Test]
        //public void Reflection_triangle()
        //{
        //    var triangle= new EllipseFactory().GetFigure(0, new Vector(0, 0), new Vector(5, 5));
        //    var triangle2 = new EllipseFactory().GetFigure(Math.PI, new Vector(0, 0), new Vector(5, -5));
        //    bool axe = true;

        //    Assert.AreEqual(triangle2, triangle.Reflection(axe));
        //}
    }

    public class Ellipse_test
    {
        [Test]
        public void IsIn_ellips_1() // внутри
        {
            var elipse = new EllipseFactory().GetFigure(0, new Vector(0, 0), new Vector(15, 5));
            var point = new Vector(4.9, 4.9);
            double eps = 0.1;

            Assert.AreEqual(true, elipse.IsIn(point, eps));
        }
        [Test]
        public void IsIn_ellips_2() // снаружи
        {
            var elipse = new EllipseFactory().GetFigure(0, new Vector(0, 0), new Vector(15, 5)); 
            var point = new Vector(7, 7);
            double eps = 0.1;

            Assert.AreEqual(false, elipse.IsIn(point, eps));
        }
        [Test]
        public void IsIn_ellips_3() // на границе
        {
            var elipse = new EllipseFactory().GetFigure(0, new Vector(0, 0), new Vector(15, 5)); 
            var point = new Vector(15, 5);
            double eps = 0.1;

            Assert.AreEqual(false, elipse.IsIn(point, eps));
        }

        [Test]
        public void Reflection_ellips_vert_angle()
        {
            var elipse = new EllipseFactory().GetFigure(Math.PI / 6, new Vector(0, 0), new Vector(15, 5));
            var elipse2 = new EllipseFactory().GetFigure(5 * Math.PI / 6, new Vector(0, 0), new Vector(15, -5));
            bool axe = true;

            Assert.AreEqual(elipse2.Angle, elipse.Reflection(axe).Angle);
        }

        [Test]
        public void Reflection_ellips_vert_scale()
        {
            var elipse = new EllipseFactory().GetFigure(Math.PI / 6, new Vector(0, 0), new Vector(15, 5));
            var elipse2 = new EllipseFactory().GetFigure(5 * Math.PI / 6, new Vector(0, 0), new Vector(15, -5));
            bool axe = true;

            Assert.AreEqual(elipse2.Scale, elipse.Reflection(axe).Scale);
        }

        [Test]
        public void Reflection_ellips_goriz_angle()
        {
            var elipse = new EllipseFactory().GetFigure(Math.PI / 6, new Vector(0, 0), new Vector(15, 5));
            var elipse2 = new EllipseFactory().GetFigure(11 * Math.PI / 6, new Vector(0, 0), new Vector(-15, 5));
            bool axe = false;

            Assert.AreEqual(elipse2.Angle, elipse.Reflection(axe).Angle);
        }

        [Test]
        public void Reflection_ellips_goriz_scale()
        {
            var elipse = new EllipseFactory().GetFigure(0, new Vector(0, 0), new Vector(15, 5));
            var elipse2 = new EllipseFactory().GetFigure(2 * Math.PI, new Vector(0, 0), new Vector(-15, 5));
            bool axe = false;

            Assert.AreEqual(elipse2.Scale, elipse.Reflection(axe).Scale);
        }

        [Test]
        public void Move_ellips()
        {
            var elipse1 = new EllipseFactory().GetFigure(0, new Vector(0, 0), new Vector(5, 5));
            var delta = new Vector (1, 1);

            var elipse2 = elipse1.Move(delta);

            Assert.AreEqual(delta, elipse2.Center);
        }

        [Test]
        public void Rotate_ellips()
        {
            var elipse = new EllipseFactory().GetFigure(0, new Vector(0, 0), new Vector(15, 5));
            var elipse2 = elipse.Rotate(Math.PI / 2);

            Vector centr1 = elipse.Center;
            Vector centr2 = elipse2.Center;

            var scale1 = new Vector(5, 15);
            Vector scale2 = elipse2.Scale;

            double angle1 = elipse.Angle;
            double angle2 = elipse2.Angle;


            //Assert.AreEqual(centr1, centr2);
            Assert.AreEqual(scale1, scale2); // ошибочка?
            //Assert.AreEqual(Math.PI / 2, angle2);
        }

        [Test]
        public void SetScale_ellips()
        {
            var elipse = new EllipseFactory().GetFigure(0, new Vector(0, 0), new Vector(15, 5));
            var elipse2 = elipse.SetScale(15, 5);

           var scale1 = new Vector(22.5, 7.5);

            Assert.AreEqual(scale1, elipse2.Scale);
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