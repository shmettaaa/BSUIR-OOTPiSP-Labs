using System.Windows.Media;

namespace Figures
{
    public class TriangleShape : ClosedShape
    {
        public double X1 { get; set; }
        public double Y1 { get; set; }
        public double X2 { get; set; }
        public double Y2 { get; set; }
        public double X3 { get; set; }
        public double Y3 { get; set; }

        public TriangleShape(double x1, double y1, double x2, double y2, double x3, double y3,
                             Brush stroke, Brush fill, double thickness)
        {
            X1 = x1; Y1 = y1; X2 = x2; Y2 = y2; X3 = x3; Y3 = y3;
            Stroke = stroke;
            Fill = fill;
            StrokeThickness = thickness;
        }
    }
}