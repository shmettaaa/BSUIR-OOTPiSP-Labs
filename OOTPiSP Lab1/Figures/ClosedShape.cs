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

        protected ClosedShape(Brush stroke, Brush fill, double thickness = 2.0)
            : base(stroke, thickness)
        {
            Fill = fill ?? Brushes.Transparent;
        }

        public abstract override void Draw(System.Windows.Controls.Canvas canvas);
    }
}