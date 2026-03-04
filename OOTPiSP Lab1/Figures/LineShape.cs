using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Figures
{
    public class LineShape : Shape
    {
        public double X2 { get; set; }
        public double Y2 { get; set; }

        public LineShape() : base()
        {
            X2 = 0;
            Y2 = 0;
        }

        public LineShape(double x1, double y1, double x2, double y2, Brush stroke, double thickness)
            : base(x1, y1, stroke, thickness)
        {
            X2 = x2;
            Y2 = y2;
        }

        public override void Draw(Canvas canvas)
        {
            var line = new Line
            {
                X1 = X,
                Y1 = Y,
                X2 = X2,
                Y2 = Y2,
                Stroke = Stroke,
                StrokeThickness = StrokeThickness
            };
            canvas.Children.Add(line);
        }
    }
}