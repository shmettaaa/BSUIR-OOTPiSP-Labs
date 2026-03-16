using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Figures.Factories
{
    public class EllipseFactory : IFigureFactory
    {
        public int RequiredPointCount => 2;

        public Shape CreateFromPoints(IReadOnlyList<Point> points,
                                      Brush stroke, Brush fill, double thickness, int sides = 0)
        {
            if (points.Count < RequiredPointCount) return null;

            double cx = (points[0].X + points[1].X) / 2;
            double cy = (points[0].Y + points[1].Y) / 2;
            double width = Math.Abs(points[1].X - points[0].X);
            double height = Math.Abs(points[1].Y - points[0].Y);

            return new EllipseShape(cx, cy, width, height, stroke, fill, thickness);
        }
    }
}