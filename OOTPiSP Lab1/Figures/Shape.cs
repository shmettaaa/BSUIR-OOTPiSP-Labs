using System.Windows.Media;
using System.Windows.Controls;

namespace Figures
{
    // Abstract base class for all shapes - defines common properties
    public abstract class Shape
    {
        // Color of the outline
        public Brush Stroke { get; set; }

        // Thickness of the outline
        public double StrokeThickness { get; set; }

        // Constructor with basic parameters
        protected Shape(Brush stroke, double thickness)
        {
            Stroke = stroke;
            StrokeThickness = thickness;
        }

        // Abstract method - each shape must implement its own drawing logic
        public abstract void Draw(Canvas canvas);
    }
}