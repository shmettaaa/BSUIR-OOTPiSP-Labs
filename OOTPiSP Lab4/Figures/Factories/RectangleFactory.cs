using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Figures.Factories
{
    public class RectangleFactory : IFigureFactory
    {
        public int RequiredPointCount => 2;

        public Shape CreateFromPoints(IReadOnlyList<Point> points,
                                      Brush stroke, Brush fill, double thickness, int sides = 0)
        {
            if (points.Count < RequiredPointCount) return null;

            double x = Math.Min(points[0].X, points[1].X);
            double y = Math.Min(points[0].Y, points[1].Y);
            double width = Math.Abs(points[1].X - points[0].X);
            double height = Math.Abs(points[1].Y - points[0].Y);

            return new RectangleShape(x, y, width, height, stroke, fill, thickness);
        }
    }
}