using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Figures.Factories
{
    public class RegularPolygonFactory : IFigureFactory
    {
        public int RequiredPointCount => 2;

        public Shape CreateFromPoints(IReadOnlyList<Point> points,
                                      Brush stroke, Brush fill, double thickness, int sides = 0)
        {
            if (points.Count < 2) return null;
            double r = Math.Sqrt(Math.Pow(points[1].X - points[0].X, 2) + Math.Pow(points[1].Y - points[0].Y, 2));
            r = Math.Max(r, 10);
            return new RegularPolygon(points[0].X, points[0].Y, sides, r, stroke, fill, thickness);
        }
    }
}