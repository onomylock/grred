using GrRed;
using GrRed.Geometry.Factory;

using Newtonsoft.Json;

using NUnit.Framework;

namespace TestProject
{
    public class Tests
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
            var elipse = new EllipseFactory().GetFigure(0, new Vector(0, 0), new Vector(5, 5)); //храним элипс
            var elipse2 = new EllipseFactory().GetFigure(System.Math.PI, new Vector(0, 0), new Vector(5, -5)); //храним элипс
            bool axe = true;

            Assert.AreEqual(elipse2, elipse.Reflection(axe));
        }
    }
}