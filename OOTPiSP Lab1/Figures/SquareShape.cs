using System.Windows.Media;

namespace Figures
{
    public class SquareShape : RectangleShape
    {
        public double Side
        {
            get => Width;
            set
            {
                Width = value;
                Height = value;
            }
        }

        public SquareShape() : base() { }

        public SquareShape(double x, double y, double side, Brush stroke, Brush fill, double thickness = 2.0)
            : base(x, y, side, side, stroke, fill, thickness)
        {
        }
    }
}