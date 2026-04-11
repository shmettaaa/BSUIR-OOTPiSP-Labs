using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Figures.Renderers
{
    public class TriangleRenderer : IShapeRenderer
    {
        public void Render(Shape shape, Canvas canvas)
        {
            if (shape is TriangleShape triangle)
            {
                var polygon = new Polygon
                {
                    Points = new PointCollection
                    {
                        new Point(triangle.X1, triangle.Y1),
                        new Point(triangle.X2, triangle.Y2),
                        new Point(triangle.X3, triangle.Y3)
                    },
                    Stroke = triangle.Stroke,
                    Fill = triangle.Fill,
                    StrokeThickness = triangle.StrokeThickness
                };
                canvas.Children.Add(polygon);
            }
        }
    }
}