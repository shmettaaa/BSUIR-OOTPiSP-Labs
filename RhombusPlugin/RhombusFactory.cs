using Figures;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace RhombusPlugin
{
    public class RhombusFactory : IFigureFactory
    {
        public int RequiredPointCount => 2;

        public Figures.Shape CreateFromPoints(IReadOnlyList<Point> points,
                                      System.Windows.Media.Brush stroke, System.Windows.Media.Brush fill, double thickness, int sides = 0)
        {
            if (points.Count < 2) return null;

            double x1 = points[0].X;
            double y1 = points[0].Y;
            double x2 = points[1].X;
            double y2 = points[1].Y;

            double left = Math.Min(x1, x2);
            double top = Math.Min(y1, y2);
            double right = Math.Max(x1, x2);
            double bottom = Math.Max(y1, y2);

            double width = right - left;
            double height = bottom - top;
            double centerX = (left + right) / 2;
            double centerY = (top + bottom) / 2;

            return new RhombusShape(centerX, centerY, width, height, stroke, fill, thickness);
        }
    }
}