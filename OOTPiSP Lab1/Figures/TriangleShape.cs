using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Figures
{
    public class TriangleShape : ClosedShape
    {
        public double X1 { get; set; }
        public double Y1 { get; set; }
        public double X2 { get; set; }
        public double Y2 { get; set; }
        public double X3 { get; set; }
        public double Y3 { get; set; }

        public TriangleShape() : base()
        {
            X1 = Y1 = X2 = Y2 = X3 = Y3 = 0;
        }

        public TriangleShape(double x1, double y1, double x2, double y2, double x3, double y3,
                             Brush stroke, Brush fill, double thickness = 2.0)
            : base(stroke, fill, thickness)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
            X3 = x3;
            Y3 = y3;
        }

        public override void Draw(Canvas canvas)
        {
            var polygon = new Polygon
            {
                Points = new PointCollection
                {
                    new Point(X1, Y1),
                    new Point(X2, Y2),
                    new Point(X3, Y3)
                },
                Stroke = Stroke,
                Fill = Fill,
                StrokeThickness = StrokeThickness
            };
            canvas.Children.Add(polygon);
        }
    }
}