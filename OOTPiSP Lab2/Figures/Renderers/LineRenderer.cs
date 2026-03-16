using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Figures.Renderers
{
    public class LineRenderer : IShapeRenderer
    {
        public void Render(Shape shape, Canvas canvas)
        {
            if (shape is LineShape line)
            {
                var wpfLine = new Line
                {
                    X1 = line.X1,
                    Y1 = line.Y1,
                    X2 = line.X2,
                    Y2 = line.Y2,
                    Stroke = line.Stroke,
                    StrokeThickness = line.StrokeThickness
                };
                canvas.Children.Add(wpfLine);
            }
        }
    }
}