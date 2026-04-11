using Figures;
using System.Windows.Media;
using static System.Net.WebRequestMethods;

namespace RhombusPlugin
{
    public class RhombusShape : ClosedShape
    {
        public double CenterX { get; set; }
        public double CenterY { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public RhombusShape(double centerX, double centerY, double width, double height,
                            Brush stroke, Brush fill, double thickness)
        {
            CenterX = centerX;
            CenterY = centerY;
            Width = width;
            Height = height;
            Stroke = stroke;
            Fill = fill;
            StrokeThickness = thickness;
        }
    }
}