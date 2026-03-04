using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Figures
{
    public class RectangleShape : ClosedShape
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public RectangleShape() : base()
        {
            Width = 0;
            Height = 0;
        }

        public RectangleShape(double x, double y, double width, double height, Brush stroke, Brush fill, double thickness)
            : base(x, y, stroke, fill, thickness)
        {
            Width = width > 0 ? width : 0;
            Height = height > 0 ? height : 0;
        }

        public override void Draw(Canvas canvas)
        {
            var rect = new Rectangle
            {
                Width = Width,
                Height = Height,
                Stroke = Stroke,
                Fill = Fill,
                StrokeThickness = StrokeThickness
            };
            Canvas.SetLeft(rect, X);
            Canvas.SetTop(rect, Y);
            canvas.Children.Add(rect);
        }
    }
}