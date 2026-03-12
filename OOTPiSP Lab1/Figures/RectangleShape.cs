using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Figures
{
    public class RectangleShape : ClosedShape
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public RectangleShape() : base()
        {
            X = Y = Width = Height = 0;
        }

        public RectangleShape(double x, double y, double width, double height,
                              Brush stroke, Brush fill, double thickness = 2.0)
            : base(stroke, fill, thickness)
        {
            X = x;
            Y = y;
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