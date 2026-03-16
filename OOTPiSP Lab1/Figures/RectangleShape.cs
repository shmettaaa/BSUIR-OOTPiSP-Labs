using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Figures
{
    // Rectangle defined by top-left corner, width and height
    // Inherits from ClosedShape (has fill)
    public class RectangleShape : ClosedShape
    {
        // Top-left corner coordinates
        public double X { get; set; }
        public double Y { get; set; }

        // Dimensions
        public double Width { get; set; }
        public double Height { get; set; }

        // Constructor with all parameters
        public RectangleShape(double x, double y, double width, double height,
                              Brush stroke, Brush fill, double thickness)
            : base(stroke, fill, thickness)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        // Draw rectangle using WPF Rectangle element
        public override void Draw(Canvas canvas)
        {
            var rect = new System.Windows.Shapes.Rectangle
            {
                Width = Width,
                Height = Height,
                Stroke = Stroke,
                Fill = Fill,
                StrokeThickness = StrokeThickness
            };
            // Position rectangle at (X, Y)
            Canvas.SetLeft(rect, X);
            Canvas.SetTop(rect, Y);
            canvas.Children.Add(rect);
        }
    }
}