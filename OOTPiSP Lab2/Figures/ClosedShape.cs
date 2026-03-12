using System.Windows.Media;

namespace Figures
{
    public abstract class ClosedShape : Shape
    {
        public Brush Fill { get; set; }

        protected ClosedShape(Brush stroke, Brush fill, double thickness)
            : base(stroke, thickness)
        {
            Fill = fill;
        }
    }
}