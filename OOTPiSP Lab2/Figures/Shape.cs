using System.Windows.Media;
using System.Windows.Controls;

namespace Figures
{
    public abstract class Shape
    {
        public Brush Stroke { get; set; }
        public double StrokeThickness { get; set; }

        protected Shape(Brush stroke, double thickness)
        {
            Stroke = stroke;
            StrokeThickness = thickness;
        }

        public abstract void Draw(Canvas canvas);
    }
}