using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Figures.Factories
{
    public class CircleFactory : IFigureFactory
    {
        public int RequiredPointCount => 2;

        public Shape CreateFromPoints(IReadOnlyList<Point> points,
                                      Brush stroke, Brush fill, double thickness, int sides = 0)
        {
            if (points.Count < RequiredPointCount) return null;

            double radius = Math.Sqrt(
                Math.Pow(points[1].X - points[0].X, 2) +
                Math.Pow(points[1].Y - points[0].Y, 2));

            radius = Math.Max(radius, 1);

            return new CircleShape(points[0].X, points[0].Y, radius, stroke, fill, thickness);
        }
    }
}