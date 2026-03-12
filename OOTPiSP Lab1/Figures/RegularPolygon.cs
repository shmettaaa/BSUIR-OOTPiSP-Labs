using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Figures
{
    public class RegularPolygon : ClosedShape
    {
        public double Cx { get; set; }
        public double Cy { get; set; }
        public int Sides { get; set; }
        public double Radius { get; set; }

        public RegularPolygon() : base()
        {
            Cx = Cy = 0;
            Sides = 3;
            Radius = 0;
        }

        public RegularPolygon(double cx, double cy, int sides, double radius,
                              Brush stroke, Brush fill, double thickness = 2.0)
            : base(stroke, fill, thickness)
        {
            Cx = cx;
            Cy = cy;
            Sides = sides < 3 ? 3 : sides;
            Radius = radius > 0 ? radius : 0;
        }

        public override void Draw(Canvas canvas)
        {
            Point[] points = new Point[Sides];
            for (int i = 0; i < Sides; i++)
            {
                double angle = 2 * Math.PI * i / Sides - Math.PI / 2;
                points[i] = new Point(
                    Cx + Radius * Math.Cos(angle),
                    Cy + Radius * Math.Sin(angle)
                );
            }

            var polygon = new Polygon
            {
                Points = new PointCollection(points),
                Stroke = Stroke,
                Fill = Fill,
                StrokeThickness = StrokeThickness
            };
            canvas.Children.Add(polygon);
        }
    }
}