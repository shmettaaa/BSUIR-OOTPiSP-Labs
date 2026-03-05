using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Figures
{
    public class RectangleShape : ClosedShape
    {
        private double _width;
        private double _height;

        public double Width
        {
            get => _width;
            set => _width = value > 0 ? value : 0;
        }

        public double Height
        {
            get => _height;
            set => _height = value > 0 ? value : 0;
        }

        public RectangleShape() : base()
        {
            Width = 0;
            Height = 0;
        }

        public RectangleShape(double x, double y, double width, double height)
            : base(x, y)
        {
            Width = width;
            Height = height;
        }

        public RectangleShape(double x, double y, double width, double height,
                             Brush stroke, Brush fill, double thickness)
            : base(x, y, stroke, fill, thickness)
        {
            Width = width;
            Height = height;
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