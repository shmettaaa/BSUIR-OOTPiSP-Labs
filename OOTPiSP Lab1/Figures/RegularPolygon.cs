using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Figures
{
    // Regular polygon (all sides and angles equal)
    // Defined by center, number of sides, and radius of circumscribed circle
    // Inherits from ClosedShape (has fill)
    public class RegularPolygon : ClosedShape
    {
        // Center coordinates
        public double Cx { get; set; }
        public double Cy { get; set; }

        // Number of sides (minimum 3)
        public int Sides { get; set; }

        // Distance from center to vertices
        public double Radius { get; set; }

        public RegularPolygon(double cx, double cy, int sides, double radius,
                              Brush stroke, Brush fill, double thickness)
            : base(stroke, fill, thickness)
        {
            Cx = cx;
            Cy = cy;
            Sides = sides;
            Radius = radius;
        }

        // Draw polygon by calculating all vertices
        public override void Draw(Canvas canvas)
        {
            // Array to store vertex positions
            Point[] points = new Point[Sides];

            // Calculate each vertex around the center
            for (int i = 0; i < Sides; i++)
            {
                // Angle from center to current vertex
                double angle = 2 * Math.PI * i / Sides - Math.PI / 2; // Start from top

                // Calculate x and y coordinates
                points[i] = new Point(
                    Cx + Radius * Math.Cos(angle),
                    Cy + Radius * Math.Sin(angle)
                );
            }

            // Create polygon with calculated points
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