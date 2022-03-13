using GrRed;
using GrRed.Geometry.Factory;
using gui.Interface;
using Newtonsoft.Json;
using gui;
using NUnit.Framework;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;

namespace TestProject
{
    public class Tests
    {
        private MainViewModel model;
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
        public void TestColorRed()
        {
            InkCanvas canvas = new();
            model = new MainViewModel(canvas);
            var color = model.SelectColor;
            color.Execute("Red");
            Assert.AreEqual(model.paintingCanvas.DefaultDrawingAttributes.Color, Color.Red);
        }
    }
}