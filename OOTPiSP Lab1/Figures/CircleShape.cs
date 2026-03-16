using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Figures
{
    // Circle defined by center and radius
    // Not inherited from Ellipse (follows Liskov Substitution Principle)
    public class CircleShape : ClosedShape
    {
        // Center coordinates
        public double Cx { get; set; }
        public double Cy { get; set; }

        // Radius
        public double Radius { get; set; }

        public CircleShape(double cx, double cy, double radius,
                           Brush stroke, Brush fill, double thickness)
            : base(stroke, fill, thickness)
        {
            Cx = cx;
            Cy = cy;
            Radius = radius;
        }

        // Draw circle as ellipse with equal width and height
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
            // Position so that center is at (Cx, Cy)
            Canvas.SetLeft(ellipse, Cx - Radius);
            Canvas.SetTop(ellipse, Cy - Radius);
            canvas.Children.Add(ellipse);
        }
    }
}