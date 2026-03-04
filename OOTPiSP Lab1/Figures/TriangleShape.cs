using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Figures
{
    public class TriangleShape : ClosedShape
    {
        public Point Point1 { get; set; }
        public Point Point2 { get; set; }
        public Point Point3 { get; set; }

        public TriangleShape() : base()
        {
            Point1 = new Point(0, 0);
            Point2 = new Point(0, 0);
            Point3 = new Point(0, 0);
        }

        public TriangleShape(Point p1, Point p2, Point p3, Brush stroke, Brush fill, double thickness = 2.0)
            : base(p1.X, p1.Y, stroke, fill, thickness) 
        {
            Point1 = p1;
            Point2 = p2;
            Point3 = p3;
        }

        public override void Draw(Canvas canvas)
        {
            var polygon = new Polygon
            {
                Points = new PointCollection { Point1, Point2, Point3 },
                Stroke = Stroke,
                Fill = Fill,
                StrokeThickness = StrokeThickness
            };
            canvas.Children.Add(polygon);
        }
    }
}