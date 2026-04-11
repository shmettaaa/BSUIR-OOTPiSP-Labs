using System.Windows.Media;

namespace Figures
{
    public class CircleShape : ClosedShape
    {
        public double Cx { get; set; }
        public double Cy { get; set; }
        public double Radius { get; set; }

        public CircleShape(double cx, double cy, double radius,
                           Brush stroke, Brush fill, double thickness)
        {
            Cx = cx; Cy = cy; Radius = radius;
            Stroke = stroke;
            Fill = fill;
            StrokeThickness = thickness;
        }
    }
}