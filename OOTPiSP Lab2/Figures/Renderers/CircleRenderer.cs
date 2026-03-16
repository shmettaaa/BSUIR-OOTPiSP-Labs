using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Figures.Renderers
{
    public class CircleRenderer : IShapeRenderer
    {
        public void Render(Shape shape, Canvas canvas)
        {
            if (shape is CircleShape circle)
            {
                var wpfEllipse = new Ellipse
                {
                    Width = 2 * circle.Radius,
                    Height = 2 * circle.Radius,
                    Stroke = circle.Stroke,
                    Fill = circle.Fill,
                    StrokeThickness = circle.StrokeThickness
                };
                Canvas.SetLeft(wpfEllipse, circle.Cx - circle.Radius);
                Canvas.SetTop(wpfEllipse, circle.Cy - circle.Radius);
                canvas.Children.Add(wpfEllipse);
            }
        }
    }
}