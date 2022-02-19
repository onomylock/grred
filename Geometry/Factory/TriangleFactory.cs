using GrRed.Geometry.Domain;

namespace GrRed.Geometry.Factory
{
    class TriangleFactory : FigureFactory
    {
        //Сюда вводить эти переменные, задать их и заполнить конструктор
        public TriangleFactory()
        {

        }

        public override IFigure GetFigure()
        {
            //Придумать что мы будем передавать в конструктор объекта круга
            Triangle triangle = new()
            {

            };
            return triangle;
        }
    }
}
