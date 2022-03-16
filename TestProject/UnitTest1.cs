using GrRed;
using GrRed.Geometry.Factory;


using Newtonsoft.Json;

using NUnit.Framework;

using System.Collections.Generic;

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
        [Test]
        public void Test2()
        {
            List<IFigure> figureList = new List<IFigure>();
            var ellipse = new EllipseFactory().GetFigure(3, new Vector(1, 0), new Vector(2, 2));
            var triangle = new TriangleFactory().GetFigure(3, new Vector(0, 1), new Vector(2, 2));
            var settings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.All
            };

            var str = "NEWOBJ";
            str += JsonConvert.SerializeObject(ellipse, settings);
            str += "NEWOBJ";
            str += JsonConvert.SerializeObject(triangle, settings);

            string[] stringlist = str.Split("NEWOBJ");

            foreach(string strobj in stringlist)
            {
                if (strobj != "")
                {
                    var obj = JsonConvert.DeserializeObject(strobj, settings) as IFigure;
                    figureList.Add(obj);
                }
            }
            Assert.AreEqual(1.0, figureList[0].Center.X);
            Assert.AreEqual(0.0, figureList[0].Center.Y);
            Assert.AreEqual(0.0, figureList[1].Center.X);
            Assert.AreEqual(1.0, figureList[1].Center.Y);
        }
    }
}