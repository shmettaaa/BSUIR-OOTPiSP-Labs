using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Figures.Renderers
{
    public class RectangleRenderer : IShapeRenderer
    {
        public void Render(Shape shape, Canvas canvas)
        {
            if (shape is RectangleShape rect)
            {
                var wpfRect = new System.Windows.Shapes.Rectangle
                {
                    Width = rect.Width,
                    Height = rect.Height,
                    Stroke = rect.Stroke,
                    Fill = rect.Fill,
                    StrokeThickness = rect.StrokeThickness
                };
                Canvas.SetLeft(wpfRect, rect.X);
                Canvas.SetTop(wpfRect, rect.Y);
                canvas.Children.Add(wpfRect);
            }
        }
    }
}