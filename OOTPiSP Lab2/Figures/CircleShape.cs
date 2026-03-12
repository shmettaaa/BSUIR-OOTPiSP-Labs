using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Figures
{
    public class CircleShape : ClosedShape
    {
        public double Cx { get; set; }
        public double Cy { get; set; }
        public double Radius { get; set; }

        public CircleShape(double cx, double cy, double radius,
                           Brush stroke, Brush fill, double thickness)
            : base(stroke, fill, thickness)
        {
            Cx = cx;
            Cy = cy;
            Radius = radius;
        }

        public override void Draw(Canvas canvas)
        {
            var ellipse = new Ellipse
            {
                Width = 2 * Radius,
                Height = 2 * Radius,
                Stroke = Stroke,
                Fill = Fill,
                StrokeThickness = StrokeThickness
            };
            Canvas.SetLeft(ellipse, Cx - Radius);
            Canvas.SetTop(ellipse, Cy - Radius);
            canvas.Children.Add(ellipse);
        }
    }
}