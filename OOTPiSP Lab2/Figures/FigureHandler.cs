using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Figures
{
    public class FigureHandler
    {
        public IFigureFactory Factory { get; }
        public Renderers.IShapeRenderer Renderer { get; }

        public FigureHandler(IFigureFactory factory, Renderers.IShapeRenderer renderer)
        {
            Factory = factory;
            Renderer = renderer;
        }

        public Shape Create(IReadOnlyList<Point> points, Brush stroke, Brush fill, double thickness, int sides = 0)
        {
            return Factory.CreateFromPoints(points, stroke, fill, thickness, sides);
        }
    }
}