using System.Windows.Media;

namespace Figures
{
    public class CircleShape : EllipseShape
    {
        public double Diameter
        {
            get => Width;
            set
            {
                Width = value;
                Height = value;
            }
        }

        public CircleShape() : base() { }

        public CircleShape(double x, double y, double diameter, Brush stroke, Brush fill, double thickness)
            : base(x, y, diameter, diameter, stroke, fill, thickness)
        {
        }
    }
}