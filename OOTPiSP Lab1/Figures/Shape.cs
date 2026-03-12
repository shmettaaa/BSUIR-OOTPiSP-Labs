using System.Windows.Media;

namespace Figures
{
    public abstract class Shape
    {
        public Brush Stroke { get; set; }
        public double StrokeThickness { get; set; }

        protected Shape()
        {
            Stroke = Brushes.Black;
            StrokeThickness = 2.0;
        }

        protected Shape(Brush stroke, double thickness = 2.0)
        {
            Stroke = stroke ?? Brushes.Black;
            StrokeThickness = thickness > 0 ? thickness : 2.0;
        }

        public abstract void Draw(System.Windows.Controls.Canvas canvas);
    }
}