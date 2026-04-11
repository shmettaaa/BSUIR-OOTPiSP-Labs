using System.Windows.Media;

namespace Figures
{
    public class RegularPolygon : ClosedShape
    {
        public double Cx { get; set; }
        public double Cy { get; set; }
        public int Sides { get; set; }
        public double Radius { get; set; }

        public RegularPolygon(double cx, double cy, int sides, double radius,
                              Brush stroke, Brush fill, double thickness)
        {
            Cx = cx; Cy = cy; Sides = sides; Radius = radius;
            Stroke = stroke;
            Fill = fill;
            StrokeThickness = thickness;
        }
    }
}