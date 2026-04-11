using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Figures
{
    public interface IFigureFactory
    {
        int RequiredPointCount { get; }

        Shape CreateFromPoints(IReadOnlyList<Point> points,
                               Brush stroke,
                               Brush fill,
                               double thickness,
                               int sides = 0);
    }
}