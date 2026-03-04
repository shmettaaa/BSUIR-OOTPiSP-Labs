using System.Windows.Controls;
using System.Windows.Media;

namespace Figures
{
    public abstract class Shape
    {
        public double X { get; set; }
        public double Y { get; set; }
        public Brush Stroke { get; set; }
        public double StrokeThickness { get; set; }

        protected Shape()
        {
            X = 0;
            Y = 0;
            Stroke = Brushes.Black;
            StrokeThickness = 2.0;
        }

        protected Shape(double x, double y) : this()
        {
            X = x;
            Y = y;
        }

        protected Shape(double x, double y, Brush stroke, double thickness) : this(x, y)
        {
            Stroke = stroke ?? Brushes.Black;
            StrokeThickness = thickness > 0 ? thickness : 2.0;
        }

        public abstract void Draw(Canvas canvas);
    }
}