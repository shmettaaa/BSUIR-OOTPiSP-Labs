using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Figures.Factories
{
    public class TriangleFactory : IFigureFactory
    {
        public int RequiredPointCount => 3;

        public Shape CreateFromPoints(IReadOnlyList<Point> points,
                                      Brush stroke, Brush fill, double thickness, int sides = 0)
        {
            if (points.Count < RequiredPointCount) return null;

            return new TriangleShape(
                points[0].X, points[0].Y,
                points[1].X, points[1].Y,
                points[2].X, points[2].Y,
                stroke, fill, thickness);
        }
    }
}