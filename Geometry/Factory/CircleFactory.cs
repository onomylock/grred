using GrRed.Geometry.Domain;

namespace GrRed.Geometry.Factory
{
    public class CircleFactory : FigureFactory
    {
        //Сюда вводить эти переменные, задать их и заполнить конструктор
        public CircleFactory()
        {

        }

        public override IFigure GetFigure()
        {
            //Придумать что мы будем передавать в конструктор объекта круга
            Circle circle = new()
            {

            };
            return circle;
        }
    }
}
