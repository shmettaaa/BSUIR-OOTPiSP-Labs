using System.Windows.Media;

namespace Figures
{
    public class LineShape : Shape
    {
        public double X1 { get; set; }
        public double Y1 { get; set; }
        public double X2 { get; set; }
        public double Y2 { get; set; }

        public LineShape(double x1, double y1, double x2, double y2,
                         Brush stroke, double thickness)
        {
            X1 = x1; Y1 = y1; X2 = x2; Y2 = y2;
            Stroke = stroke;
            StrokeThickness = thickness;
        }
    }
}