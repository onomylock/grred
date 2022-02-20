using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrRed.Geometry.Factory;

namespace GrRed.Logic
{
    public interface IButton
    {
        object sender { get; }
        void Get_Action(object sender);
        string Get_Name(object sender);
        
    }
    public class Help
    {
        Help() { }
        public static FigureFactory GetFactory(string name)
        {
            return name switch
            {
                "Circle" => new CircleFactory(),
                "Triangle" => new TriangleFactory(),
                "Square" => new SquareFactory(),
                _ => null,
            };
        }
    }
    public class MainLogic
    {
        private FigureFactory fabric = null;


    }
}
