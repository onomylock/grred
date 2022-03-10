﻿using GrRed.Geometry.Domain;

namespace GrRed.Geometry.Factory
{
    public class EllipseFactory : FigureFactory
    {
        public override IFigure GetFigure(double Angle, Vector Center, Vector Scale)
        {
            return new Ellipse(Angle, Center, Scale);
        }
    }
}