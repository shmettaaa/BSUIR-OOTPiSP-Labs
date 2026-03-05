using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Figures
{
    public class EllipseShape : ClosedShape
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

        public EllipseShape() : base()
        {
            Width = 0;
            Height = 0;
        }

        public EllipseShape(double x, double y, double width, double height)
            : base(x, y)
        {
            Width = width;
            Height = height;
        }

        public EllipseShape(double x, double y, double width, double height,
                           Brush stroke, Brush fill, double thickness)
            : base(x, y, stroke, fill, thickness)
        {
            Width = width;
            Height = height;
        }

        public override void Draw(Canvas canvas)
        {
            var ellipse = new Ellipse
            {
                Width = Width,
                Height = Height,
                Stroke = Stroke,
                Fill = Fill,
                StrokeThickness = StrokeThickness
            };
            Canvas.SetLeft(ellipse, X);
            Canvas.SetTop(ellipse, Y);
            canvas.Children.Add(ellipse);
        }
    }
}