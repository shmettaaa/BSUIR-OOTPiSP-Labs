using System.Windows.Media;

namespace Figures
{
    // Base class for closed shapes (shapes that can be filled)
    public abstract class ClosedShape : Shape
    {
        // Fill color for closed shapes
        public Brush Fill { get; set; }

        // Constructor adds fill parameter
        protected ClosedShape(Brush stroke, Brush fill, double thickness)
            : base(stroke, thickness)
        {
            Fill = fill; // Closed shapes have interior fill
        }
    }
}