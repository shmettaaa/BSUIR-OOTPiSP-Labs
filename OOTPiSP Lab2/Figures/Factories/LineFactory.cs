using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Figures.Factories
{
    public class LineFactory : IFigureFactory
    {
        public int RequiredPointCount => 2;

        public Shape CreateFromPoints(IReadOnlyList<Point> points,
                                      Brush stroke, Brush fill, double thickness, int sides = 0)
        {
            if (points.Count < 2) return null;
            return new LineShape(points[0].X, points[0].Y, points[1].X, points[1].Y, stroke, thickness);
        }
    }
}