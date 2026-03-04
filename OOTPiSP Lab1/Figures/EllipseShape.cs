using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Figures
{
    public class EllipseShape : ClosedShape
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public EllipseShape() : base()
        {
            Width = 0;
            Height = 0;
        }

        public EllipseShape(double x, double y, double width, double height, Brush stroke, Brush fill, double thickness)
            : base(x, y, stroke, fill, thickness)
        {
            Width = width > 0 ? width : 0;
            Height = height > 0 ? height : 0;
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