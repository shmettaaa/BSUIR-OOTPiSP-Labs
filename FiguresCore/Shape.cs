using System.Windows.Media;

namespace Figures
{
    public abstract class Shape
    {
        public Brush Stroke { get; set; }
        public double StrokeThickness { get; set; }

    }
}