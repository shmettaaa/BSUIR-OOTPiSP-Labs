using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Figures.Renderers
{
    public class EllipseRenderer : IShapeRenderer
    {
        public void Render(Shape shape, Canvas canvas)
        {
            if (shape is EllipseShape ellipse)
            {
                var wpfEllipse = new Ellipse
                {
                    Width = ellipse.Width,
                    Height = ellipse.Height,
                    Stroke = ellipse.Stroke,
                    Fill = ellipse.Fill,
                    StrokeThickness = ellipse.StrokeThickness
                };
                Canvas.SetLeft(wpfEllipse, ellipse.Cx - ellipse.Width / 2);
                Canvas.SetTop(wpfEllipse, ellipse.Cy - ellipse.Height / 2);
                canvas.Children.Add(wpfEllipse);
            }
        }
    }
}