using System.Windows.Controls;
using System.Windows.Media;

namespace Figures
{
    public abstract class ClosedShape : Shape
    {
        public Brush Fill { get; set; }

        
        protected ClosedShape() : base()
        {
            Fill = Brushes.Transparent;
        }

        protected ClosedShape(double x, double y) : base(x, y)
        {
            Fill = Brushes.Transparent;
        }

        protected ClosedShape(double x, double y, Brush stroke, Brush fill,
            double thickness) : base(x, y, stroke, thickness)
        {
            Fill = fill ?? Brushes.Transparent;
        }

        public abstract override void Draw(Canvas canvas);
    }
}
