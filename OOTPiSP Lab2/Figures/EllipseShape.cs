using System.Windows.Media;

namespace Figures
{
    public class EllipseShape : ClosedShape
    {
        public double Cx { get; set; }
        public double Cy { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public EllipseShape(double cx, double cy, double width, double height,
                            Brush stroke, Brush fill, double thickness)
        {
            Cx = cx; Cy = cy; Width = width; Height = height;
            Stroke = stroke;
            Fill = fill;
            StrokeThickness = thickness;
        }
    }
}