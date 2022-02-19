using GrRed.Geometry.Domain;

namespace GrRed.Geometry.Factory
{
    public class SquareFactory : FigureFactory
    {
        //Сюда вводить эти переменные, задать их и заполнить конструктор
        public SquareFactory()
        {

        }

        public override IFigure GetFigure()
        {
            //Придумать что мы будем передавать в конструктор объекта круга
            Square square = new()
            {

            };
            return square;
        }
    }
}
