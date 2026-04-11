using System.Windows.Media;

namespace Figures
{
    public class RectangleShape : ClosedShape
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public RectangleShape(double x, double y, double width, double height,
                              Brush stroke, Brush fill, double thickness)
        {
            X = x; Y = y; Width = width; Height = height;
            Stroke = stroke;
            Fill = fill;
            StrokeThickness = thickness;
        }
    }
}